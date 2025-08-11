using Codice.Client.Common;
using Game.DataBase;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Universal.Collections;
using Universal.Collections.Generic;
using Universal.Time;

namespace Game.Fight
{
    public class Entity : StaticPoolableObject, IDamageReciever
    {
        #region fields & properties
        public EntityStats Stats => observer.Stats;
        public EntityStatsObserver StatsObserver => observer;
        [SerializeField] private EntityStatsObserver observer;
        protected SPUM_Prefabs Animator => animator;
        [SerializeField] private SPUM_Prefabs animator;
        [SerializeField][Min(0)] protected int attackAnimation = 0;
        private readonly VectorTimeChanger positionChanger = new();
        #endregion fields & properties

        #region methods
        /// <summary>
        /// Use this to initialize entity
        /// </summary>
        /// <param name="stats"></param>
        public virtual void Initialize(EntityStats stats)
        {
            observer.Initialize(stats);
            observer.InvokeEvents();
            animator.PopulateAnimationLists();
            animator.OverrideControllerInit();
        }
        public void MoveToPosition(Vector3 pos, float time, Action onMoveEnd)
        {
            animator.PlayAnimation(PlayerState.MOVE, 0);
            Animator._anim.speed = 1f;
            positionChanger.SetValues(transform.localPosition, pos);
            positionChanger.SetActions(x => transform.localPosition = x, delegate
            {
                transform.localPosition = pos;
                animator.PlayAnimation(PlayerState.IDLE, 0);
                Animator._anim.speed = 1f;
                onMoveEnd?.Invoke();
            });
            positionChanger.Restart(time);
        }
        public void StopAttacking()
        {
            CancelInvoke(nameof(StartAttacking));
            Animator.PlayAnimation(PlayerState.IDLE, 0);
            Animator._anim.speed = 1f;
        }
        public virtual void StartAttacking()
        {
            Invoke(nameof(StartAttacking), 1f / Stats.AttackSpeed.Value);
            AttackOnTarget();
            Animator.PlayAnimation(PlayerState.ATTACK, attackAnimation);
            Animator._anim.speed = Stats.AttackSpeed.Value;
        }
        public virtual void AttackOnTarget()
        {
            throw new System.NotImplementedException();
        }
        protected IEnumerator AttackIEnumerator(IDamageReciever target, float timeBeforeAttack)
        {
            yield return new WaitForSeconds(timeBeforeAttack);
            Attack(target);
        }
        protected void Attack(IDamageReciever target)
        {
            target.TakeDamage(Stats.Attack.Value);
        }
        public void TakeDamage(float value)
        {
            float newHp = observer.Stats.Health.Value - value;
            if (newHp <= 0)
            {
                OnDeath();
                observer.Stats.Health.SetValueOrZero(newHp);
                return;
            }
            observer.Stats.Health.SetValueOrZero(newHp);
            animator.PlayAnimation(PlayerState.DAMAGED, 0);
            Animator._anim.speed = 1f;
        }
        public virtual void OnDeath()
        {
            StopAttacking();
            animator.PlayAnimation(PlayerState.DEATH, 0);
            Animator._anim.speed = 1f;
            IsUsing = false;
            CancelInvoke();
            StopAllCoroutines();
            positionChanger.Stop();
        }
        #endregion methods
    }
}