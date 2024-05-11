using System.Collections.Generic;
using UnityEngine;

public class FinishedRaceModal : OneButtonModal
{
    [SerializeField] Leaderboard finishedRaceModalLeaderboard = null;
    [SerializeField] GameObject confettiImageObject = null;
    [SerializeField] GameObject confettiParticlesObject = null;
    [SerializeField] GameObject trophyObject = null;
    [SerializeField] AudioSource congratulationsAudio = null;
    [SerializeField] AudioSource achievementAudio = null;

    public void UpdateFinalLeaderboard(List<CarController> listOfCarsInRace)
    {
        //finishedRaceModalLeaderboard.UpdateLeaderboard(listOfCarsInRace);
    }

     public void ShowFirstPlaceCelebration(bool shouldShow)
    {
        if (shouldShow)
        {
            confettiImageObject.SetActive(true);
            confettiParticlesObject.SetActive(true);
            trophyObject.SetActive(true);
            Invoke("StopTrophySpinning", 1.5f);
            if(achievementAudio != null)
            {
                achievementAudio.Play();
            }
        }else
        {
            confettiImageObject.SetActive(false);
            confettiParticlesObject.SetActive(false);
            trophyObject.SetActive(false);
        }
    }

    private void StopTrophySpinning()
    {
        Rotate360 rotate360Script = trophyObject.GetComponent<Rotate360>();
        rotate360Script.enabled = false;
        if(congratulationsAudio != null)
        {
            congratulationsAudio.Play();
        }
    }
}
