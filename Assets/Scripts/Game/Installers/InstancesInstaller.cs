using Game.Fight;
using Game.UI.Overlay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Behaviour;
using Zenject;

namespace Game.Installers
{
    public class InstancesInstaller : MonoInstaller
    {
        #region fields & properties
        [SerializeField] private Player player;
        #endregion fields & properties

        #region methods
        public override void InstallBindings()
        {
            InstallPlayer();
        }
        private void InstallPlayer()
        {
            Container.Bind<Player>().FromInstance(player).AsSingle().NonLazy();
            Container.QueueForInject(player);
        }
        #endregion methods
    }
}