using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{   
    [SerializeField] GameObject menuObject = null;
    [SerializeField] Button resumeButton = null;
     [SerializeField] Button restartButton = null;
     [SerializeField] Button quitButton = null;
     private void Awake()
     {
            Time.timeScale = 0;
     }

     private void Start()
     {
        resumeButton.onClick.AddListener(HandleResumeButtonClicked);
        restartButton.onClick.AddListener(HandleRestartButtonClicked);
        quitButton.onClick.AddListener(HandleQuitButtonClicked);

     }
    private void OnDestroy()
    {
        resumeButton.onClick.RemoveListener(HandleResumeButtonClicked);
        restartButton.onClick.RemoveListener(HandleRestartButtonClicked);
        quitButton.onClick.RemoveListener(HandleQuitButtonClicked);

     }
     
    void HandleResumeButtonClicked()
    {
        Time.timeScale = 1;
        menuObject.SetActive(false);
    }

    void HandleRestartButtonClicked()
    {
        int currentTrack = int.Parse(NavigationManager.SceneData["track"]);
        switch(currentTrack)
        {
            case 1:
                NavigationManager.LoadScene(Scenes.TRACK1);
                break;
            case 2:
                NavigationManager.LoadScene(Scenes.TRACK2);
                break;
            case 3:
                NavigationManager.LoadScene(Scenes.TRACK3);
                break;        

        }
     }
    void HandleQuitButtonClicked()
    {
        Dictionary<string, string> sceneData = new Dictionary<string, string>();
        NavigationManager.LoadScene(Scenes.MAIN_MENU, sceneData);
    }

}
