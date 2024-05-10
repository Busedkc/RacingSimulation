using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class NavigationManager : Singleton<NavigationManager>
{
    static Stack<string> navigationStack;
    public static Dictionary<string, string> SceneData;
    private static Stack<Dictionary<string, string>> sceneDataStack;

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        if(navigationStack == null)
        {
            navigationStack = new Stack<string>();
        }
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        if(SceneData == null)
        {
            SceneData = new Dictionary<string, string>();
        }
        if(sceneDataStack == null)
        {
            sceneDataStack = new Stack<Dictionary<string, string>>();
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Events.BackButtonPressed -= HandleBackButtonPressed;
        Events.BackButtonPressed += HandleBackButtonPressed;
    }

    private void HandleBackButtonPressed()
    {
        navigationStack.Pop();
        sceneDataStack.Pop();
        LoadScene(navigationStack.Peek(), sceneDataStack.Peek(), false);

    }

    public static void LoadScene(string sceneName, Dictionary<string, string> sceneData = null, bool addToNavigationStack = true)
    {

        SetSceneData(sceneData);
        if(addToNavigationStack)
        {
            Dictionary<string, string> priorSceneData = new Dictionary<string, string>();
            foreach(string key in SceneData.Keys)
            {
                priorSceneData.Add(key, SceneData[key]);
            }
            sceneDataStack.Push(priorSceneData);
            navigationStack.Push(sceneName);

        }
        SceneManager.LoadScene(sceneName);
    }
    private static void SetSceneData(Dictionary<string, string> sceneData)
    {
        SceneData.Clear();
        if(sceneData != null)
        {
            foreach(string key in sceneData.Keys)
            {
                SceneData.Add(key, sceneData[key]);
            }
        }
    }
}
