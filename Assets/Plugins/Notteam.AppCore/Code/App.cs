using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Notteam.AppCore
{
    public class App
    {
        private static string _startSceneName;

        private static AppManager _appManager;

        public static AppManager AppManager => _appManager;

        private static void Initialize()
        {
            _appManager = UnityEngine.Object.FindAnyObjectByType<AppManager>();

            _appManager.Initialize(_startSceneName);
        }

        private static void OnBootSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            SceneManager.sceneLoaded -= OnBootSceneLoaded;

            Initialize();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Boot()
        {
            _startSceneName = SceneManager.GetActiveScene().name;

            if (_startSceneName != AppSettings.BootScene)
            {
                SceneManager.LoadScene(AppSettings.BootScene, LoadSceneMode.Single);

                SceneManager.sceneLoaded += OnBootSceneLoaded;
            }
            else
            {
                Initialize();
            }
        }

        public static T GetSystem<T>() where T : AppSystem
        {
            return _appManager.GetSystem<T>();
        }

        public static AppSystem GetSystem(Type type)
        {
            return _appManager.GetSystem(type);
        }
    }
}
