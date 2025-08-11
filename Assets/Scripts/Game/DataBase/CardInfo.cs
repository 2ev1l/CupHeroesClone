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