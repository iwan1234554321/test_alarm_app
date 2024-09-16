using Notteam.AppCore;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ClockTimeWebSetterSystem : AppSystem
{
    [SerializeField] private int      syncAfterSeconds = 3600;
    [SerializeField] private string[] urlServices;

    private List<Clock> _clocks;

    private int   _lastSecondsAfterSync;
    private float _currentSeconds;

    private DateTime GetTimeFromRandomService()
    {
        var random = UnityEngine.Random.Range(0, urlServices.Length - 1);

        var currentService = urlServices[random];

        using (NtpClient client = new NtpClient(currentService))
        {
            DateTime dateTime = client.GetNetworkTime();

            return dateTime.ToLocalTime();
        }
    }

    private void UpdateTime()
    {
        var lastDateTime = GetTimeFromRandomService();

        _lastSecondsAfterSync = ClockUtils.GetDateTimeTotalSecondsOfDay(lastDateTime);

        foreach (var clock in _clocks)
            clock.SetDate(lastDateTime);
    }

    protected override void OnStart()
    {
        _clocks = App.GetSystem<ClockUpdater>().SystemObjects;

        UpdateTime();
    }

    protected override void OnUpdate()
    {
        if (_currentSeconds >= (_lastSecondsAfterSync + syncAfterSeconds))
            UpdateTime();
        else
            _currentSeconds += 1.0f * Time.deltaTime;
    }
}
