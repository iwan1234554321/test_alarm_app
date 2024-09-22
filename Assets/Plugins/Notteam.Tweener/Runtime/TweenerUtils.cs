using UnityEngine;

namespace Notteam.Tweener
{
    public static class TweenerUtils
    {
        public static string GetRightTweenID(this GameObject gameObject, string id)
        {
            return id + $" : {gameObject.name} : {gameObject.GetInstanceID()}";
        }

        public static Tween AddTween(this GameObject gameObject, Tween tween)
        {
            tween.ChangeIDByGameObject(gameObject);

            return Tweener.Manager.AddTween(tween);
        }

        public static void DestroyTween(this GameObject gameObject, string id)
        {
            Tweener.Manager.DestroyTween(gameObject.GetRightTweenID(id));
        }
    }
}
