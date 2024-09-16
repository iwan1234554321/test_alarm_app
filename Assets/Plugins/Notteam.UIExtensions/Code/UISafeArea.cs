using UnityEngine;

namespace Notteam.UIExtensions
{
    [ExecuteAlways]
    [RequireComponent(typeof(RectTransform))]
    public class UISafeArea : MonoBehaviour
    {
        private ScreenOrientation _screenOrientation;

        private Canvas        _canvas;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _canvas        = GetComponentInParent<Canvas>();
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            _screenOrientation = Screen.orientation;

            var anchorX = _screenOrientation == ScreenOrientation.Portrait || _screenOrientation == ScreenOrientation.LandscapeLeft ? 1 : 0;

            _rectTransform.anchorMin = new Vector2(anchorX, 0);
            _rectTransform.anchorMax = new Vector2(anchorX, 0);
            _rectTransform.pivot     = new Vector2(anchorX, 0);

            _rectTransform.anchoredPosition = new Vector2(0, 0);

            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Screen.safeArea.width / _canvas.scaleFactor);
            _rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.safeArea.height / _canvas.scaleFactor);
        }
    }
}
