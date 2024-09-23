using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Notteam.UIExtensions
{
    public class UIElementButton : UIElement
    {
        public event Action<UIElementButton, bool> onPress;

        protected virtual void OnEnable()
        {
            onPointerDown += OnDown;
            onPointerUp   += OnUp;
        }

        protected virtual void OnDisable()
        {
            onPointerDown -= OnDown;
            onPointerUp   -= OnUp;
        }

        private void OnDown(PointerEventData data)
        {
            onPress?.Invoke(this, true);
        }

        private void OnUp(PointerEventData data)
        {
            onPress?.Invoke(this, false);
        }
    }
}
