using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Core;

namespace Game.DataBase
{
    [System.Serializable]
    public class EntityStats : ICloneable<EntityStats>
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
        public EntityStats Clone() => new(health.Value, attack.Value, attackSpeed.Value);
        public EntityStats(float health, float attack, float attackSpeed)
        {
            this.health = new(health);
            this.attack = new(attack);
            this.attackSpeed = new(attackSpeed);
        }
        public EntityStats() { }
        #endregion methods
    }
}