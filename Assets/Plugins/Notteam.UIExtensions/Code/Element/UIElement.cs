using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Notteam.UIExtensions
{
    [DefaultExecutionOrder(0)]
    public abstract class UIElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
    {
        protected event Action<PointerEventData> onPointerDown;
        protected event Action<PointerEventData> onPointerUp;
        protected event Action<PointerEventData> onPointerEnter;
        protected event Action<PointerEventData> onPointerExit;
        protected event Action<PointerEventData> onPointerMove;

        public void OnPointerDown(PointerEventData eventData)
        {
            onPointerDown?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            onPointerUp?.Invoke(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onPointerEnter?.Invoke(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onPointerExit?.Invoke(eventData);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            onPointerMove?.Invoke(eventData);
        }
    }
}