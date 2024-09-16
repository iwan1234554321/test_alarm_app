using UnityEngine;
using UnityEngine.UI;

namespace Notteam.UIExtensions
{
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class UIOrientationSizer : MonoBehaviour
    {
        [SerializeField] private bool workInEditor;
        [Space]
        [SerializeField] private UIOrientationTransformation portraitTransformation;
        [SerializeField] private UIOrientationTransformation landscapeLeftTransformation;
        [SerializeField] private UIOrientationTransformation landscapeRightTransformation;

        [Space]
        [SerializeField] private ScreenOrientation _screenOrientation;

        private RectTransform _rectTransform;

        private Canvas       _canvas;
        private CanvasScaler _canvasScaler;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            _canvas        = GetComponentInParent<Canvas>();
            _canvasScaler  = GetComponentInParent<CanvasScaler>();
        }

        [ContextMenu("SetDefaultPositionsForAllTransformation")]
        private void SetDefaultPositionsForAllTransformation()
        {
            portraitTransformation       = new UIOrientationTransformation(_rectTransform);
            landscapeLeftTransformation  = new UIOrientationTransformation(_rectTransform);
            landscapeRightTransformation = new UIOrientationTransformation(_rectTransform);
        }

        private void SetTransformation(UIOrientationTransformation transformation)
        {
            _rectTransform.anchoredPosition = transformation.Position;

            _rectTransform.sizeDelta = transformation.Size;

            _rectTransform.anchorMin = transformation.AnchorMin;
            _rectTransform.anchorMax = transformation.AnchorMax;

            _rectTransform.pivot = transformation.Pivot;

            _rectTransform.localScale = transformation.Scale;

            _rectTransform.localRotation = Quaternion.Euler(transformation.Rotation);
        }

        private void UpdateSizer()
        {
            switch (_screenOrientation)
            {
                case ScreenOrientation.Portrait:

                    SetTransformation(portraitTransformation);

                    break;

                case ScreenOrientation.LandscapeLeft:

                    SetTransformation(landscapeLeftTransformation);

                    break;

                case ScreenOrientation.LandscapeRight:

                    SetTransformation(landscapeRightTransformation);

                    break;
            }
        }

        private void Update()
        {
            if (_canvas == null && _canvasScaler == null)
                return;

            _screenOrientation = Screen.orientation;

            if (!Application.isPlaying)
            {
                if (workInEditor)
                    UpdateSizer();
            }
            else
                UpdateSizer();
        }
    }
}
