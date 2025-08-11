using Game.DataBase;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Overlay
{
    public class EntityStatsExposer : MonoBehaviour
    {
        #region fields & properties
        [SerializeField] private TextMeshProUGUI maxHealthText;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private TextMeshProUGUI attackText;
        [SerializeField] private TextMeshProUGUI attackSpeedText;
        #endregion fields & properties

        #region methods
        public void SetMaxHealthText(float value)
        {
            healthSlider.maxValue = value;
            if (maxHealthText != null)
                maxHealthText.text = StatType.Health.ToDigitText(value);
        }
        public void SetHealthText(float value) => healthSlider.value = value;
        public void SetAttackText(float value) => attackText.text = StatType.Attack.ToDigitText(value);
        public void SetAttackSpeedText(float value) => attackSpeedText.text = StatType.AttackSpeed.ToDigitText(value);
        #endregion methods
    }
}