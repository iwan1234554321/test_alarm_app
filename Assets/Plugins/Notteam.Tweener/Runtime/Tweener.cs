using UnityEngine;

namespace Notteam.Tweener
{
    public static class Tweener
    {
        internal static TweenerManager Manager;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Boot()
        {
            var managerGameObject = new GameObject("TweenManager", typeof(TweenerManager));

            Object.DontDestroyOnLoad(managerGameObject);

            Manager = managerGameObject.GetComponent<TweenerManager>();
        }
    }
}