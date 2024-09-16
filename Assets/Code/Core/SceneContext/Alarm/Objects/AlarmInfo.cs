using TMPro;
using UnityEngine;

public class AlarmInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text title;

    public void SetText(string text)
    {
        title.text = text;
    }
}
