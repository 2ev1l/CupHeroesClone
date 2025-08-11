using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class EntityStats
    {
        #region fields & properties
        public Health Health => health;
        [SerializeField] private Health health = new(100);
        public Attack Attack => attack;
        [SerializeField] private Attack attack = new(10);
        public AttackSpeed AttackSpeed => attackSpeed;
        [SerializeField] private AttackSpeed attackSpeed = new(1);
        #endregion fields & properties

        #region methods

        #endregion methods
    }
}