using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class CardInfo : DBInfo
    {
        #region fields & properties
        public Sprite PreviewSprite => previewSprite;
        [SerializeField] private Sprite previewSprite;
        public IReadOnlyList<StatChange> StatChanges => statChanges;
        [SerializeField] private List<StatChange> statChanges;
        public int Price => price;
        [SerializeField][Min(1)] private int price = 1;
        #endregion fields & properties

        #region methods
        public string GetCardText()
        {
            StringBuilder sb = new();
            foreach (var el in statChanges)
            {
                sb.AppendLine(el.GetChangedText());
            }
            return sb.ToString();
        }
        #endregion methods
    }
}