using UnityEngine;
using UnityEngine.Events;

namespace Game.DataBase
{
    [System.Serializable]
    public abstract class Stat
    {
        #region fields & properties
        /// <summary>
        /// <see cref="{T0} - changedValue"/>
        /// </summary>
        public UnityAction<float> OnValueChanged;
        /// <summary>
        /// Must be greater than 0
        /// </summary>
        public float Value
        {
            get => value;
            set => SetValue(value);
        }
        [SerializeField][Min(0)] protected float value = 0f;
        #endregion fields & properties

        #region methods
        public abstract StatType GetStatType();
        public void SetValueOrZero(float value)
        {
            if (value < 0)
                value = 0f;
            SetValue(value);
        }
        protected virtual void SetValue(float value)
        {
            if (value < 0)
                throw new System.ArgumentOutOfRangeException("Value");
            this.value = value;
            OnValueChanged?.Invoke(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Must be greater than 0</param>
        public Stat(float value)
        {
            this.value = value;
        }
        #endregion methods
    }
}