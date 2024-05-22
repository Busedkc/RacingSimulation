using UnityEngine;

public class PoliceCarController : Car
{
    [SerializeField] Camera policeCarCamera = null;
    [SerializeField] GameObject redOverlay = null;
    [SerializeField] Light light1 = null;
    [SerializeField] Light light2 = null;
    [SerializeField] AudioSource siren = null;
    [SerializeField] BoxCollider boxCollider = null;
    [SerializeField] float speed;
    [SerializeField] bool isStatic;
    [Range(1, 2000)] [SerializeField] float range;
    [SerializeField] AudioSource startPursuitAudio = null;
    [SerializeField] AudioSource backupAudio = null;
    [SerializeField] AudioSource lostSightAudio = null;
    [SerializeField] AudioSource stopTheVehicleAudio = null;
    enum PoliceCarState { IDLE, CHASE };
    PoliceCarState state;
    GameObject player;
    bool lightToggle;
    float policeCloseToPlayerTimer;
    bool hasPlayedEffect;
    bool hasPlayedSound;

    public override void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        gameObject.transform.LookAt(player.transform);
        policeCarCamera.gameObject.SetActive(false);
        redOverlay.SetActive(false);
        light1.gameObject.SetActive(false);
        light2.gameObject.SetActive(false);
        siren.gameObject.SetActive(false);
    }

    public override void Update()
    {
        base.Update();
        if (!isStatic)
        {
            gameObject.transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
        }
        else
        {
            InvokeRepeating("ToggleLights", 0f, 0.5f);
        }
        if (state == PoliceCarState.CHASE)
        {
            ChasePlayerCar();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player") && !hasPlayedEffect)
        {
            hasPlayedEffect = true;
            policeCarCamera.gameObject.SetActive(true);
            policeCarCamera.transform.LookAt(gameObject.transform);
            redOverlay.SetActive(true);
            Invoke("SwitchBackToMainCamera", 1.0f);
            InvokeRepeating("ToggleLights", 0f, 0.5f);
            startPursuitAudio.Play();
            Invoke("PlayBackupAudio", 4.0f);
        }
        else if (other.gameObject.tag.Equals("Player"))
        {
            state = PoliceCarState.CHASE;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            lostSightAudio.Play();
            state = PoliceCarState.IDLE;
        }
    }

    private void SwitchBackToMainCamera()
    {
        policeCarCamera.gameObject.SetActive(false);
        redOverlay.SetActive(false);
        state = PoliceCarState.CHASE;
        siren.gameObject.SetActive(true);
    }

    private void ToggleLights()
    {
        lightToggle = !lightToggle;
        light1.gameObject.SetActive(lightToggle);
        light2.gameObject.SetActive(!lightToggle);
    }

    private void ChasePlayerCar()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
        if (Vector3.Distance(transform.position, player.transform.position) < 6.0f)
        {
            if (!hasPlayedSound)
            {
                hasPlayedSound = true;
                stopTheVehicleAudio.Play();
            }
            policeCloseToPlayerTimer += Time.deltaTime;
        }
        else
        {
            hasPlayedSound = false;
            policeCloseToPlayerTimer = 0;
        }
        if (policeCloseToPlayerTimer > 3.0f)
        {
            NavigationManager.LoadScene(Scenes.ARRESTED);
        }
    }

    public void ScaleCollider()
    {
        boxCollider.transform.localScale = new Vector3(range, 10, range);
    }

    private void PlayBackupAudio()
    {
        backupAudio.Play();
    }

}