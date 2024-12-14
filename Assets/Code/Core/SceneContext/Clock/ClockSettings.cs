using UnityEngine;

[CreateAssetMenu(fileName = "ClockSettings", menuName = "Clock/Create Clock Settings", order = 0)]
public class ClockSettings : ScriptableObject
{
    [SerializeField] private int hourOfDay = 24;
    [SerializeField] private int minutesOfHour = 60;
    [SerializeField] private int secondsOfMinutes = 60;
    [Space]
    [SerializeField] private float          animationTime = 2.0f;
    [SerializeField] private AnimationCurve animationTweenCurve;

    public int HourOfDay        => hourOfDay;
    public int MinutesOfHour    => minutesOfHour;
    public int SecondsOfMinutes => secondsOfMinutes;

    public float          AnimationTime       => animationTime;
    public AnimationCurve AnimationTweenCurve => animationTweenCurve;
}
