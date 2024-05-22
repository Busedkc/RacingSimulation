using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroSequence : MonoBehaviour
{
    [SerializeField] Image introImage = null;
    [SerializeField] List<Camera> regularRaceCameras = null;
    [SerializeField] Animator pulseAnimator = null;
    [SerializeField] AudioSource introMusic = null;
    [SerializeField] Camera introCamera = null;
    [SerializeField] GameObject rotationObject = null;
    [SerializeField] float introCameraMoveSpeed;
    [SerializeField] AudioSource engineRevvingSound = null;
    [SerializeField] GameObject regularRacingHUD = null;
    [SerializeField] GameObject countdownObject = null;
    [SerializeField] GameObject startRaceObject = null;
    [SerializeField] AudioSource startAudio = null;

    private void Start()
    {
        foreach(Camera raceCamera in regularRaceCameras)
        {
            raceCamera.gameObject.SetActive(false);
        }
        InvokeRepeating("ChangeIntroImageToRandomColor", 0f, 0.2f);
        Invoke("HideIntroImage", 1.5f);
        InvokeRepeating("MoveIntroCameraForward", 1.5f, 0.01f);
        Invoke("SwitchToRaceCamera", 7.0f);
        Invoke("StartRace", 13.5f);
        Invoke("HideStartMessage", 14.5f);
    }

    void SwitchToRaceCamera()
    {
        //regularRaceCameras[GameManager.GetCurrentActiveCarIndex()].gameObject.SetActive(true);
        regularRaceCameras[0].gameObject.SetActive(true);
        introCamera.gameObject.SetActive(false);
        introMusic.Stop();
        regularRacingHUD.SetActive(true);
        engineRevvingSound.Play();
        pulseAnimator.enabled = false;
        countdownObject.SetActive(true);
    }

    void StartRace()
    {
        countdownObject.SetActive(false);
        startRaceObject.SetActive(true);
        startAudio.Play();
    }

    void HideStartMessage()
    {
        startRaceObject.SetActive(false);
        CarFactory.EnableAICars();
        Events.RaceStarted?.Invoke();
    }

    void ChangeIntroImageToRandomColor()
    {
        Color randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        introImage.color = randomColor;
    }

    void HideIntroImage()
    {
        introImage.gameObject.SetActive(false);
    }

    void MoveIntroCameraForward()
    {
        introCamera.gameObject.transform.position -= rotationObject.transform.forward * Time.deltaTime * introCameraMoveSpeed;
    }
}
