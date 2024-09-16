using Notteam.AppCore;
using System;
using UnityEngine;

public class Clock : AppSystemObject
{
    [Header("Control")]
    [SerializeField] private float timeSpeed = 1.0f;
    [SerializeField] private float totalSeconds;
    [Space]
    [SerializeField] private DayOfWeek dayOfWeek;
    [SerializeField] private int       day, month, year, hours, minutes, seconds;

    [Space]
    [SerializeField] private ClockSettings settings;

    private int _maxDaysOfMonth;
    private int _maxSecondsOfDay;
    private int _maxSecondsOfMinutes;

    private float _hoursTime;
    private float _minutesTime;
    private float _secondsTime;

    private ClockDayPeriod _dayPeriod;

    private int _changedTotalSeconds;

    public DayOfWeek      DayOfWeek => dayOfWeek;
    public int            Day       => day;
    public int            Month     => month;
    public int            Year      => year;
    public int            Hours     => hours;
    public int            Minutes   => minutes;
    public int            Seconds   => seconds;
    public ClockDayPeriod DayPeriod => _dayPeriod;

    public int HourOfDay => settings.HourOfDay;
    public int MinutesOfHour => settings.MinutesOfHour;
    public int SecondsOfMinutes => settings.SecondsOfMinutes;

    public float TotalSeconds => totalSeconds;
    public int   MaxSecondsOfDay => _maxSecondsOfDay;
    public int   MaxSecondsOfMinutes => _maxSecondsOfMinutes;

    public bool  GrabbedHandles { get; set; }

    public event Action<int>      OnSetSeconds;
    public event Action<DateTime> OnSetDate;
    public event Action<int>      OnUpdateSeconds;
    public event Action           OnUpdateTime;

    protected override void OnStart()
    {
        totalSeconds = ClockUtils.GetTotalSeconds(hours, minutes, seconds);
    }

    public void AddDay()
    {
        if ((int)dayOfWeek < 6)
            dayOfWeek += 1;
        else
            dayOfWeek = 0;

        if (day < _maxDaysOfMonth)
            day += 1;
        else
            day = 0;

        Debug.Log("Add Day");
    }

    public void RemoveDay()
    {
        if ((int)dayOfWeek > 0)
            dayOfWeek -= 1;
        else
            dayOfWeek = (DayOfWeek)6;

        if (day > 0)
            day -= 1;
        else
            day = _maxDaysOfMonth;

        Debug.Log("Remove Day");
    }

    private void AddOrRemoveDayByTimeOutOfRange(float minTime, float maxTime)
    {
        if (totalSeconds > maxTime)
        {
            if (!GrabbedHandles)
                AddDay();

            totalSeconds = minTime;
        }
        else if (totalSeconds < minTime)
        {
            if (!GrabbedHandles)
                RemoveDay();

            totalSeconds = maxTime;
        }
    }

    private void UpdateTimeValues()
    {
        _hoursTime   = (totalSeconds / ClockUtils.SecondsInHour) % HourOfDay;
        _minutesTime = (totalSeconds / SecondsOfMinutes) % MinutesOfHour;
        _secondsTime = totalSeconds % SecondsOfMinutes;

        _maxSecondsOfDay     = HourOfDay * ClockUtils.SecondsInHour;
        _maxSecondsOfMinutes = MinutesOfHour * SecondsOfMinutes;

        hours   = Mathf.FloorToInt(_hoursTime);
        minutes = Mathf.FloorToInt(_minutesTime);
        seconds = Mathf.FloorToInt(_secondsTime);

        _dayPeriod = ClockUtils.GetDayPeriod(hours);
    }

    protected override void OnUpdate()
    {
        totalSeconds += timeSpeed * Time.deltaTime;

        AddOrRemoveDayByTimeOutOfRange(0, _maxSecondsOfDay);

        UpdateTimeValues();

        var intTotalSeconds = Mathf.FloorToInt(totalSeconds);

        if (_changedTotalSeconds != intTotalSeconds)
        {
            OnUpdateTime?.Invoke();

            OnUpdateSeconds?.Invoke(intTotalSeconds);

            _changedTotalSeconds = intTotalSeconds;
        }
    }

    public void SetSeconds(float seconds)
    {
        totalSeconds = seconds;

        UpdateTimeValues();

        OnSetSeconds?.Invoke((int)seconds);
    }

    public void SetDate(DateTime dateTime)
    {
        day   = dateTime.Day;
        month = dateTime.Month;
        year  = dateTime.Year;

        _maxDaysOfMonth = DateTime.DaysInMonth(year, month);

        dayOfWeek = dateTime.DayOfWeek;

        totalSeconds = ClockUtils.GetDateTimeTotalSecondsOfDay(dateTime.ToLocalTime());

        UpdateTimeValues();

        OnSetDate?.Invoke(dateTime);
    }
}
