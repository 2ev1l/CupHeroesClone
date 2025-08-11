using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class MonsterInfo : DBInfo
    {
        #region fields & properties
        public Health Health => health;
        [SerializeField] private Health health = new(100);
        public Attack Attack => attack;
        [SerializeField] private Attack attack = new(1);
        public AttackSpeed AttackSpeed => attackSpeed;
        [SerializeField] private AttackSpeed attackSpeed = new(1);
        public int MoneyReward => moneyReward;
        [SerializeField][Min(0)] private int moneyReward = 1;
        public GameObject Prefab => prefab;
        [SerializeField] private GameObject prefab;
        #endregion fields & properties

        #region methods

        #endregion methods
    }
}