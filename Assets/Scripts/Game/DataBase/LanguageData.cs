using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Universal.Serialization;

namespace Game.DataBase
{
    [System.Serializable]
    public class LanguageData
    {
        #region fields & properties
        public string[] MenuData => menuData;
        [SerializeField][TextArea(0, 30)] private string[] menuData;
        public string[] GameData => gameData;
        [SerializeField][TextArea(0, 30)] private string[] gameData;

        public static string LanguagePath => Application.dataPath + "/StreamingAssets/Language";
        #endregion fields & properties

        #region methods
        public static LanguageData GetLanguage(string lang) => SavingUtils.LoadJson<LanguageData>(LanguagePath, $"{lang}.json");
        public static List<string> GetLanguageNames()
        {
            var diInfo = new DirectoryInfo(LanguagePath);
            var filesInfo = diInfo.GetFiles("*.json");
            List<string> list = new();
            if (filesInfo.Length == 0) return list;
            string englishName = LanguageSettings.DEFAULT_LANGUAGE;
            list.Add(englishName);
            for (int i = 0; i < filesInfo.Length; i++)
            {
                string name = filesInfo[i].Name;
                name = name.Remove(name.Length - 5);
                if (name.Equals(englishName)) continue;
                list.Add(name);
            }
            return list;
        }
        #endregion methods
    }
}