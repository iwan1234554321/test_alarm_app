using System;
using UnityEngine;

namespace Notteam.AppCore
{
    public class AppManager : MonoBehaviour
    {
        [SerializeField] internal AppManagerSettings Settings;

        private bool _initialized;

        private AppSceneLoader   _sceneLoader   = new AppSceneLoader();
        private AppSystemManager _systemManager = new AppSystemManager();

        public AppSceneLoader  SceneLoader  => _sceneLoader;
        public AppSystemManager SystemManager => _systemManager;

        internal void Initialize(string startSceneName)
        {
            if (Settings == null)
                return;

            _systemManager.Initialize(transform);

            DontDestroyOnLoad(gameObject);

            _initialized = true;

            if (startSceneName != AppSettings.BootScene)
                _sceneLoader.LoadScene(startSceneName);
            else
                _sceneLoader.LoadScene(Settings.MainScene);
        }

        private void FixedUpdate()
        {
            if (_initialized)
                _systemManager.OnUpdateFixed();
        }

        private void Update()
        {
            if (_initialized)
                _systemManager.OnUpdate();
        }

        private void LateUpdate()
        {
            if (_initialized)
                _systemManager.OnUpdateLate();
        }

        public T GetSystem<T>() where T : AppSystem
        {
            if (SystemManager.HasSystem<T>())
                return SystemManager.GetSystem<T>();
            else if (SceneLoader.SceneManager.SystemManager.HasSystem<T>())
                return SceneLoader.SceneManager.SystemManager.GetSystem<T>();
            else
                return null;
        }

        public AppSystem GetSystem(Type type)
        {
            if (SystemManager.HasSystem(type))
                return SystemManager.GetSystem(type);
            else if (SceneLoader.SceneManager.SystemManager.HasSystem(type))
                return SceneLoader.SceneManager.SystemManager.GetSystem(type);
            else
                return null;
        }
    }
}