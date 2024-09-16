using TMPro;
using UnityEngine;

public class ClockView : ClockViewBase
{
    [Header("UI References")]
    [SerializeField] private TMP_Text hoursTextTitle;
    [SerializeField] private TMP_Text minutesTextTitle;
    [SerializeField] private TMP_Text dateTextTitle;

    protected override void UpdateView()
    {
        if (hoursTextTitle && minutesTextTitle && dateTextTitle)
        {
            var hoursText = ClockUtils.ConvertTimeToText(clock.Hours);
            var minutesText = ClockUtils.ConvertTimeToText(clock.Minutes);
            //var secondsText = ClockUtils.ConvertTimeToText(_seconds);

            var dayText   = ClockUtils.ConvertTimeToText(clock.Day);
            var monthText = ClockUtils.ConvertTimeToText(clock.Month);
            var yearText  = ClockUtils.ConvertTimeToText(clock.Year);

            hoursTextTitle.text   = $"{hoursText}";
            minutesTextTitle.text = $"{minutesText}";
            dateTextTitle.text    = $"{clock.DayOfWeek} {dayText}.{monthText}.{yearText} {clock.DayPeriod}";

            //this.hoursText.text = $"{hoursText}:{minutesText}\n<size={55}>{dayOfWeek} {dayText}.{monthText}.{yearText} {_dayPeriod}</size>";
        }
    }
}
