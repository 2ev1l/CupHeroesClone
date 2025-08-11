using Game.DataBase;
using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Collections;
using Universal.Collections.Generic;
using Universal.Time;

namespace Game.Fight
{
    public class MonsterFactory : MonoBehaviour
    {
        #region fields & properties
        public const float MONSTER_MOVE_TIME = 2f;
        [SerializeField] private RectTransform monsterSpawnPosition;
        [SerializeField] private RectTransform monsterFinalPosition;
        [SerializeField] private float randomFinalX = 30f;

        public ObjectPool<StaticPoolableObject> MonstersPool => monstersPool;
        /// <summary>
        /// 'Entity'
        /// </summary>
        private ObjectPool<StaticPoolableObject> monstersPool = null;
        #endregion fields & properties

        #region methods
        public void SpawnMonstersInGrid()
        {
            WaveInfo waveInfo = GameData.Data.WaveData.CurrentWaveInfo;
            monstersPool = new(waveInfo.Monsters[0].Data.Prefab, monsterSpawnPosition);
            int monstersCount = waveInfo.Monsters.Count;

            Rect spawnRect = monsterSpawnPosition.rect;

            float aspectRatio = spawnRect.width / spawnRect.height;
            float sqrtCount = Mathf.Sqrt(monstersCount);

            int cols = Mathf.CeilToInt(sqrtCount * aspectRatio);
            int rows = Mathf.CeilToInt(monstersCount / (float)cols);

            while (rows * cols < monstersCount)
            {
                rows++;
            }

            float cellWidth = spawnRect.width / cols;
            float cellHeight = spawnRect.height / rows;

            int monsterIndex = 0;

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    if (monsterIndex >= monstersCount)
                    {
                        break;
                    }

                    float cellXMin = spawnRect.xMin + x * cellWidth;
                    float cellYMin = spawnRect.yMin + y * cellHeight;

                    float randomX = Random.Range(cellXMin, cellXMin + cellWidth);
                    float randomY = Random.Range(cellYMin, cellYMin + cellHeight);

                    Vector3 spawnPos = new Vector3(randomX, randomY, 0);

                    float randomSpawnTime = Random.Range(0f, 0.5f);

                    StartCoroutine(SpawnMonsterIEnumerator(randomSpawnTime, waveInfo, monsterIndex, spawnPos));

                    monsterIndex++;
                }
                if (monsterIndex >= monstersCount)
                {
                    break;
                }
            }
        }
        private IEnumerator SpawnMonsterIEnumerator(float timeToWait, WaveInfo waveInfo, int index, Vector3 localPos)
        {
            yield return new WaitForSeconds(timeToWait);
            SpawnMonster(waveInfo, index, localPos);
        }
        private void SpawnMonster(WaveInfo waveInfo, int index, Vector3 localPos)
        {
            MonsterInfo currentMonster = waveInfo.Monsters[index].Data;
            Monster monster = (Monster)monstersPool.GetObject();
            monster.Initialize(currentMonster.Stats);
            monster.transform.localPosition = localPos;
            Vector3 finalPosition = monster.transform.localPosition;
            float direction = monsterFinalPosition.localPosition.x - monster.transform.localPosition.x;
            finalPosition.x += direction + Random.Range(-randomFinalX, randomFinalX);
            monster.MoveToPosition(finalPosition, MONSTER_MOVE_TIME, delegate { monster.MoveToPlayer(); });
        }

        #endregion methods
    }
}