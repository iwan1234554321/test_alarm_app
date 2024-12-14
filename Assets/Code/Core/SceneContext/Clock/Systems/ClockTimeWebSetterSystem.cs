using Notteam.AppCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class TimeData
{
    public double time;
}

public class ClockTimeWebSetterSystem : AppSystem
{
    [Space]
    [SerializeField] private bool     getFromSocket;
    [SerializeField] private int      syncAfterSeconds = 3600;
    [Header("Socket Settings")]
    [SerializeField] private string[] urlServices;
    [Header("WebRequest Settings")]
    [SerializeField] private string   urlJsonScript;
    [Tooltip("Proxy for bypassing CORS rules when working with web requests in the browser")]
    [SerializeField] private string   proxy;

    private bool     _isUpdateTime;

    private DateTime _dateTime;

    private List<Clock> _clocks;

    private int   _lastSecondsAfterSync;
    private float _currentSeconds;

    public event Action OnTimeUpdated;

    private IEnumerator GetWebRequestTime(Action succesResult = null)
    {
        using (var request = UnityWebRequest.Get(proxy + urlJsonScript))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);

                App.GetSystem<NotifySystem>().Show("Unable to synchronize time with server", "Ok");
            }
            else
            {
                var json = JsonUtility.FromJson<TimeData>(request.downloadHandler.text);

                _dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds((long)json.time);

                succesResult?.Invoke();

                Debug.Log($"Hour : {_dateTime.ToLocalTime().Hour} Minute : {_dateTime.ToLocalTime().Minute} Seconds : {_dateTime.ToLocalTime().Second}");
            }
        }
    }

    private void GetTimeFromRandomService()
    {
        var random = UnityEngine.Random.Range(0, urlServices.Length - 1);

        var currentService = urlServices[random];

        using (NtpClient client = new NtpClient(currentService))
        {
            _dateTime = client.GetNetworkTime();
        }
    }

    private void SetTime()
    {
        var lastDateTime = _dateTime.ToLocalTime();

        _lastSecondsAfterSync = ClockUtils.GetDateTimeTotalSecondsOfDay(lastDateTime);

        _currentSeconds = _lastSecondsAfterSync;

        foreach (var clock in _clocks)
            clock.SetDate(lastDateTime);

        _isUpdateTime = false;

        OnTimeUpdated?.Invoke();
    }

    private void UpdateTime()
    {
        if (getFromSocket)
        {
            GetTimeFromRandomService();

            SetTime();
        }
        else
            StartCoroutine(GetWebRequestTime(SetTime));
    }

    protected override void OnStart()
    {
        _clocks = App.GetSystem<ClockUpdater>().SystemObjects;

        UpdateTime();
    }

    protected override void OnUpdate()
    {
        if (_currentSeconds > (_lastSecondsAfterSync + syncAfterSeconds))
        {
            if (!_isUpdateTime)
            {
                UpdateTime();

                _isUpdateTime = true;
            }
        }
        else
            _currentSeconds += 1.0f * Time.deltaTime;
    }

    public void SetProxy(string url) => proxy = url;

    public void RequestTimeByProxy()
    {
        StartCoroutine(GetWebRequestTime(SetTime));
    }
}
