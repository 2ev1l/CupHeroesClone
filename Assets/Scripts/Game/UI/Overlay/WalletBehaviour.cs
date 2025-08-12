using EditorCustom.Attributes;
using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UI.Overlay
{
    public abstract class WalletBehaviour : MonoBehaviour
    {
        #region fields & properties
        /// <summary>
        /// <see cref="{T0}"/> - currentValue, <see cref="{T0}"/> - changedValue
        /// </summary>
        public UnityEvent<int, int> OnWalletChangedEvent;

        [SerializeField] private TextMeshProUGUI walletText;
        [SerializeField] private bool doAnimation = true;
        [SerializeField][DrawIf(nameof(doAnimation), true)] private ChangedValueTextAnimation textAnimation;
        public Wallet Wallet { get => wallet; protected set => wallet = value; }
        private Wallet wallet = null;
        #endregion fields & properties

        #region methods
        protected abstract void Initialize();
        private void Awake()
        {
            Initialize();
        }
        protected virtual void OnEnable()
        {
            wallet.OnValueChanged += OnWalletValueChanged;
            UpdateText();
        }
        protected virtual void OnDisable()
        {
            wallet.OnValueChanged -= OnWalletValueChanged;
        }
        private void OnWalletValueChanged(int currentValue, int changedAmount)
        {
            UpdateText();
            if (doAnimation)
                DoAnimation(changedAmount);
            OnWalletChangedEvent?.Invoke(currentValue, changedAmount);
        }
        private void UpdateText()
        {
            walletText.text = wallet.Value.ToString();
        }
        private void DoAnimation(int changedAmount)
        {
            textAnimation.DoAnimation(changedAmount);
        }
        #endregion methods
    }
}