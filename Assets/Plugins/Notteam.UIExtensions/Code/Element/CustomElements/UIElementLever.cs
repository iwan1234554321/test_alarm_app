using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Notteam.UIExtensions
{
    public class UIElementLever : UIElement
    {
        private float   _angleDeltaOffset;
        private float   _angleOffset;
        private float   _angleStart;
        private float   _angleCurrent;
        private float   _angleDelta;

        private Vector3 _targetPosition;

        private bool    _grabbed;

        public float AngleStart       => _angleStart;
        public float AngleCurrent     => _angleCurrent;
        public float AngleDelta       => _angleDelta;

        public event Action<UIElementLever, bool> onGrabbed;

        private void OnEnable()
        {
            onPointerDown += OnGrab;
            onPointerUp   += OnRelease;
        }

        private void OnDisable()
        {
            onPointerDown -= OnGrab;
            onPointerUp   -= OnRelease;
        }

        private float GetCurrentAngle()
        {
            var angle = Vector3.SignedAngle(transform.up, Vector3.up, Vector3.forward);

            if (angle < 0.0f)
                angle += 360.0f;

            return angle;
        }

        private float GetAngleFromLeverToTargetPosition()
        {
            return Vector3.SignedAngle(transform.up, _targetPosition - transform.position, Vector3.back);
        }

        private void SetTargetPosition()
        {
            _targetPosition = Input.mousePosition;
        }

        private void Update()
        {
            if (_grabbed)
            {
                SetTargetPosition();

                _angleDeltaOffset = GetAngleFromLeverToTargetPosition() - _angleOffset;

                _angleDelta += _angleDeltaOffset;
            }
            else
            {
                _angleDeltaOffset = 0.0f;

                _angleDelta = 0.0f;
            }

            transform.localRotation *= Quaternion.AngleAxis(_angleDeltaOffset, Vector3.back);

            _angleCurrent = GetCurrentAngle();
        }

        private void CalculateOffsetAngle()
        {
            SetTargetPosition();

            _angleOffset = GetAngleFromLeverToTargetPosition();
        }

        private void CalculateStartAngle()
        {
            _angleStart = GetCurrentAngle();
        }

        private void OnGrab(PointerEventData data)
        {
            _grabbed = true;

            CalculateStartAngle();
            CalculateOffsetAngle();

            onGrabbed?.Invoke(this, _grabbed);
        }

        private void OnRelease(PointerEventData data)
        {
            _grabbed = false;

            onGrabbed?.Invoke(this, _grabbed);
        }

        public void ResetGrab()
        {
            _angleDelta = 0.0f;
        }
    }
}
