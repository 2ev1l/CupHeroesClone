using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Universal.Core;

namespace Game.Serialization.World
{
    [System.Serializable]
    public class Wallet: ICloneable<Wallet>
    {
        #region fields & properties
        /// <summary>
        /// <see cref="{T0}"/> - currentValue; <br></br>
        /// <see cref="{T1}"/> - changedAmount
        /// </summary>
        public UnityAction<int, int> OnValueChanged;
        public int Value => value;
        [SerializeField][Min(0)] private int value = 0;
        #endregion fields & properties

        #region methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">Must be > 0</param>
        /// <returns></returns>
        public bool CanDecreaseValue(int amount)
        {
            return value >= amount && CanChangeValue(amount);
        }
        private bool CanChangeValue(int amount)
        {
            return amount > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">Must be > 0</param>
        /// <returns></returns>
        public bool TryDecreaseValue(int amount)
        {
            if (!CanDecreaseValue(amount)) return false;
            SetValue(value - amount);
            return true;
        }
        public void IncreaseValue(int amount)
        {
            SetValue(value + amount);
        }
        private void SetValue(int newValue)
        {
            int amountChanged = newValue - value;
            this.value = newValue;
            OnValueChanged?.Invoke(value, amountChanged);
        }

        public Wallet Clone()
        {
            return new(this.value);
        }

        public Wallet() { }
        public Wallet(int value)
        {
            this.value = value;
        }
        #endregion methods
    }
}