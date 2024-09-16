using Notteam.AppCore;
using Notteam.Tweener;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Screen : AppSystemObject
{
    [SerializeField] private bool  additive;
    [SerializeField] private float switchTime = 1.0f;

    private CanvasGroup _canvasGroup;

    public bool Additive => additive;

    protected override void OnStart()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Activate()
    {
        var startAlpha = 0.0f;

        gameObject.AddTween(new Tween("Switch Screen", switchTime, true,
            () =>
            {
                startAlpha = _canvasGroup.alpha;
            },
            (t) =>
            {
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, 1.0f, t);
            },
            () =>
            {
                _canvasGroup.blocksRaycasts = true;

                _canvasGroup.interactable   = true;
            }
            ));
    }

    public void Deactivate()
    {
        //gameObject.AddTween(new Tween("Deactivate Screen", switchTime));

        var startAlpha = 0.0f;

        gameObject.AddTween(new Tween("Switch Screen", switchTime, true,
            () =>
            {
                _canvasGroup.interactable = false;

                startAlpha = _canvasGroup.alpha;
            },
            (t) =>
            {
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, 0.0f, t);
            },
            () =>
            {
                _canvasGroup.blocksRaycasts = false;
            }
            ));
    }
}
