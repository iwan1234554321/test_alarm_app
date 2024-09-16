using System;
using TMPro;
using UnityEngine;

public class AlarmView : ClockViewBase
{
    [Header("UI References")]
    [SerializeField] private TMP_InputField hoursTextTitle;
    [SerializeField] private TMP_InputField minutesTextTitle;
    [SerializeField] private TMP_Text       dateTextTitle;

    public bool EnableView { get; set; } = true;

    protected override void OnStart()
    {
        base.OnStart();

        clock.OnSetDate    += Clock_OnSetDate;
        clock.OnSetSeconds += Clock_OnSetSeconds;

        hoursTextTitle.onValueChanged.AddListener(SetSecondsByText);
        minutesTextTitle.onValueChanged.AddListener(SetSecondsByText);
    }

    private void Clock_OnSetSeconds(int _)
    {
        UpdateTextsValues(clock.Hours, clock.Minutes, clock.Day, clock.Month, clock.Year, clock.DayOfWeek, clock.DayPeriod);
    }

    protected override void OnFinal()
    {
        base.OnFinal();

        clock.OnSetDate    -= Clock_OnSetDate;
        clock.OnSetSeconds -= Clock_OnSetSeconds;

        hoursTextTitle.onValueChanged.RemoveListener(SetSecondsByText);
        minutesTextTitle.onValueChanged.RemoveListener(SetSecondsByText);
    }

    private void UpdateTextsValues(int hours, int minutes, int day, int month, int year, DayOfWeek dayOfWeek, ClockDayPeriod dayPeriod)
    {
        if (hoursTextTitle && minutesTextTitle && dateTextTitle)
        {
            var hoursText = ClockUtils.ConvertTimeToText(hours);
            var minutesText = ClockUtils.ConvertTimeToText(minutes);
            //var secondsText = ClockUtils.ConvertTimeToText(_seconds);

            var dayText   = ClockUtils.ConvertTimeToText(day);
            var monthText = ClockUtils.ConvertTimeToText(month);
            var yearText  = ClockUtils.ConvertTimeToText(year);

            hoursTextTitle.text   = $"{hoursText}";
            minutesTextTitle.text = $"{minutesText}";
            dateTextTitle.text    = $"{dayOfWeek} {dayText}.{monthText}.{yearText} {dayPeriod}";
        }
    }

    private void Clock_OnSetDate(DateTime _)
    {
        UpdateTextsValues(clock.Hours, clock.Minutes, clock.Day, clock.Month, clock.Year, clock.DayOfWeek, clock.DayPeriod);

        EnableView = false;
    }

    protected override void UpdateView()
    {
        if (hoursTextTitle && minutesTextTitle && dateTextTitle)
        {
            if (EnableView)
                UpdateTextsValues(clock.Hours, clock.Minutes, clock.Day, clock.Month, clock.Year, clock.DayOfWeek, clock.DayPeriod);
        }
    }

    public void SetSecondsByText(string argument)
    {
        if (clock)
            clock.SetSeconds(ClockUtils.GetTotalSeconds(int.Parse(hoursTextTitle.text), int.Parse(minutesTextTitle.text), 0));
    }
}
