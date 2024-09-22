using System;
using System.Collections.Generic;
using UnityEngine;

namespace Notteam.AppCore
{
    [Serializable]
    public class AppSystemManager
    {
        private AppSystem[]                 _systems;
        private Dictionary<Type, AppSystem> _systemsDictionary = new Dictionary<Type, AppSystem>();

        internal void Initialize(Transform parentSystems)
        {
            _systems = parentSystems.GetComponentsInChildren<AppSystem>();

            foreach (var system in _systems)
            {
                _systemsDictionary.Add(system.GetType(), system);
            }

            OnStartSystems();
        }

        internal void OnUpdateFixed()
        {
            OnUpdateFixedSystems();
        }

        internal void OnUpdate()
        {
            OnUpdateSystems();
        }

        internal void OnUpdateLate()
        {
            OnUpdateLateSystems();
        }

        internal void Deinitialize()
        {
            OnFinalSystems();
        }

        private void OnStartSystems()
        {
            foreach (var system in _systems)
            {
                if (system.enabled)
                    system.OnStartInternal();
            }
        }

        private void OnUpdateFixedSystems()
        {
            foreach (var system in _systems)
            {
                if (system.enabled)
                    system.OnUpdateFixedInternal();
            }
        }

        private void OnUpdateSystems()
        {
            foreach (var system in _systems)
            {
                if (system.enabled)
                    system.OnUpdateInternal();
            }
        }

        private void OnUpdateLateSystems()
        {
            foreach (var system in _systems)
            {
                if (system.enabled)
                    system.OnUpdateLateInternal();
            }
        }

        private void OnFinalSystems()
        {
            foreach (var system in _systems)
            {
                if (system.enabled)
                    system.OnFinalInternal();
            }
        }

        public T GetSystem<T>() where T : AppSystem
        {
            if (_systemsDictionary.TryGetValue(typeof(T), out var outSystem))
                return outSystem as T;

            return null;
        }

        public AppSystem GetSystem(Type type)
        {
            if (_systemsDictionary.TryGetValue(type, out var outSystem))
                return outSystem;

            return null;
        }

        public bool HasSystem<T>()
        {
            if (_systemsDictionary.ContainsKey(typeof(T)))
                return true;

            return false;
        }

        public bool HasSystem(Type type)
        {
            if (_systemsDictionary.ContainsKey(type))
                return true;

            return false;
        }
    }
}
