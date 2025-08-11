using EditorCustom.Attributes;
using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Events;
using Universal.Core;

namespace Game.UI.Overlay
{
    public class EntityStatsObserver : MonoBehaviour
    {
        #region fields & properties
        [Title("Default Events")]
        public UnityEvent<float> OnMaxHealthChangedEvent;
        public UnityEvent<float> OnHealthChangedEvent;
        public UnityEvent<float> OnAttackChangedEvent;
        public UnityEvent<float> OnAttackSpeedChangedEvent;

        [Title("Special Events")]
        public UnityEvent OnEntityDead;

        /// <summary>
        /// May be null
        /// </summary>
        public EntityStats Stats => stats;
        [Title("Misc")][SerializeField][ReadOnly] private EntityStats stats;
        private bool isInitialized = false;
        private bool isSubscribed = false;
        #endregion fields & properties

        #region methods
        public void Initialize(EntityStats stats)
        {
            if (isInitialized)
            {
                Debug.LogError("Trying to initialize again", this);
                return;
            }
            this.stats = stats;
            isInitialized = true;
            Subscribe();
        }
        protected virtual void OnEnable()
        {
            if (isInitialized) Subscribe();
        }
        protected virtual void OnDisable()
        {
            if (isInitialized) UnSubscribe();
        }
        private void Subscribe()
        {
            if (isSubscribed) return;
            OnSubscribe();
            isSubscribed = true;
        }
        private void UnSubscribe()
        {
            if (!isSubscribed) return;
            OnUnSubscribe();
            isSubscribed = false;
        }
        protected virtual void InvokeEvents()
        {
            OnMaxHealthChanged(stats.MaxHealth.Value);
            OnHealthChanged(stats.Health.Value);
            OnAttackChanged(stats.Attack.Value);
            OnAttackSpeedChanged(stats.AttackSpeed.Value);
        }
        protected virtual void OnSubscribe()
        {
            stats.MaxHealth.OnValueChanged += OnMaxHealthChanged;
            stats.Health.OnValueChanged += OnHealthChanged;
            stats.Attack.OnValueChanged += OnAttackChanged;
            stats.AttackSpeed.OnValueChanged += OnAttackSpeedChanged;
        }
        protected virtual void OnUnSubscribe()
        {
            stats.MaxHealth.OnValueChanged -= OnMaxHealthChanged;
            stats.Health.OnValueChanged -= OnHealthChanged;
            stats.Attack.OnValueChanged -= OnAttackChanged;
            stats.AttackSpeed.OnValueChanged -= OnAttackSpeedChanged;
        }
        private void OnMaxHealthChanged(float value) => OnMaxHealthChangedEvent?.Invoke(value);
        private void OnHealthChanged(float value)
        {
            OnHealthChangedEvent?.Invoke(value);
            if (value.Approximately(0, 0.1f)) OnEntityDead?.Invoke();
        }
        private void OnAttackChanged(float value) => OnAttackChangedEvent?.Invoke(value);
        private void OnAttackSpeedChanged(float value) => OnAttackSpeedChangedEvent?.Invoke(value);
        #endregion methods
    }
}