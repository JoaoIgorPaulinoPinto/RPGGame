using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAudioManager : MonoBehaviour
{
    public AudioClip audioClip; 
    public AudioSource audioSource;

    public void PlaySound()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
