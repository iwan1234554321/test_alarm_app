using System;
using UnityEngine;
using UnityEngine.UI;

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
        [SerializeField] private UIElementButtonHighlightElement[] highlightElements;

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
            base.OnDisable();

            onPress -= OnPress;
        }

        private void OnPress(UIElementButton _, bool press)
        {
            foreach (var element in highlightElements)
            {
                if (press)
                {
                    element.Graphic.color = element.Press;
                }
                else
                {
                    element.Graphic.color = element.Normal;
                }
            }
        }
    }
}
