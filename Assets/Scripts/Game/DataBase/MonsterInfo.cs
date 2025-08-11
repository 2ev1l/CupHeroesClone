using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class MonsterInfo : DBInfo
    {
        #region fields & properties
        /// <summary>
        /// Returns clone of 'stats' to prevent db modifying
        /// </summary>
        public EntityStats Stats => stats.Clone();
        [SerializeField] private EntityStats stats = new();
        public int MoneyReward => moneyReward;
        [SerializeField][Min(0)] private int moneyReward = 1;
        public GameObject Prefab => prefab;
        [SerializeField] private GameObject prefab;
        #endregion fields & properties

        #region methods

        #endregion methods
    }
}