using Game.DataBase;
using Game.Serialization.World;
using Game.UI.Collections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UI.Overlay
{
    public class CardSelectList : ContextInfinityList<CardInfo>
    {
        #region fields & properties
        public UnityEvent OnCardGet;
        #endregion fields & properties

        #region methods
        protected override void OnDisable()
        {
            base.OnDisable();
            DoActionForCards(UnSubscribeForCard);
        }
        public override void UpdateListData()
        {
            DoActionForCards(UnSubscribeForCard);
            List<CardInfo> currentCards = GameData.Data.WaveData.CurrentWaveInfo.GetRandomCards();
            ItemList.UpdateListDefault(currentCards, x => x);
            DoActionForCards(SubscribeForCard);
        }
        private void UnSubscribeForCard(CardItem card)
        {
            card.OnPurchased -= OnCardPurchased;
        }
        private void SubscribeForCard(CardItem card)
        {
            card.OnPurchased += OnCardPurchased;
        }
        private void OnCardPurchased() => OnCardGet?.Invoke();
        private void DoActionForCards(System.Action<CardItem> action)
        {
            foreach (CardItem el in ItemList.Items.Cast<CardItem>())
            {
                action?.Invoke(el);
            }
        }
        #endregion methods
    }
}