using System.Collections;
using UnityEngine;

public class EnviromentSoundModifier : MonoBehaviour
{
    public AudioSource audioSource;  // Fonte de áudio
    public AudioClip audioClip;      // Clipe de áudio que será tocado
    public float fadeDuration = 1.0f; // Duração do fade in/out

    private Coroutine currentCoroutine; // Para interromper a corrotina anterior

    float startVol;
    private void Start()
    {
        startVol = audioSource.volume;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (currentCoroutine != null) // Interrompe o fade anterior, se houver
            {
                StopCoroutine(currentCoroutine);
            }

            // Inicia o fade in
            currentCoroutine = StartCoroutine(FadeIn());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (currentCoroutine != null) // Interrompe o fade anterior, se houver
            {
                StopCoroutine(currentCoroutine);
            }

            // Inicia o fade out
            currentCoroutine = StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeIn()
    {
        audioSource.clip = audioClip;
        audioSource.Play();

        float startVolume = 0.0f;
        audioSource.volume = startVolume;

        // Aumenta o volume gradualmente
        while (audioSource.volume < startVol)
        {
            audioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }

        audioSource.volume = startVol; // Garante que o volume chegue ao máximo
    }

    private IEnumerator FadeOut()
    {
        float startVolume = audioSource.volume;

        // Diminui o volume gradualmente
        while (audioSource.volume > 0.0f)
        {
            audioSource.volume -= startVolume * (Time.deltaTime / fadeDuration);
            yield return null;
        }

        audioSource.Stop(); // Para o áudio quando o volume chega a zero
        audioSource.volume = startVolume; // Reseta o volume para o valor original
    }
}
