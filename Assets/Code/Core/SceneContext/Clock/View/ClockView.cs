using TMPro;
using UnityEngine;

public class ClockView : ClockViewBase
{
    [Header("UI References")]
    [SerializeField] private TMP_InputField hoursTextTitle;
    [SerializeField] private TMP_InputField minutesTextTitle;
    [SerializeField] private TMP_InputField secondsTextTitle;
    [SerializeField] private TMP_Text       dateTextTitle;

    private bool _isUpdatedSeconds;

    private void SetTotalSecondsByInputFields(string _)
    {
        if (!hoursTextTitle.interactable && !minutesTextTitle.interactable && !secondsTextTitle.interactable)
            return;

        _isUpdatedSeconds = true;

        var totalSecondsParsed = ClockUtils.GetTotalSeconds(int.Parse(hoursTextTitle.text), int.Parse(minutesTextTitle.text), int.Parse(secondsTextTitle.text));

        clock.SetSeconds(totalSecondsParsed);

        _isUpdatedSeconds = false;
    }

    protected override void OnStart()
    {
        base.OnStart();

        hoursTextTitle.onValueChanged.AddListener(SetTotalSecondsByInputFields);
        minutesTextTitle.onValueChanged.AddListener(SetTotalSecondsByInputFields);
        secondsTextTitle.onValueChanged.AddListener(SetTotalSecondsByInputFields);
    }

    protected override void OnFinal()
    {
        base.OnFinal();

        hoursTextTitle.onValueChanged.RemoveListener(SetTotalSecondsByInputFields);
        minutesTextTitle.onValueChanged.RemoveListener(SetTotalSecondsByInputFields);
        secondsTextTitle.onValueChanged.RemoveListener(SetTotalSecondsByInputFields);
    }

    protected override void UpdateView()
    {
        if (hoursTextTitle && minutesTextTitle && secondsTextTitle && dateTextTitle && !_isUpdatedSeconds)
        {
            var hoursText   = ClockUtils.ConvertTimeToText(clock.Hours);
            var minutesText = ClockUtils.ConvertTimeToText(clock.Minutes);
            var secondsText = ClockUtils.ConvertTimeToText(clock.Seconds);

            var dayText   = ClockUtils.ConvertTimeToText(clock.Day);
            var monthText = ClockUtils.ConvertTimeToText(clock.Month);
            var yearText  = ClockUtils.ConvertTimeToText(clock.Year);

            hoursTextTitle.text   = $"{hoursText}";
            minutesTextTitle.text = $"{minutesText}";
            secondsTextTitle.text = $"{secondsText}";
            dateTextTitle.text    = $"{clock.DayOfWeek} {dayText}.{monthText}.{yearText} {clock.DayPeriod}";
        }
    }

    public void SetInteractionInputFields(bool interactable)
    {
        hoursTextTitle.interactable   = interactable;
        minutesTextTitle.interactable = interactable;
        secondsTextTitle.interactable = interactable;
    }
}
