using Notteam.AppCore;
using UnityEngine;

[RequireComponent(typeof(Clock))]
public abstract class ClockViewBase : AppSystemObject
{
    protected Clock clock;

    protected override void OnStart()
    {
        clock = GetComponent<Clock>();

        clock.OnUpdateTime += UpdateView;
    }

    protected override void OnFinal()
    {
        clock.OnUpdateTime -= UpdateView;
    }

    protected virtual void UpdateView() { }
}
