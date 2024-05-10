using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StartMenuController : MonoBehaviour
{
    [SerializeField] Button newGameButton = null;
    [SerializeField] Button loadGameButton = null;
    [SerializeField] EnterUsernameModal enterUsernameModal = null;
    [SerializeField] SaveSlotsModal saveSlotsModal = null;

    private void OnEnable()
    {
        newGameButton.onClick.AddListener(HandleNewGameButtonclicked);
        loadGameButton.onClick.AddListener(HandleLoadGameButtonClicked);
        Events.UsernameSubmitted += HandleUsernameWasSubmitted;
        //saveSlotsModal.SaveSlotSelected += HandleSaveSlotWasSelected;
    }

    private void OnDisable()
    {
        newGameButton.onClick.RemoveListener(HandleNewGameButtonclicked);
        loadGameButton.onClick.RemoveListener(HandleLoadGameButtonClicked);
        Events.UsernameSubmitted -= HandleUsernameWasSubmitted;
        //saveSlotsModal.SaveSlotSelected -= HandleSaveSlotWasSelected;


    }

    void HandleNewGameButtonclicked()
    {
        //saveSlotsModal.SetIsNewGame(true);
        //ShowModal();
    }

    void HandleLoadGameButtonClicked()
    {
        //saveSlotsModal.SetIsNewGame(false);
        //saveSlotsModal.ShowModal();
    }

    void HandleUsernameWasSubmitted(string username)
    {
        Dictionary<string, string> sceneData = new Dictionary<string, string>();
        NavigationManager.LoadScene(Scenes.CHOOSE_A_CAR, sceneData);
    }

    void HandleSaveSlotWasSelected(bool isNewGame, string index)
    {
        if (isNewGame)
        {
            //enterUsernameModal.ShowModal();
            //enterUsernameModal.Init(enterUsernameModal.HideModal);
        } else
        {
            Dictionary<string, string> sceneData = new Dictionary<string, string>();
            sceneData.Add("car_index", index);
            NavigationManager.LoadScene(Scenes.MAIN_MENU, sceneData);
        }
    }
}