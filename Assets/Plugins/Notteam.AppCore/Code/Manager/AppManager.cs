using System;
using UnityEngine;

namespace Notteam.AppCore
{
    public class AppManager : MonoBehaviour
    {
        [SerializeField] internal AppManagerSettings Settings;

        private bool _initialized;

        private AppSceneManager  _sceneManager  = new AppSceneManager();
        private AppSystemManager _systemManager = new AppSystemManager();

        public AppSceneManager  SceneManager  => _sceneManager;
        public AppSystemManager SystemManager => _systemManager;

        internal void Initialize(string startSceneName)
        {
            if (Settings == null)
                return;

            _sceneManager.Initialize();
            _systemManager.Initialize(transform);

            DontDestroyOnLoad(gameObject);

            _initialized = true;

            if (startSceneName != AppSettings.BootScene)
                _sceneManager.LoadScene(startSceneName);
            else
                _sceneManager.LoadScene(Settings.MainScene);
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

        public T GetSystem<T>() where T : AppSystem
        {
            if (SystemManager.HasSystem<T>())
                return SystemManager.GetSystem<T>();
            else if (SceneManager.SceneContext.SystemManager.HasSystem<T>())
                return SceneManager.SceneContext.SystemManager.GetSystem<T>();
            else
                return null;
        }

        public AppSystem GetSystem(Type type)
        {
            if (SystemManager.HasSystem(type))
                return SystemManager.GetSystem(type);
            else if (SceneManager.SceneContext.SystemManager.HasSystem(type))
                return SceneManager.SceneContext.SystemManager.GetSystem(type);
            else
                return null;
        }

        public T GetAppSystem<T>() where T : AppSystem
        {
            return SystemManager.GetSystem<T>();
        }

        public T GetSceneSystem<T> () where T : AppSystem
        {
            return SceneManager.SceneContext.GetSystem<T>();
        }
    }
}