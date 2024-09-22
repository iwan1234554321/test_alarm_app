using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Notteam.AppCore
{
    public abstract class AppSystem : MonoBehaviour
    {
        internal virtual void OnStartInternal() { OnStart(); }
        internal virtual void OnUpdateFixedInternal() { OnUpdateFixed(); }
        internal virtual void OnUpdateInternal() { OnUpdate(); }
        internal virtual void OnUpdateLateInternal() { OnUpdateLate(); }
        internal virtual void OnFinalInternal() { OnFinal(); }

        protected internal virtual void OnStart() { }
        protected internal virtual void OnUpdateFixed() { }
        protected internal virtual void OnUpdate() { }
        protected internal virtual void OnUpdateLate() { }
        protected internal virtual void OnFinal() { }
    }

    public abstract class AppSystem<T> : AppSystem where T : AppSystemObject
    {
        private List<T> allSystemObjects;
        private List<T> systemObjects;

        public List<T> SystemObjects => systemObjects;

        /// <summary>
        /// Start after start of system objects
        /// </summary>
        protected internal virtual void OnStartAfterSystemObjects() { }

        internal override void OnStartInternal()
        {
            allSystemObjects = FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID).ToList();

            allSystemObjects.Sort((x, y) => y.GetInstanceID().CompareTo(x.GetInstanceID()));

            systemObjects = new List<T>();

            foreach (var systemObject in allSystemObjects)
            {
                if (systemObject.gameObject.activeInHierarchy)
                    systemObjects.Add(systemObject);
            }

            base.OnStartInternal();

            foreach (var systemObject in systemObjects)
            {
                systemObject.OnStart();
            }

            OnStartAfterSystemObjects();
        }

        internal override void OnUpdateFixedInternal()
        {
            base.OnUpdateFixedInternal();

            foreach (var systemObject in systemObjects)
            {
                systemObject.OnUpdateFixed();
            }
        }

        internal override void OnUpdateInternal()
        {
            base.OnUpdateInternal();

            foreach (var systemObject in systemObjects)
            {
                systemObject.OnUpdate();
            }
        }

        internal override void OnUpdateLateInternal()
        {
            base.OnUpdateLateInternal();

            foreach (var systemObject in systemObjects)
            {
                systemObject.OnUpdateLate();
            }
        }

        internal override void OnFinalInternal()
        {
            base.OnFinalInternal();

            foreach (var systemObject in systemObjects)
            {
                systemObject.OnFinal();
            }
        }
    }
}
