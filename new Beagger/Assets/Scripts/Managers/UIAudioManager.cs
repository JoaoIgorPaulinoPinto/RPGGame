using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip openClip;
    public AudioClip closeClip;

    public void PlayOpenSound()
    {
        audioSource.clip = openClip;
        audioSource.Play();
    }
}
