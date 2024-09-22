using System.Collections.Generic;
using UnityEngine;

namespace Notteam.Tweener
{
    public class TweenerManager : MonoBehaviour
    {
        private Dictionary<string, Tween> _tweens;
        private Dictionary<string, Tween> _tweensUsed;

        private void Awake()
        {
            _tweens     = new Dictionary<string, Tween>();
            _tweensUsed = new Dictionary<string, Tween>();
        }

        private void Update()
        {
            foreach (var tween in _tweens)
            {
                if (!tween.Value.IsCompleted)
                    tween.Value.UpdateTween(Time.deltaTime);
                else
                {
                    AddTweenInUsed(tween.Value);
                }
            }

            ClearUsedTweens();
        }

        private void ClearUsedTweens()
        {
            if (_tweensUsed.Count > 0)
            {
                foreach (var tweenUsed in _tweensUsed)
                {
                    _tweens.Remove(tweenUsed.Key);

                    //Debug.Log($"REMOVED USED TWEEN : {tweenUsed.Key}");
                }

                _tweensUsed.Clear();

                //Debug.Log($"CLEAR USED TWEENS");
            }
        }

        private void AddTweenInUsed(Tween tween)
        {
            _tweensUsed.Add(tween.ID, tween);

            //Debug.Log($"ADDED USED TWEEN : {tween.ID}");
        }

        internal Tween AddTween(Tween tween)
        {
            if (_tweens.ContainsKey(tween.ID))
            {
                if (tween.IsInterrupted)
                {
                    AddTweenInUsed(_tweens.GetValueOrDefault(tween.ID));
                    ClearUsedTweens();
                }
                else
                {
                    Debug.Log($"Tween : {tween.ID} is not interrupted");

                    return null;
                }
            }

            _tweens.Add(tween.ID, tween);

            return _tweens.GetValueOrDefault(tween.ID);
        }

        internal void DestroyTween(string id)
        {
            if (_tweens.TryGetValue(id, out var tween))
                tween.IsCompleted = true;
        }
    }
}
