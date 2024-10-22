using UnityEngine;

[CreateAssetMenu(fileName = "ClockSettings", menuName = "Clock/Create Clock Settings", order = 0)]
public class ClockSettings : ScriptableObject
{
    [SerializeField] private int hourOfDay = 24;
    [SerializeField] private int minutesOfHour = 60;
    [SerializeField] private int secondsOfMinutes = 60;

    public int HourOfDay        => hourOfDay;
    public int MinutesOfHour    => minutesOfHour;
    public int SecondsOfMinutes => secondsOfMinutes;
}
