using Notteam.AppCore;
using Notteam.UIExtensions;
using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Screen))]
public class NotifyView : AppSystemObject
{
    [SerializeField] private TMP_Text        textMessage;
    [SerializeField] private TMP_Text        textButton;
    [SerializeField] private UIElementButton buttonClose;

    private Screen _controlScreen;

    private event Action OnComplete;

    protected override void OnStart()
    {
        _controlScreen = GetComponent<Screen>();

        buttonClose.onPress += ButtonClose_OnPress;
    }

    protected override void OnFinal()
    {
        buttonClose.onPress -= ButtonClose_OnPress;
    }

    private void ButtonClose_OnPress(UIElementButton button, bool press)
    {
        if (press)
        {
            App.GetSystem<ScreenSystem>().SetPreviousScreen();

            OnComplete?.Invoke();

            OnComplete = null;
        }
    }

    public void Display(string message, string buttonMessage = null, Action completed = null)
    {
        textMessage.text = message;

        if (buttonMessage != null)
            textButton.text = buttonMessage;

        App.GetSystem<ScreenSystem>().SetScreen(_controlScreen);

        OnComplete += completed;
    }
}
