using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Notteam.AppCore
{
    public class App
    {
        private static string _initialSceneName;

        private static AppManager _appManager;

        public static AppManager AppManager => _appManager;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Boot()
        {
            _initialSceneName = SceneManager.GetActiveScene().name;

            if (_initialSceneName != AppSettings.BootScene)
            {
                SceneManager.LoadScene(AppSettings.BootScene, LoadSceneMode.Single);

                SceneManager.sceneLoaded += OnBootSceneLoaded;
            }
            else
            {
                Initialize();
            }
        }

        private static void OnBootSceneLoaded(Scene scene, LoadSceneMode loadMode)
        {
            SceneManager.sceneLoaded -= OnBootSceneLoaded;

            Initialize();
        }

        private static void Initialize()
        {
            _appManager = UnityEngine.Object.FindAnyObjectByType<AppManager>();

            _appManager.Initialize(_initialSceneName);
        }

        public static T GetSystem<T>() where T : AppSystem
        {
            return _appManager.GetSystem<T>();
        }

        public static AppSystem GetSystem(Type type)
        {
            return _appManager.GetSystem(type);
        }

        public static void LoadScene(string name)
        {
            _appManager.SceneLoader.LoadScene(name);
        }
    }
}
