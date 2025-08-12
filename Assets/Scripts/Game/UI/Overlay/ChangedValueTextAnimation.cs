using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.UI.Overlay
{
    public class ChangedValueTextAnimation : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private FloatingTextFactory textFactory;
        #endregion fields & properties

        #region methods
        public void DoAnimation(int changedValue) => DoAnimation((float)changedValue);
        public void DoAnimation(float changedValue)
        {
            string text = $"{(changedValue >= 0 ? "+" : "-")}{changedValue}";
            textFactory.SpawnText(text);
        }
        #endregion methods
    }
}