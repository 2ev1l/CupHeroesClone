using DebugStuff;
using EditorCustom.Attributes;
using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Fight
{
    public class PlayerStatsObserver : EntityStatsObserver
    {
        #region fields & properties

        #endregion fields & properties

        #region methods

        #endregion methods

#if UNITY_EDITOR
        [Title("Tests")]
        [SerializeField][DontDraw] private bool ___testBool;
        [Button(nameof(AddMoney))]
        private void AddMoney()
        {
            if (!DebugCommands.IsApplicationPlaying()) return;
            GameData.Data.PlayerData.Wallet.IncreaseValue(1);
        }
        [Button(nameof(AddMaxHealth))]
        private void AddMaxHealth()
        {
            if (!DebugCommands.IsApplicationPlaying()) return;
            GameData.Data.PlayerData.Stats.MaxHealth.Value += 10;
        }
        [Button(nameof(AddHealth))]
        private void AddHealth()
        {
            if (!DebugCommands.IsApplicationPlaying()) return;
            GameData.Data.PlayerData.Stats.Health.Value += 10;
        }

        [Button(nameof(AddAttack))]
        private void AddAttack()
        {
            if (!DebugCommands.IsApplicationPlaying()) return;
            GameData.Data.PlayerData.Stats.Attack.Value += 1;
        }
        [Button(nameof(AddAttackSpeed))]
        private void AddAttackSpeed()
        {
            if (!DebugCommands.IsApplicationPlaying()) return;
            GameData.Data.PlayerData.Stats.AttackSpeed.Value += 0.1f;
        }
        
#endif //UNITY_EDITOR

    }
}