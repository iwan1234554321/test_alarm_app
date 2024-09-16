using Notteam.AppCore;
using Notteam.UIExtensions;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Clock))]
public class ClockHandles : AppSystemObject
{
    [SerializeField] private UIElementLever hoursHandle;
    [SerializeField] private UIElementLever minutesHandle;
    [SerializeField] private UIElementLever secondsHandle;
    [Space]
    [SerializeField] private UnityEvent<bool> onGrabbedEvent;

    private int _hourTurnCount;

    private bool _grabbedHandle;
    private bool _grabbedHoursHandle;
    private bool _grabbedMinutesHandle;
    private bool _grabbedSecondsHandle;

    private float _grabbedHandleTotalSeconds;

    private float _lastTotalSeconds;

    private Clock _clock;

    public bool GrabbedHandle => _grabbedHandle;

    private void OnGrabbedHandle(UIElementLever handle, bool grabbed)
    {
        if (handle == hoursHandle)
        {
            _grabbedHoursHandle = grabbed;
        }

        if (handle == minutesHandle)
        {
            _grabbedMinutesHandle = grabbed;
        }

        if (handle == secondsHandle)
        {
            _grabbedSecondsHandle = grabbed;
        }

        _grabbedHandle = grabbed;

        _clock.GrabbedHandles = _grabbedHandle;

        if (_grabbedHandle)
            _lastTotalSeconds = _clock.TotalSeconds;

        onGrabbedEvent?.Invoke(_grabbedHandle);
    }

    protected override void OnStart()
    {
        _clock = GetComponent<Clock>();

        hoursHandle.onGrabbed   += OnGrabbedHandle;
        minutesHandle.onGrabbed += OnGrabbedHandle;
        secondsHandle.onGrabbed += OnGrabbedHandle;
    }

    private void SetClockSeconds()
    {
        if (_grabbedHandle)
        {
            _grabbedHandleTotalSeconds = Mathf.Clamp(_grabbedHandleTotalSeconds, -1, _clock.MaxSecondsOfDay + 1);

            if (_grabbedHandleTotalSeconds > _clock.MaxSecondsOfDay)
            {
                _lastTotalSeconds = 1;

                hoursHandle.ResetGrab();
                minutesHandle.ResetGrab();
                secondsHandle.ResetGrab();

                _clock.AddDay();
            }
            else if (_grabbedHandleTotalSeconds < 0)
            {
                _lastTotalSeconds = _clock.MaxSecondsOfDay - 1;

                hoursHandle.ResetGrab();
                minutesHandle.ResetGrab();
                secondsHandle.ResetGrab();

                _clock.RemoveDay();
            }

            _clock.SetSeconds(_grabbedHandleTotalSeconds);
        }
    }

    private void UpdateHandles()
    {
        if (hoursHandle)
        {
            var maxHourAngle    = 360 * _hourTurnCount;
            var maxMinutesAngle = 360.0f;
            var maxSecondsAngle = 360.0f;

            var hourHandleAngle    = Mathf.Lerp(0, maxHourAngle, Mathf.InverseLerp(0, _clock.MaxSecondsOfDay, _clock.TotalSeconds));
            var minutesHandleAngle = Mathf.Lerp(0, maxMinutesAngle, Mathf.InverseLerp(0, _clock.MaxSecondsOfMinutes, _clock.TotalSeconds % _clock.MaxSecondsOfMinutes));
            var secondsHandleAngle = Mathf.Lerp(0, maxSecondsAngle, Mathf.InverseLerp(0, _clock.SecondsOfMinutes, _clock.TotalSeconds % _clock.SecondsOfMinutes));

            if (_grabbedHoursHandle)
                _grabbedHandleTotalSeconds = _lastTotalSeconds + ClockUtils.ConvertAngleToTimeUnclamped(hoursHandle.AngleDelta, 0, _clock.MaxSecondsOfDay, true) * ClockUtils.SecondsInHour;
            else
                hoursHandle.transform.localRotation = Quaternion.AngleAxis(hourHandleAngle, Vector3.back);

            if (_grabbedMinutesHandle)
                _grabbedHandleTotalSeconds = _lastTotalSeconds + ClockUtils.ConvertAngleToTimeUnclamped(minutesHandle.AngleDelta, 0, _clock.MaxSecondsOfMinutes);
            else
                minutesHandle.transform.localRotation = Quaternion.AngleAxis(minutesHandleAngle, Vector3.back);

            if (_grabbedSecondsHandle)
                _grabbedHandleTotalSeconds = _lastTotalSeconds + ClockUtils.ConvertAngleToTimeUnclamped(secondsHandle.AngleDelta, 0, _clock.SecondsOfMinutes);
            else
                secondsHandle.transform.localRotation = Quaternion.AngleAxis(secondsHandleAngle, Vector3.back);

            SetClockSeconds();
        }
    }

    protected override void OnUpdate()
    {
        _hourTurnCount = _clock.HourOfDay / 12;

        UpdateHandles();
    }

    protected override void OnFinal()
    {
        hoursHandle.onGrabbed   -= OnGrabbedHandle;
        minutesHandle.onGrabbed -= OnGrabbedHandle;
        secondsHandle.onGrabbed -= OnGrabbedHandle;
    }
}
