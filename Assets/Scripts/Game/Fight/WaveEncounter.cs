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
        public UnityEvent OnFightStarted;
        public UnityEvent OnFightEnded;
        public UnityEvent OnWaveReachedMax;
        [SerializeField] private MonsterFactory monsterFactory;

        [SerializeField][Min(MonsterFactory.MONSTER_MOVE_TIME)] private float timeToMove = 5f;
        [SerializeField] private float cameraOffsetX = -100f;
        [SerializeField] private VectorTimeChanger cameraPositionChanger = new();

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
            Timer timer = new();
            timer.OnChangeEnd = SpawnMonsters;
            timer.Restart(timeToMove - MonsterFactory.MONSTER_MOVE_TIME);
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
            foreach (Monster el in monsterFactory.MonstersPool.Objects.Cast<Monster>())
            {
                el.StatsObserver.OnEntityDead.RemoveListener(TryEndWave);
                if (!el.IsUsing) continue;
                el.StatsObserver.OnEntityDead.AddListener(TryEndWave);
            }
            Player.Instance.FindEnemies();
        }
        private void TryEndWave()
        {
            foreach (var el in monsterFactory.MonstersPool.Objects)
            {
                if (!el.IsUsing) continue;
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
            }
        }
        #endregion methods
    }
}