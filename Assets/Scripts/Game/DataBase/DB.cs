using UnityEngine;
using EditorCustom.Attributes;
using Universal.Core;
using System.Linq;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif //UNITY_EDITOR

namespace Game.DataBase
{
    [ExecuteAlways]
    public class DB : MonoBehaviour
    {
        #region fields & properties
        public static DB Instance
        {
            get
            {
#if UNITY_EDITOR
                if (!Application.isPlaying) //only in editor
                    return GameObject.FindFirstObjectByType<DB>(FindObjectsInactive.Include);
#endif //UNITY_EDITOR
                return instance;
            }
            set
            {
                if (instance == null)
                    instance = value;
            }
        }
        private static DB instance;

        public DBSOSet<MonsterInfoSO> Monsters => monsters;
        [SerializeField] private DBSOSet<MonsterInfoSO> monsters;
        public DBSOSet<CardInfoSO> Cards => cards;
        [SerializeField] private DBSOSet<CardInfoSO> cards;
        public DBSOSet<WaveInfoSO> Waves => waves;
        [SerializeField] private DBSOSet<WaveInfoSO> waves;
        #endregion fields & properties

        #region methods

        #endregion methods

#if UNITY_EDITOR
        [SerializeField] private bool automaticallyUpdateEditor = false;
        private void OnValidate()
        {
            if (!automaticallyUpdateEditor) return;
            GetAllDBInfo();
            CheckAllErrors();
        }
        /// <summary>
        /// You need to manually add code
        /// </summary>
        [Button(nameof(GetAllDBInfo))]
        private void GetAllDBInfo()
        {
            if (Application.isPlaying) return;
            AssetDatabase.Refresh();
            Undo.RegisterCompleteObjectUndo(this, "Update DB");

            monsters.CollectAll();
            cards.CollectAll();
            waves.CollectAll();
            //call dbset.CollectAll()

            EditorUtility.SetDirty(this);
        }
        /// <summary>
        /// You need to manually add code
        /// </summary>
        [Button(nameof(CheckAllErrors))]
        private void CheckAllErrors()
        {
            if (!Application.isPlaying) return;
            //call dbset.CatchExceptions(x => ...)
            System.Exception e = new();

            monsters.CatchExceptions(x => x.Data.Prefab == null, e, "Prefab must be not null");

            cards.CatchExceptions(x => x.Data.PreviewSprite == null, e, "Preview sprite must be not null");
            cards.CatchExceptions(x => x.Data.StatChanges.Count == 0, e, "Stat changes must be > 0");
            cards.CatchExceptions(x => x.Data.StatChanges.Exists(x => x.ChangeValue == 0, out _), e, "Changed stat value must be != 0");

            waves.CatchExceptions(x => x.Data.Monsters.Count == 0, e, "Monsters must be > 0");
            waves.CatchExceptions(x => x.Data.PossibleCards.Count < WaveInfo.MAX_CARDS, e, $"Max cards must be >= {WaveInfo.MAX_CARDS}");
        }

#endif //UNITY_EDITOR
    }
}