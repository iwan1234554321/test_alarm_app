using UnityEngine;

namespace Notteam.Tweener
{
    public class Example : MonoBehaviour
    {
        [SerializeField] private bool isInterrupted;
        [SerializeField] private bool isLoop;

        [Space]
        [SerializeField] private float          moveTime  = 1.0f;
        [SerializeField] private AnimationCurve moveCurve = AnimationCurve.Linear(0, 0, 1, 1);

        [Space]
        [SerializeField] private bool createTween;
        [SerializeField] private bool destroyTween;

        private void CreateTween()
        {
            var startPosition = transform.position;
            var startRotation = transform.rotation;

            var finalPosition = startPosition + Vector3.up;
            var finalRotation = startRotation * Quaternion.AngleAxis(45, Vector3.up);

            gameObject.AddTween(new Tween("MoveVertical", moveTime, isInterrupted, isLoop,
                onUpdateTween: (t) =>
                {
                    var startLerp = Mathf.InverseLerp(0.0f, 0.5f, t);
                    var finalLerp = Mathf.InverseLerp(0.5f, 1.0f, t);

                    var pingPongLerp = startLerp - (1.0f * finalLerp);

                    transform.position = Vector3.Lerp(startPosition, finalPosition, moveCurve.Evaluate(pingPongLerp));
                    transform.rotation = Quaternion.Lerp(startRotation, finalRotation, moveCurve.Evaluate(pingPongLerp));
                }));
        }

        private void Start()
        {
            CreateTween();
        }

        private void Update()
        {
            if (createTween)
            {
                CreateTween();

                createTween = false;
            }

            if (destroyTween)
            {
                gameObject.DestroyTween("MoveVertical");

                destroyTween = false;
            }
        }
    }
}
