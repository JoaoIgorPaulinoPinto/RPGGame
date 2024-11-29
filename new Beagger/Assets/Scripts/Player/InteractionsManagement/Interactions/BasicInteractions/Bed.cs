using System.Collections;
using UnityEngine;

public class Bed : InteractableGameObject, IInteractable
{
    [SerializeField] float sleepingUntil = 7; // Hora para acordar (exemplo: 7h da manhã)
    [SerializeField] AudioListener listener;

    // Adiciona uma referência ao script de configurações para pegar o volume salvo
    [SerializeField] ConfigurationScreenManager configManager;

    public void Interact()
    {
        StartCoroutine(IESleep());
    }

    IEnumerator IESleep()
    {
        // Salva o volume atual antes de diminuir para o sono
        float savedVolume = AudioListener.volume;

        // Começa o processo de sono
        GeneralUIManager.Instance.SetAnimatioState("Sleep", true);
        PlayerControlsManager.Instance.realease = false;

        // Diminui o volume do AudioListener para 0 gradativamente
        yield return StartCoroutine(FadeAudioListenerVolume(savedVolume, 0f, 1f));

        // Simula o tempo de sono
        yield return new WaitForSeconds(3);

        // Avança para o próximo dia
        TimeController.Instance.PassDay();

        int wkeTime = (int)(((sleepingUntil + TimeController.Instance.dayCount * 24f) % 24f) * TimeController.Instance.dayDuration / 24f);

        // Calcula o valor correto para dayTimer usando a fórmula inversa
        TimeController.Instance.dayTimer = wkeTime;

        // Finaliza o dia e restaura o estado normal
        GeneralUIManager.Instance.SetAnimatioState("Sleep", false);
        yield return StartCoroutine(FadeAudioListenerVolume(0f, savedVolume, 1f));  // Restaura o volume

        // Notifica o jogador
        PopUpSystem.Instance.SendMsg("Você acordou", MessageType.Information, null);
        PlayerControlsManager.Instance.realease = true;
    }

    IEnumerator FadeAudioListenerVolume(float startVolume, float endVolume, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float newVolume = Mathf.Lerp(startVolume, endVolume, elapsed / duration);
            AudioListener.volume = newVolume;
            yield return null;
        }
        AudioListener.volume = endVolume;
    }
}
