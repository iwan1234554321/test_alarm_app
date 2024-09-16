using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Notteam.AppCore
{
    [Serializable]
    public class AppSceneManager
    {
        private string _nextSceneName;
        private string _prevSceneName;

        private AppSceneContextManager _sceneContextManager;

        public AppSceneContextManager SceneContext => _sceneContextManager;

        private IEnumerator LoadSceneAsync(string name)
        {
            _nextSceneName = name == App.AppManager.Settings.LoadScene ? App.AppManager.Settings.MainScene : name;
            _prevSceneName = SceneManager.GetActiveScene().name;

            yield return SceneManager.LoadSceneAsync(App.AppManager.Settings.LoadScene, LoadSceneMode.Additive);

            yield return UnloadSceneAsync(_prevSceneName);

            var loadView = UnityEngine.Object.FindAnyObjectByType<AppSceneLoadView>();

            if (loadView)
            {
                var loadOperation = SceneManager.LoadSceneAsync(_nextSceneName, LoadSceneMode.Additive);

                while (!loadOperation.isDone)
                {
                    loadView.SliderEvent?.Invoke(loadOperation.progress);

                    yield return null;
                }

                yield return UnloadSceneAsync(_prevSceneName);

                _sceneContextManager = UnityEngine.Object.FindAnyObjectByType<AppSceneContextManager>();

                if (_sceneContextManager)
                    _sceneContextManager.Initialize();
                else
                    Debug.LogError("Scene Context is not exist");
            }
            else
                Debug.LogError("Load View is not exist");
        }

        private IEnumerator UnloadSceneAsync(string name)
        {
            yield return SceneManager.UnloadSceneAsync(name);

            _prevSceneName = SceneManager.GetActiveScene().name;
        }

        private void LoadedScene(Scene scene, LoadSceneMode loadMode)
        {
        }

        private void UnloadedScene(Scene scene)
        {
        }

        internal void Initialize()
        {
            SceneManager.sceneLoaded   += LoadedScene;
            SceneManager.sceneUnloaded += UnloadedScene;
        }

        public void LoadScene(string name)
        {
            App.AppManager.StartCoroutine(LoadSceneAsync(name));
        }
    }
}