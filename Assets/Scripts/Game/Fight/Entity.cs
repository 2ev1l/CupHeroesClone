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
        public bool IsDead => isDead;
        [SerializeField] private bool isDead = false;
        private readonly VectorTimeChanger positionChanger = new();
        #endregion fields & properties

        #region methods
        protected override void Init()
        {
            base.Init();
            isDead = false;
        }
        protected virtual void SetMoveAnimation()
        {
            animator.PlayAnimation(PlayerState.MOVE, 0);
            Animator._anim.speed = 1f;
        }
        protected virtual void SetIdleAnimation()
        {
            animator.PlayAnimation(PlayerState.IDLE, 0);
            Animator._anim.speed = 1f;
        }
        protected virtual void SetDeathAnimation()
        {
            animator.PlayAnimation(PlayerState.DEATH, 0);
            Animator._anim.speed = 1f;
        }
        protected virtual void SetDamagedAnimation()
        {
            animator.PlayAnimation(PlayerState.DAMAGED, 0);
            Animator._anim.speed = 1f;
        }
        protected virtual void SetAttackAnimation()
        {
            animator.PlayAnimation(PlayerState.ATTACK, attackAnimation);
            Animator._anim.speed = Stats.AttackSpeed.Value;
        }
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
            SetMoveAnimation();
            positionChanger.SetValues(transform.localPosition, pos);
            positionChanger.SetActions(x => transform.localPosition = x, delegate
            {
                transform.localPosition = pos;
                SetIdleAnimation();
                onMoveEnd?.Invoke();
            });
            positionChanger.Restart(time);
        }
        public void StopAttacking()
        {
            CancelInvoke(nameof(StartAttacking));
            SetIdleAnimation();
        }
        public virtual void StartAttacking()
        {
            Invoke(nameof(StartAttacking), 1f / Stats.AttackSpeed.Value);
            AttackOnTarget();
            SetAttackAnimation();
        }
        public virtual void AttackOnTarget()
        {
            throw new System.NotImplementedException();
        }
        protected virtual IEnumerator AttackIEnumerator(IDamageReciever target, float timeBeforeAttack)
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
            if (IsDead) return;
            float newHp = observer.Stats.Health.Value - value;
            if (newHp <= 0)
            {
                OnDeath();
                observer.Stats.Health.SetValueOrZero(newHp);
                return;
            }
            observer.Stats.Health.SetValueOrZero(newHp);
            SetDamagedAnimation();
        }
        public virtual void OnDeath()
        {
            isDead = true;
            StopAttacking();
            SetDeathAnimation();
            CancelInvoke();
            StopAllCoroutines();
            positionChanger.Stop();
        }
        #endregion methods
    }
}