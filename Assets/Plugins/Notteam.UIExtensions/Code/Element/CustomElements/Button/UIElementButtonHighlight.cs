using System;
using UnityEngine;
using UnityEngine.UI;
using Notteam.Tweener;

namespace Notteam.UIExtensions
{
    [Serializable]
    public class UIElementButtonHighlightElement
    {
        [SerializeField] private MaskableGraphic graphic;
        [SerializeField] private Color press = Color.white;

        private Color normal;

        public MaskableGraphic Graphic => graphic;
        public Color Normal => normal;
        public Color Press  => press;

        public void SetNormalColor()
        {
            normal = graphic.color;
        }
    }

    [DefaultExecutionOrder(-1)]
    public class UIElementButtonHighlight : UIElementButton
    {
        [SerializeField] private float                             animationTime = 0.5f;
        [SerializeField] private bool                              stateLight;
        [SerializeField] private UIElementButtonHighlightElement[] highlightElements;

        private bool _pressed;

        private void Awake()
        {
            foreach (var element in highlightElements)
                element.SetNormalColor();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            onPress += OnPress;
        }

        protected override void OnDisable()
        {
            onPress -= OnPress;

            base.OnDisable();
        }

        private void OnPress(UIElementButton _, bool press)
        {
            if (press)
                _pressed = !_pressed;

            foreach (var element in highlightElements)
            {
                if (press)
                {
                    if (stateLight)
                    {
                        var currentColor = element.Graphic.color;

                        element.Graphic.gameObject.AddTween(new Tween("Color Lerp", animationTime, true, onUpdateTween: (t) =>
                        {
                            element.Graphic.color = Color.Lerp(currentColor, _pressed ? element.Press : element.Normal, t);
                        }));
                    }
                    else
                        element.Graphic.color = element.Press;
                }
                else
                {
                    if (!stateLight)
                        element.Graphic.color = element.Normal;
                }
            }
        }
    }
}
