using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestScript : MonoBehaviour
{
    public string serverURL = "time.windows.com";
    public TMP_Text NetworkTimeText;

    private void Start()
    {
        GetNetworkTime();
    }

    public void GetNetworkTime()
    {
        using (NtpClient client = new NtpClient(serverURL))
        {
            DateTime dt = client.GetNetworkTime();

            string timeUTC = "Network Time(UTC): " + dt.ToString();

            var totalSeconds = TimeSpan.FromTicks(dt.Ticks).TotalSeconds;

            string time = "Network Time: " + dt.ToLocalTime().ToString();

            string server = $"Server: {serverURL}";

            NetworkTimeText.text = timeUTC + Environment.NewLine + time + Environment.NewLine + server;
        }
    }
}
