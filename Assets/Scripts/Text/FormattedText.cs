using TMPro;
using UnityEngine;

public abstract class FormattedText : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI formattedText = null;
    [SerializeField] protected string prefix;

    public string GetValue()
    {
        return formattedText.text;
    }

    protected abstract void FormatText();
}
