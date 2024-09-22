using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Notteam.AppCore
{
    [Serializable]
    public class AppSceneLoader
    {
        private string _nextSceneName;
        private string _prevSceneName;

        private Scene  _currentScene;

        private AppSceneManager _sceneManager;

        public Scene           CurrentScene => _currentScene;
        public AppSceneManager SceneManager => _sceneManager;

        private IEnumerator LoadSceneAsync(string name)
        {
            _nextSceneName = name == App.AppManager.Settings.LoadScene ? App.AppManager.Settings.MainScene : name;
            _prevSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

            yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(App.AppManager.Settings.LoadScene, LoadSceneMode.Additive);

            yield return UnloadSceneAsync(_prevSceneName);

            var loadView = UnityEngine.Object.FindAnyObjectByType<AppLoadView>();

            if (loadView)
            {
                var loadOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(_nextSceneName, LoadSceneMode.Additive);

                loadOperation.allowSceneActivation = false;

                while (!loadOperation.isDone)
                {
                    loadView.SliderEvent?.Invoke(loadOperation.progress);

                    if (loadOperation.progress >= 0.9f)
                    {
                        yield return new WaitForSeconds(2.5f);

                        loadOperation.allowSceneActivation = true;
                    }

                    yield return null;
                }

                yield return UnloadSceneAsync(_prevSceneName);

                _currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();

                _sceneManager = UnityEngine.Object.FindAnyObjectByType<AppSceneManager>();

                if (_sceneManager)
                    _sceneManager.Initialize();
                else
                    Debug.LogError("Scene Manager is not exist");
            }
            else
                Debug.LogError("Load View is not exist");
        }

        private IEnumerator UnloadSceneAsync(string name)
        {
            yield return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(name);

            _prevSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }

        public void LoadScene(string name)
        {
            App.AppManager.StartCoroutine(LoadSceneAsync(name));
        }
    }
}