using UnityEngine;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NavigationManager.LoadScene(Scenes.WELCOME);
    }

}
