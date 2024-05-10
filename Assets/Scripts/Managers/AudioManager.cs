using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] AudioSource saveSlotSound=null;

    public void PlaySaveSlotSound()
    {
        saveSlotSound.Play();
    }
}
