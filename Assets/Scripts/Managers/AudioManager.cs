using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource saveSlotSound = null;
    [SerializeField] AudioSource backButtonSound = null;
    [SerializeField] AudioSource okButtonSound = null;
    [SerializeField] AudioSource chaChingButtonSound = null;
    [SerializeField] AudioSource rightAndLeftButtonSound = null;
    [SerializeField] AudioSource cancelButtonSound = null;
    public void PlaySaveSlotSound()
    { 
        saveSlotSound.Play();
    }

    public void PlayBackButtonSound()
    { 
        backButtonSound.Play();
    }

    public void PlayOkButtonSound()
    { 
        okButtonSound.Play();
    }

    public void PlayChaChingButtonSound()
    { 
        chaChingButtonSound.Play();
    }

    public void PlayRightAndLeftButtonSound()
    { 
        rightAndLeftButtonSound.Play();
    }

    public void PlayCancelButtonSound()
    {
        cancelButtonSound.Play();
    }
}
