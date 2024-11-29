using UnityEngine;
using System.Linq;

public class GeneralAudioManager : MonoBehaviour
{
    [SerializeField]ConfigurationScreenManager configuration;
   

    [Header("Audio Sources")]
    public AudioSource[] generalSources; // Array de fontes de áudio geral
    public AudioSource[] musicSources;   // Array de fontes de música
    public AudioSource[] sfxSources;     // Array de fontes de SFX


    public void SetMusicVolume(float volume)
    {
        foreach (AudioSource musicSource in musicSources)
        {
            if (musicSource != null)
            {
                musicSource.volume = volume;
            }
        }
    }

    public void SetSFXVolume(float volume)
    {
        foreach (AudioSource sfxSource in sfxSources)
        {
            if (sfxSource != null)
            {
                sfxSource.volume = volume;
            }
        }
    }

    public void SetMasterVolume(float volume)
    {
        foreach (AudioSource source in generalSources)
        {
            // Verifica se a fonte de áudio não pertence aos arrays de música ou SFX
            if (source != null && !musicSources.Contains(source) && !sfxSources.Contains(source))
            {
                source.volume = volume;
            }
        }
    }
    private void Start()
    {
        configuration.LoadSettings();
    }
}
