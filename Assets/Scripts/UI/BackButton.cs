using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    [SerializeField] Button backButton = null;

    private void Start()
    {
        backButton.onClick.AddListener(HandleBackButtonPressed);
    }

    private void OnDestroy()
    {
        backButton.onClick.RemoveListener(HandleBackButtonPressed);
    }

    private void HandleBackButtonPressed()
    {
        Events.BackButtonPressed?.Invoke();
    }
}
