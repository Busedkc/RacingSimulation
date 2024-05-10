using UnityEngine;
using TMPro;

public class UserDataPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI username = null;
    [SerializeField] TextMeshProUGUI money = null;

    private void Start()
    {
        //username.SetText(GameManager.GetUsername());
        //money.SetText(GameManager.GetMoneyAsString());
    }
}
