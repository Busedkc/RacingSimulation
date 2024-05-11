using UnityEngine;
using TMPro;

public class EnterUsernameModal : OneButtonModal
{
    [SerializeField] TMP_InputField inputField = null;

    protected override void HandleButtonClicked()
    {
        base.HandleButtonClicked();
        string username = inputField.text;
        Events.UsernameSubmitted?.Invoke(username);
    }
}
