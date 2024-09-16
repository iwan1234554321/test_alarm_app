using Notteam.Tweener;
using UnityEngine;

public class DebugInvoker : MonoBehaviour
{
    [SerializeField] private bool debugTween;

    public void DebugInvoke(string message)
    {
        Debug.Log(message);

        if (debugTween)
        {
            gameObject.AddTween(new Tween("Debug Tween", 1,
            () =>
            {
                Debug.Log("ACTIVATE DEBUG TWEEN");
            },
            (t) =>
            {
                Debug.Log($"UPDATE DEBUG TWEEN : {t}");
            },
            () =>
            {
                Debug.Log("DEACTIVATE DEBUG TWEEN");
            }
            ));
        }
    }
}
