using System;
using Notteam.AppCore;
using Notteam.UIExtensions;
using UnityEngine;

[Serializable]
public struct AlarmReferences
{
    public Clock         Clock;
    public ClockHandles  ClockHandles;
    public ClockViewBase ClockView;
}

[RequireComponent(typeof(AudioSource))]
public class AlarmSystem : AppSystem
{
    [SerializeField] private AlarmData alarm;

    [Space]
    [SerializeField] private int clockScreenIndex;
    [SerializeField] private int alarmScreenIndex;
    [Space]
    [SerializeField] private AudioClip alarmSound;

    [Header("References")]
    [SerializeField] private UIElementButton activateAlarmAreaButton;
    [SerializeField] private UIElementButton setAlarmAreaButton;
    [SerializeField] private UIElementButton deactivateAlarmAreaButton;
    [Space]
    [SerializeField] private AlarmReferences clock;
    [SerializeField] private AlarmReferences clockAlarm;
    [Space]
    [SerializeField] private AlarmInfo info;

    private bool _setupAlarm;

    private AudioSource _audioSource;

    private void UpdateClockScreensActivation()
    {
        App.GetSystem<ScreenSystem>().SetScreen(_setupAlarm ? alarmScreenIndex : clockScreenIndex);
    }

    protected override void OnStart()
    {
        _audioSource = GetComponent<AudioSource>();

        clock.Clock.OnUpdateSeconds += OnUpdateSeconds;

        activateAlarmAreaButton.onPress   += OnPress;
        setAlarmAreaButton.onPress        += OnPress;
        deactivateAlarmAreaButton.onPress += OnPress;

        UpdateClockScreensActivation();
    }

    protected override void OnFinal()
    {
        clock.Clock.OnUpdateSeconds -= OnUpdateSeconds;

        activateAlarmAreaButton.onPress   -= OnPress;
        setAlarmAreaButton.onPress        -= OnPress;
        deactivateAlarmAreaButton.onPress -= OnPress;
    }

    private void OnUpdateSeconds(int _)
    {
        if (alarm.installed)
        {
            if (alarm.MatchTime(clock.Clock))
            {
                _audioSource.PlayOneShot(alarmSound);

                App.GetSystem<NotifySystem>().ShowNotify(
                    $"Alarm!\nTime : {ClockUtils.ConvertTimeToText(clock.Clock.Hours)}:{ClockUtils.ConvertTimeToText(clock.Clock.Minutes)}",
                    () => { _audioSource.Stop(); });

                alarm.installed = false;
            }
        }
    }

    private void OnPress(UIElementButton button, bool press)
    {
        if (button == activateAlarmAreaButton)
        {
            if (press)
                _setupAlarm = true;
        }
        else if (button == setAlarmAreaButton)
        {
            if (press)
            {
                _setupAlarm = false;

                alarm = new AlarmData
                {
                    dayOfWeek    = clockAlarm.Clock.DayOfWeek,
                    day          = clockAlarm.Clock.Day,
                    month        = clockAlarm.Clock.Month,
                    year         = clockAlarm.Clock.Year,
                    totalSeconds = (int)clockAlarm.Clock.TotalSeconds,
                    installed    = true
                };

                info.SetText($"{clockAlarm.Clock.DayOfWeek} : {ClockUtils.ConvertTimeToText(clockAlarm.Clock.Hours)}:{ClockUtils.ConvertTimeToText(clockAlarm.Clock.Minutes)}");
            }
                
        }
        else if (button == deactivateAlarmAreaButton)
        {
            if (press)
                _setupAlarm = false;
        }

        UpdateClockScreensActivation();

        if (_setupAlarm)
        {
            clockAlarm.Clock.SetSeconds(clock.Clock.TotalSeconds);
        }
    }
}
