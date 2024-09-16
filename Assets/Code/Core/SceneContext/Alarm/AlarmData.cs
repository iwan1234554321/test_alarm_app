using System;
using UnityEngine;

[Serializable]
public struct AlarmData
{
    public DayOfWeek dayOfWeek;
    [Space]
    public int day;
    public int month;
    public int year;
    [Space]
    public int totalSeconds;
    [Space]
    public bool installed;

    public bool MatchTime(Clock clock)
    {
        if (clock.DayOfWeek == dayOfWeek &&
            clock.Day == day &&
            clock.Month == month &&
            clock.Year == year &&
            clock.TotalSeconds >= totalSeconds)
            return true;

        return false;
    }
}
