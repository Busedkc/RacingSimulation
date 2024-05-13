using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TMPro;

public class SaveSlotsModal : Modal
{
    public Action<bool,string> SaveSlotSelected;

    [SerializeField] List<GameData> saveSlots = null;
    [SerializeField] List<Button> saveSlotButtons = null;
    [SerializeField] List<TextMeshProUGUI> saveSlotTexts = null;
    [SerializeField] TwoButtonModal warningModal = null;

    bool isNewGame;
    int index;

    protected override void OnEnable()
    {
        base.OnEnable();
        //warningModal.Init(HandleOverwriteGameData,warningModal.HideModal);
        //for(int i=0;i<saveSlotButtons.Count;i++)
        //{
        //    int index=i;
        //    saveSlotButtons[i].onClick.AddListener(()=> {HandleSaveSlotsSelected(index);});
        //    bool isSaveSlotEmpty=string.IsNullOrEmpty(saveSlots[i].username);
        //    if(!isSaveSlotEmpty)
        //    {
        //        ShowSaveSlotInfo();
        //    }
        //} 
        
        if(isNewGame)
        {
            for(int i=0;i<saveSlots.Count;i++)
            {
                saveSlotButtons[i].interactable=true;
            } 
        } else
        {
                DisableEmptySaveSlots();
        }
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        for(int i=0; i<saveSlotButtons.Count; i++)
        {
            saveSlotButtons[i].onClick.RemoveAllListeners();
        }
    }

    public void SetIsNewGame(bool isNewGame)
    {
        this.isNewGame = isNewGame;
    }
    
    void HandleSaveSlotsSelected(int index)
    {
        Events.SaveSlotClicked?.Invoke();
        this.index = index;
        /*if(isNewGame && !string.IsNullOrEmpty(saveSlots[index].username))
        {
            warningModal.ShowModal();
        } else
        {
            HandleOverwriteGameData();
        }*/
    }

    void HandleOverwriteGameData()
    {
        SetSaveSlot();
        SaveSlotSelected?.Invoke(isNewGame, index.ToString());
    }

    void SetSaveSlot()
    {
        /*GameManager.gameData=saveSlots[index];
        warningModal.HideModal();
        HideModal(); */
    }

    void DisableEmptySaveSlots()
    {
        /*for(int i=0; i<saveSlots.Count; i++)
        {
            if(string.IsNullOrEmpty(saveSlots[i].username))
            {
                saveSlotButtons[i].interactable=false;
            }
        }*/
    }

    void ShowSaveSlotInfo()
    {
        /*string text="";
        text+=saveSlots[index].username+"\n";
        text+="<sup>";
        TimeSpan time=TimeSpan.FromSeconds(saveSlots[index].playTime);
        text+="Time:" + new TimeSpan(time.Hours, time.Minutes, time.Seconds)+" | ";
        text+=GameManager.GetMoneyAsString()+ " | ";
        text+="Last Played: "+new DateTime(saveSlots[index].lastPlayed);
        text+="</sup>";
        saveSlotTexts[index].SetText(text); */
    }
}
