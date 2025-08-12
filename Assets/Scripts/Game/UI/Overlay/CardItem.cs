using EditorCustom.Attributes;
using Game.DataBase;
using Game.Serialization.World;
using Game.UI.Collections;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Universal.Core;

namespace Game.UI.Overlay
{
    public class CardItem : ContextActionsItem<CardInfo>
    {
        #region fields & properties
        public UnityAction OnPurchased;

        [SerializeField] private Image image;
        [SerializeField] private TextMeshProUGUI descriptionText;
        [SerializeField] private TextMeshProUGUI priceText;
        [SerializeField] private GameObject purchaseButtonBlock;
        #endregion fields & properties

        #region methods
        protected override void UpdateUI()
        {
            base.UpdateUI();
            image.sprite = Context.PreviewSprite;
            descriptionText.text = Context.GetCardText();
            UpdateBuyOptions();
        }
        private bool CanBuy() => GameData.Data.PlayerData.Wallet.Value >= Context.Price;
        public void UpdateBuyOptions()
        {
            priceText.text = Context.Price.ToString();
            purchaseButtonBlock.SetActive(!CanBuy());
        }
        [SerializedMethod]
        public void GetCard()
        {
            if (!GameData.Data.PlayerData.Wallet.TryDecreaseValue(Context.Price)) return;
            AddStatChanges();
            OnPurchased?.Invoke();
        }
        private void AddStatChanges()
        {
            List<Stat> stats = GameData.Data.PlayerData.Stats.AllStats;
            stats.RemoveAt(1);
            foreach (StatChange stat in Context.StatChanges)
            {
                if (!stats.Exists(x => x.GetStatType().Equals(stat.StatType), out Stat exists)) continue;
                exists.Value += stat.ChangeValue;
            }
        }
        #endregion methods
    }
}