using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class StatChange
    {
        #region fields & properties
        public StatType StatType => statType;
        [SerializeField] private StatType statType;
        public float ChangeValue => changeValue;
        [SerializeField] private float changeValue = 0;
        #endregion fields & properties

        #region methods
        public string GetChangedText() => $"{statType.ToLanguage()} {(changeValue > 0 ? "+" : "")}{statType.ToDigitText(changeValue)}";
        #endregion methods
    }
}