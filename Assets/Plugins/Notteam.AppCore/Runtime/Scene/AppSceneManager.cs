using UnityEngine;

namespace Notteam.AppCore
{
    public class AppSceneManager : MonoBehaviour
    {
        private bool _initialized;

        private AppSystemManager _systemManager = new AppSystemManager();

        public AppSystemManager SystemManager => _systemManager;

        internal void Initialize()
        {
            _systemManager.Initialize(transform);

            _initialized = true;
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

        private void OnDisable()
        {
            if (_initialized)
                _systemManager.Deinitialize();
        }

        public T GetSystem<T>() where T : AppSystem
        {
            return SystemManager.GetSystem<T>();
        }
    }
}
