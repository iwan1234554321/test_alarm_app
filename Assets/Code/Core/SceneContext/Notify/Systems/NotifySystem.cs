using Notteam.AppCore;
using Notteam.UIExtensions;
using System;
using TMPro;
using UnityEngine;

public class NotifySystem : AppSystem
{
    [SerializeField] private int             screenNotifyIndex;
    [SerializeField] private TMP_Text        notifyMessage;
    [SerializeField] private UIElementButton closeNotifyButton;

    private event Action OnComplete;

    protected override void OnStart()
    {
        closeNotifyButton.onPress += CloseNotifyButton_onPress;
    }

    protected override void OnFinal()
    {
        closeNotifyButton.onPress -= CloseNotifyButton_onPress;
    }

    private void CloseNotifyButton_onPress(UIElementButton button, bool press)
    {
        if (press)
        {
            Debug.Log("CLOSE NOTIFY");

            App.GetSceneSystem<ScreenSystem>().SetPreviousScreen();

            OnComplete?.Invoke();

            OnComplete = null;
        }
    }

    public void ShowNotify(string message, Action completed = null)
    {
        notifyMessage.text = message;

        App.GetSceneSystem<ScreenSystem>().SetScreen(screenNotifyIndex);

        OnComplete += completed;
    }
}
