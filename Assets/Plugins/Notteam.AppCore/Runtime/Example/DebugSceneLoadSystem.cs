using UnityEngine;

namespace Notteam.AppCore
{
    public class DebugSceneLoadSystem : AppSystem
    {
        protected internal override void OnStart()
        {
            Debug.Log($"ON START ON : {App.AppManager.SceneLoader.CurrentScene.name}");
        }

        protected internal override void OnFinal()
        {
            Debug.Log($"ON FINAL ON : {App.AppManager.SceneLoader.CurrentScene.name}");
        }
    }
}
