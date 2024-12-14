using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Notteam.UIExtensions
{
    [DefaultExecutionOrder(0)]
    public abstract class UIElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
    {
        public bool interactable = true;

        protected event Action<PointerEventData> onPointerDown;
        protected event Action<PointerEventData> onPointerUp;
        protected event Action<PointerEventData> onPointerEnter;
        protected event Action<PointerEventData> onPointerExit;
        protected event Action<PointerEventData> onPointerMove;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (interactable)
                onPointerDown?.Invoke(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (interactable)
                onPointerUp?.Invoke(eventData);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (interactable)
                onPointerEnter?.Invoke(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (interactable)
                onPointerExit?.Invoke(eventData);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (interactable)
                onPointerMove?.Invoke(eventData);
        }
    }
}