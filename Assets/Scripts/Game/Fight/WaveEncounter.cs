using Game.DataBase;
using Game.Serialization.World;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Universal.Collections;
using Universal.Collections.Generic;
using Universal.Time;

namespace Game.Fight
{
    public class WaveEncounter : MonoBehaviour
    {
        #region fields & properties
        public UnityEvent OnNewWaveLoad;
        public UnityEvent OnFightStarted;
        public UnityEvent OnFightEnded;
        public UnityEvent OnWaveReachedMax;
        [SerializeField] private MonsterFactory monsterFactory;

        [SerializeField][Min(MonsterFactory.MONSTER_MOVE_TIME)] private float timeToMove = 5f;
        [SerializeField] private float cameraOffsetX = -100f;
        [SerializeField] private VectorTimeChanger cameraPositionChanger = new();
        [SerializeField] private RectTransform monsterDeathPosition;
        [SerializeField] private RectTransform monsterSpawnParent;
        private readonly List<Monster> currentWaveMonsters = new();
        #endregion fields & properties

        #region methods
        private void OnEnable()
        {
            GameData.Data.WaveData.OnWaveIncreased += LoadWave;
            LoadWave(GameData.Data.WaveData.CurrentWave);
        }
        private void OnDisable()
        {
            GameData.Data.WaveData.OnWaveIncreased -= LoadWave;
        }
        public void LoadWave(int currentWave)
        {
            MoveCamera();
            MoveOldMonsters();
            Timer timer = new();
            timer.OnChangeEnd = SpawnMonsters;
            timer.Restart(timeToMove - MonsterFactory.MONSTER_MOVE_TIME);
            OnNewWaveLoad?.Invoke();
        }
        private void MoveOldMonsters()
        {
            foreach (var el in currentWaveMonsters)
            {
                MoveOldMonster(el);
            }
        }
        private void MoveOldMonster(Monster monster)
        {
            VectorTimeChanger vtc = new();
            vtc.VTC.Curve = cameraPositionChanger.VTC.Curve;
            Vector3 woldPos = monsterDeathPosition.position;
            Vector3 localPos = monsterSpawnParent.InverseTransformPoint(woldPos);
            localPos.y = monster.transform.localPosition.y;
            vtc.SetValues(monster.transform.localPosition, localPos);
            vtc.SetActions(x => monster.transform.localPosition = x, monster.DisableObject);
            vtc.Restart(timeToMove - 0.5f);
        }
        private void SpawnMonsters() => monsterFactory.SpawnMonstersInGrid();
        private void MoveCamera()
        {
            Vector3 newCameraOffset = Camera.main.transform.position;
            newCameraOffset.x += cameraOffsetX;
            cameraPositionChanger.SetValues(Camera.main.transform.position, newCameraOffset);
            cameraPositionChanger.SetActions(x => Camera.main.transform.position = x, StartFight);
            cameraPositionChanger.Restart(timeToMove);
        }

        public void StartFight()
        {
            OnFightStarted?.Invoke();
            currentWaveMonsters.Clear();
            foreach (Monster el in monsterFactory.MonstersPool.Objects.Cast<Monster>())
            {
                el.StatsObserver.OnEntityDead.RemoveListener(TryEndWave);
                if (!el.IsUsing) continue;
                currentWaveMonsters.Add(el);
                el.StatsObserver.OnEntityDead.AddListener(TryEndWave);
            }
            Player.Instance.FindEnemies();
        }
        private void TryEndWave()
        {
            foreach (Monster el in monsterFactory.MonstersPool.Objects.Cast<Monster>())
            {
                if (!el.IsUsing) continue;
                if (el.IsDead) continue;
                return;
            }
            OnFightEnded?.Invoke();
        }
        public void CompleteWave()
        {
            bool increased = GameData.Data.WaveData.TryIncreaseWave();
            if (!increased)
            {
                OnWaveReachedMax?.Invoke();
                return;
            }
        }
        #endregion methods
    }
}