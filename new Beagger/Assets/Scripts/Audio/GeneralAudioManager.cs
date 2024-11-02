using UnityEngine;
using System.Linq;

public class GeneralAudioManager : MonoBehaviour
{
    [SerializeField]ConfigurationScreenManager configuration;
    public static GeneralAudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource[] generalSources; // Array de fontes de �udio geral
    public AudioSource[] musicSources;   // Array de fontes de m�sica
    public AudioSource[] sfxSources;     // Array de fontes de SFX

    private void Awake()
    {
        // Configura��o de Singleton para que haja apenas uma inst�ncia do GeneralAudioManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
            // Verifica se a fonte de �udio n�o pertence aos arrays de m�sica ou SFX
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
