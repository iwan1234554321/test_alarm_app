using System;
using UnityEngine;

namespace Notteam.Tweener
{
    [Serializable]
    public class Tween
    {
        private string _id;

        private float _time;
        private float _timeline;

        private bool  _isInterrupted;
        private bool  _isCompleted;

        private Action        onStartTween;
        private Action<float> onUpdateTween;
        private Action        onFinalTween;

        public Tween(string id, float time, Action onStartTween = null, Action<float> onUpdateTween = null, Action onFinalTween = null)
        {
            _id   = id;
            _time = time;

            this.onStartTween  = onStartTween;
            this.onUpdateTween = onUpdateTween;
            this.onFinalTween  = onFinalTween;
        }

        public Tween(string id, float time, bool isInterrupted, Action onStartTween = null, Action<float> onUpdateTween = null, Action onFinalTween = null)
        {
            _id            = id;
            _time          = time;
            _isInterrupted = isInterrupted;

            this.onStartTween  = onStartTween;
            this.onUpdateTween = onUpdateTween;
            this.onFinalTween  = onFinalTween;
        }

        public string ID => _id;
        public bool   IsInterrupted => _isInterrupted;
        public bool   IsCompleted   => _isCompleted;
        
        internal void UpdateTween(float deltaTime)
        {
            if (!_isCompleted)
            {
                if (_timeline <= 0.0f)
                    onStartTween?.Invoke();

                _timeline += (1.0f / _time) * deltaTime;

                onUpdateTween?.Invoke(Mathf.Clamp01(_timeline));

                if (_timeline >= 1.0f)
                {
                    onFinalTween?.Invoke();

                    _isCompleted = true;
                }
            }
        }

        internal void AdditiveID(GameObject gameObject)
        {
            _id += $": {gameObject.name} : {gameObject.GetInstanceID()}";
        }
    }
}
