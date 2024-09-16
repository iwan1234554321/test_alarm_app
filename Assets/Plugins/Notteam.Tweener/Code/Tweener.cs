using UnityEngine;

namespace Notteam.Tweener
{
    public static class Tweener
    {
        private static TweenerManager _manager;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Boot()
        {
            var managerGameObject = new GameObject("TweenManager", typeof(TweenerManager));

            Object.DontDestroyOnLoad(managerGameObject);

            _manager = managerGameObject.GetComponent<TweenerManager>();
        }

        public static void AddTween(this GameObject gameObject, Tween tween)
        {
            tween.AdditiveID(gameObject);

            _manager.AddTween(tween);
        }
    }
}