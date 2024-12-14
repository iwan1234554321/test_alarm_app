using Notteam.AppCore;
using System;

public class NotifySystem : AppSystem<NotifyView>
{
    public void Show(string message, string buttonMessage = null, Action completed = null)
    {
        SystemObjects[0].Display(message, buttonMessage, completed);
    }
}
