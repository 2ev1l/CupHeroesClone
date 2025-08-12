using Game.DataBase;
using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Fight
{
    public class Monster : Entity
    {
        #region fields & properties
        private static Player Player => Player.Instance;
        public float MoveTimeToPlayer => moveTimeToPlayer;
        [SerializeField][Min(1f)] private float moveTimeToPlayer = 7f;
        private const float PLAYER_OFFSET_X = 100;
        private const float PLAYER_OFFSET_Y = 50;
        private MonsterInfo monsterInfo = null;
        #endregion fields & properties

        #region methods
        /// <summary>
        /// Use this to initialize monster
        /// </summary>
        /// <param name="monsterInfo"></param>
        public void Initialize(MonsterInfo monsterInfo)
        {
            this.monsterInfo = monsterInfo;
            base.Initialize(monsterInfo.Stats);
        }
        private void OnEnable()
        {
            Player.StatsObserver.OnEntityDead.AddListener(StopAttacking);
        }
        private void OnDisable()
        {
            Player.StatsObserver.OnEntityDead.RemoveListener(StopAttacking);
        }

        public void MoveToPlayer()
        {
            Vector3 newPos = Player.transform.localPosition;
            newPos.x += Random.Range(PLAYER_OFFSET_X * 0.9f, PLAYER_OFFSET_X * 1.1f);
            newPos.y += Random.Range(-PLAYER_OFFSET_Y, PLAYER_OFFSET_Y);
            MoveToPosition(newPos, moveTimeToPlayer, StartAttacking);
        }
        public override void AttackOnTarget()
        {
            Attack(Player);
        }
        public override void OnDeath()
        {
            GameData.Data.PlayerData.Wallet.IncreaseValue(monsterInfo.MoneyReward);
            base.OnDeath();
        }
        #endregion methods
    }
}