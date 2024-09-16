using System;
using UnityEngine;

namespace Notteam.UIExtensions
{
    [Serializable]
    public struct UIOrientationTransformation
    {
        [SerializeField] private Vector2 position;
        [SerializeField] private Vector2 size;
        [Space]
        [SerializeField] private Vector2 anchorMin;
        [SerializeField] private Vector2 anchorMax;
        [Space]
        [SerializeField] private Vector2 pivot;
        [Space]
        [SerializeField] private Vector3 scale;
        [Space]
        [SerializeField] private Vector3 rotation;

        public UIOrientationTransformation(RectTransform transform)
        {
            position = transform.anchoredPosition;

            size = transform.sizeDelta;

            anchorMin = transform.anchorMin;
            anchorMax = transform.anchorMax;

            pivot = transform.pivot;

            scale = transform.localScale;

            rotation = transform.localEulerAngles;
        }

        public Vector2 Position  => position;
        public Vector2 Size      => size;
        public Vector2 AnchorMin => anchorMin;
        public Vector2 AnchorMax => anchorMax;
        public Vector2 Pivot     => pivot;
        public Vector3 Scale     => scale;
        public Vector3 Rotation  => rotation;
    }
}
