using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.DataBase
{
    [System.Serializable]
    public class WaveInfo : DBInfo
    {
        #region fields & properties
        public const int MAX_CARDS = 3;
        public IReadOnlyList<MonsterInfoSO> Monsters => monsters;
        [SerializeField] private List<MonsterInfoSO> monsters = new();
        public IReadOnlyList<CardInfoSO> PossibleCards => possibleCards;
        [SerializeField] private List<CardInfoSO> possibleCards = new();
        #endregion fields & properties

        #region methods
        public List<CardInfo> GetRandomCards()
        {
            List<CardInfo> tempList = possibleCards.Select(x => x.Data).ToList();
            int GetRandomIndex() => Random.Range(0, tempList.Count);
            void GetCard(out CardInfo c)
            {
                int index = GetRandomIndex();
                c = tempList[index];
                tempList.RemoveAt(index);
            }

            List<CardInfo> finalList = new();
            for (int i = 0; i < MAX_CARDS; ++i)
            {
                GetCard(out CardInfo c);
                finalList.Add(c);
            }
            return finalList;
        }
        #endregion methods
    }
}