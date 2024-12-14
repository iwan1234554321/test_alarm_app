using Notteam.AppCore;
using UnityEngine;

public class ScreenSystem : AppSystem<Screen>
{
    [SerializeField] private int currentScreen;

    private int _previousScreen;

    private int _currentScreenChanged;

    private Screen _currentScreenObject;

    protected override bool SortByHierarchyOrder => true;

    protected override void OnStartAfterSystemObjects()
    {
        SetScreen(currentScreen);
        ChangeScreen();
    }

    protected override void OnUpdate()
    {
        if (currentScreen != _currentScreenChanged)
        {
            ChangeScreen();
            //SetScreen(currentScreen);

            _currentScreenChanged = currentScreen;
        }
    }

    private void DeactivateAllScreens()
    {
        foreach (var screen in SystemObjects)
            screen.Deactivate();
    }

    public void ChangeScreen()
    {
        var currentScreenObject = SystemObjects[currentScreen];

        if (_currentScreenObject != null)
        {
            if (!currentScreenObject.Additive)
                _currentScreenObject.Deactivate();
        }
        else
            DeactivateAllScreens();

        currentScreenObject.Activate();

        _currentScreenObject = currentScreenObject;
    }

    public void SetScreen(int index)
    {
        _previousScreen = currentScreen;

        if (index > SystemObjects.Count - 1)
            currentScreen = 0;
        else if (index < 0)
            currentScreen = SystemObjects.Count - 1;
        else
            currentScreen = index;
    }

    public void SetScreen(Screen screen)
    {
        var index = SystemObjects.IndexOf(screen);

        _previousScreen = currentScreen;

        currentScreen = index;
    }

    public void SetPreviousScreen()
    {
        SetScreen(_previousScreen);
    }
}
