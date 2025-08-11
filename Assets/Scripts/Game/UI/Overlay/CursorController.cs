using UnityEngine;
using UnityEngine.Events;
using Game.Audio;
using AudioType = Game.Audio.AudioType;
using Game.UI.Cursor;
using Universal.Events;
using Zenject;

namespace Game.UI.Overlay
{
    [System.Serializable]
    public class CursorController : RequestExecutor, ITickable
    {
        #region fields & properties
        [SerializeField] private AudioClip onClickSound;

        /// <summary>
        /// Represents direction based on screen resolution. 
        /// Use <see cref="Vector3.ClampMagnitude(Vector3, float)"/> if you want to use const value
        /// </summary>
        public static Vector3 MouseDirection { get; private set; } = Vector3.zero;
        /// <summary>
        /// Represents position based on screen resolution
        /// </summary>
        public static Vector3 LastMousePointOnScreen { get; private set; } = Vector3.zero;
        /// <summary>
        /// Scaled <see cref="LastMousePointOnScreen"/> to (0,0)~(1,1)
        /// </summary>
        public static Vector3 LastMousePointOnScreenScaled { get; private set; } = Vector3.zero;
        private static Vector3 currentMousePosition3D = Vector3.zero;

        private static Camera MainCamera
        {
            get
            {
                if (mainCamera == null)
                {
                    mainCamera = Camera.main;
                }
                return mainCamera;
            }
        }
        private static Camera mainCamera = null;
        #endregion fields & properties

        #region methods
        public void DoClickSound()
        {
            if (onClickSound == null) return;
            AudioManager.PlayClip(onClickSound, AudioType.Sound);
        }

        private void Update()
        {
            Vector3 inputMousePosition = Input.mousePosition;
            MouseDirection = inputMousePosition - LastMousePointOnScreen;
            LastMousePointOnScreen = inputMousePosition;
            LastMousePointOnScreenScaled = MainCamera.ScreenToViewportPoint(LastMousePointOnScreen);

            currentMousePosition3D.x = inputMousePosition.x;
            currentMousePosition3D.y = inputMousePosition.y;
            currentMousePosition3D.z = MainCamera.nearClipPlane;
        }

        public override bool TryExecuteRequest(ExecutableRequest request)
        {
            if (request is not CursorUIRequest cursorRequest) return false;
            if (cursorRequest.DoClickSound)
            {
                DoClickSound();
                return true;
            }

            cursorRequest.Close();
            return true;
        }

        public void Tick() => Update();
        #endregion methods
    }
}