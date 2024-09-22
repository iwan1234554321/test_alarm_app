using UnityEngine;

namespace Notteam.AppCore
{
    public abstract class AppSystemObject : MonoBehaviour
    {
        protected internal virtual void OnStart() { }
        protected internal virtual void OnUpdateFixed() { }
        protected internal virtual void OnUpdate() { }
        protected internal virtual void OnUpdateLate() { }
        protected internal virtual void OnFinal() { }
    }
}
