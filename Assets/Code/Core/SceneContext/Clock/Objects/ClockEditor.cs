using Notteam.AppCore;
using Notteam.UIExtensions;
using UnityEngine;

[RequireComponent(typeof(Clock))]
[RequireComponent(typeof(ClockHandles))]
[RequireComponent(typeof(ClockView))]
public class ClockEditor : AppSystemObject
{
    [SerializeField] private UIElementButton editButton;

    private bool _isEditMode;

    private Clock        _clock;
    private ClockHandles _clockHandles;
    private ClockView    _clockView;

    protected override void OnStart()
    {
        _clock        = GetComponent<Clock>();
        _clockHandles = GetComponent<ClockHandles>();
        _clockView    = GetComponent<ClockView>();

        editButton.onPress += EditButton_onPress;

        UpdateEditorState();
    }

    protected override void OnFinal()
    {
        editButton.onPress -= EditButton_onPress;
    }

    private void UpdateEditorState()
    {
        _clock.IsUpdateTime = !_isEditMode;
        _clockHandles.SetInteractable(_isEditMode);
        _clockView.SetInteractionInputFields(_isEditMode);
    }

    private void EditButton_onPress(UIElementButton button, bool press)
    {
        if (press)
        {
            _isEditMode = !_isEditMode;
        }

        UpdateEditorState();
    }
}
