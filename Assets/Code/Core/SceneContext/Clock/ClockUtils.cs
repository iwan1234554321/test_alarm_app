using System;
using UnityEngine;

public static class ClockUtils
{
    public const int SecondsInHour = 3600;

    public static string ConvertTimeToText(int time)
    {
        return time > 9 ? $"{time}" : $"0{time}";
    }

    public static float ConvertAngleToTime(float angle, int minTime, int maxTime, bool isHour = false)
    {
        var maxTimeTurnsCount = (maxTime / 12);

        var maxAngle = isHour ? 360 * maxTimeTurnsCount : 360;

        var angleTurnCount = (int)(Mathf.Abs(angle) / maxAngle);

        if (angle < 0.0f)
            angle += maxAngle * (angleTurnCount + 1);

        var normalizedAngle = Mathf.InverseLerp(0, maxAngle, angle % maxAngle);

        return Mathf.Lerp(minTime, maxTime, normalizedAngle);
    }

    public static float ConvertAngleToTimeUnclamped(float angle, int minTime, int maxTime, bool isHour = false)
    {
        var maxTimeTurnsCount = (maxTime / 12);

        var maxAngle = isHour ? 360 * maxTimeTurnsCount : 360;

        var normalizedAngle = (angle - 0.0f) / (maxAngle - 0.0f);

        return Mathf.LerpUnclamped(minTime, maxTime, normalizedAngle);
    }

    public static int GetTotalSeconds(int hours, int minutes, int seconds)
    {
        return (hours * 3600) + (minutes * 60) + seconds;
    }

    public static int GetDateTimeTotalSecondsOfDay(DateTime dateTime)
    {
        return GetTotalSeconds(dateTime.Hour, dateTime.Minute, dateTime.Second);
    }

    public static ClockDayPeriod GetDayPeriod(int hours)
    {
        return hours >= 12 ? ClockDayPeriod.PM : ClockDayPeriod.AM;
    }
}
