using UnityEngine;

public class PoliceOfficer : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] float speed;
    [SerializeField] OneButtonModal modal = null;
    [SerializeField] Animator animator = null;
    [SerializeField] AudioSource suspectInCustodyAudio = null;
    bool audioPlayed;

    private void Awake()
    {
        modal.Init(BackToMainMenu);
        modal.HideModal();

    }

    private void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        if(Vector3.Distance(transform.position, target.transform.position) < 0.001f)
        {
            if(!audioPlayed)
            {
                audioPlayed = true;
                suspectInCustodyAudio.Play();
            }
            modal.ShowModal();
            animator.enabled = false;

        }
    }

    void BackToMainMenu()
    {
        NavigationManager.LoadScene(Scenes.MAIN_MENU);
    }
}
