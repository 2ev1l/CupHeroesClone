using EditorCustom.Attributes;
using Game.Animation;
using Game.DataBase;
using Game.Serialization.World;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Collections;
using Universal.Collections.Generic;
using Zenject;

namespace Game.Fight
{
    public class Player : Entity
    {
        #region fields & properties
        private const float MIN_DISTANCE_TO_HAND_ATTACK = 150f;
        public static Player Instance { get; private set; }
        [SerializeField] private Transform enemySpawn;
        [Title("Arrows")]
        [SerializeField] private float arrowSpeed = 30f;
        [SerializeField] private float arrowHeight = 3f;
        [SerializeField] private ObjectPool<StaticPoolableObject> arrowPool;

        private List<Monster> monsters = null;
        #endregion fields & properties

        #region methods
        private void OnEnable()
        {
            Initialize(GameData.Data.PlayerData.Stats);
            GameData.Data.WaveData.OnWaveIncreased += SetMoveAnimation;
            SetMoveAnimation();
        }
        private void OnDisable()
        {
            GameData.Data.WaveData.OnWaveIncreased -= SetMoveAnimation;
        }
        public override void Initialize(EntityStats stats)
        {
            base.Initialize(stats);
            Instance = this;
        }
        private void SetMoveAnimation(int _)
        {
            SetIdleAnimation();
            SetMoveAnimation();
        }
        protected override void SetDamagedAnimation()
        {
            base.SetDamagedAnimation();
            SetIdleAnimation();
        }
        public void FindEnemies()
        {
            if (monsters != null)
                monsters.Clear();
            else
                monsters = new();
            //can be optimized
            enemySpawn.GetComponentsInChildren<Monster>(false, monsters);
            StartAttacking();
        }

        public override void AttackOnTarget()
        {
            Monster nearest = FindNearestAliveMonster(out float minDistance);
            if (minDistance > MIN_DISTANCE_TO_HAND_ATTACK)
            {
                attackAnimation = 3;
            }
            else
            {
                attackAnimation = 2;
            }

            if (nearest == null)
            {
                StopAttacking();
                SetIdleAnimation();
                return;
            }

            if (attackAnimation == 3)
            {
                ShootArrow(nearest);
                return;
            }
            Attack(nearest);
        }
        public void ShootArrow(Monster toMonster)
        {
            Arrow arrow = (Arrow)arrowPool.GetObject();
            arrow.transform.localPosition = transform.localPosition;

            Vector2 start2D = new(arrow.transform.position.x, arrow.transform.position.y);
            Vector2 target2D = new(toMonster.transform.position.x, toMonster.transform.position.y);
            float distance2D = Vector2.Distance(start2D, target2D);
            float flightDuration = distance2D / arrowSpeed;

            target2D = new(toMonster.transform.position.x - distance2D * flightDuration / toMonster.MoveTimeToPlayer, toMonster.transform.position.y);

            arrow.Initialize(target2D, flightDuration, arrowHeight);
            StartCoroutine(AttackIEnumerator(toMonster, flightDuration));
        }
        protected override IEnumerator AttackIEnumerator(IDamageReciever target, float timeBeforeAttack)
        {
            yield return new WaitForSeconds(timeBeforeAttack);
            Monster nearest = FindNearestAliveMonster(out _);
            if (nearest == null)
            {
                StopAttacking();
                SetIdleAnimation();
                yield break;
            }
            Attack(target);
        }
        private Monster FindNearestAliveMonster(out float minDistance)
        {
            minDistance = Mathf.Infinity;
            Monster nearestMonster = null;
            for (int i = 0; i < monsters.Count; ++i)
            {
                if (!monsters[i].IsUsing) continue;
                if (monsters[i].IsDead) continue;

                float newDist = Vector3.Distance(monsters[i].transform.localPosition, transform.localPosition);
                if (newDist < minDistance)
                {
                    nearestMonster = monsters[i];
                    minDistance = newDist;
                }
            }
            return nearestMonster;
        }

        #endregion methods
    }
}