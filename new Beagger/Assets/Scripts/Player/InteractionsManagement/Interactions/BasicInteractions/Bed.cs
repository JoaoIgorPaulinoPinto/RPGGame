using System.Collections;
using UnityEngine;

public class Bed : InteractableGameObject, IInteractable
{
    [SerializeField] float WakeTime = 7;
    [SerializeField]AudioListener listener;
    

    public void Interact()
    {
        print("Deitou");
        StartCoroutine(IESleep());
    }

    IEnumerator IESleep()
    {
        // Começa o processo de sono
        GeneralUIManager.Instance.SetAnimatioState("Sleep", true);
        PlayerControlsManager.Instance.realease = false;

        // Diminui o volume do AudioListener para 0 gradativamente
        yield return StartCoroutine(FadeAudioListenerVolume(1f, 0f, 1f));

        yield return new WaitForSeconds(3);

        int waktime = (int)(WakeTime * TimeController.Instance.dayDuration);
        TimeController.Instance.dayTimer = waktime / 24;

        // Finaliza o dia e mostra os ganhos do dia (exemplo de comentário)

        // Começa a restaurar o volume do AudioListener para o nível normal
        GeneralUIManager.Instance.SetAnimatioState("Sleep", false);
        yield return new WaitForSeconds(1);

        // Gradativamente aumenta o volume de volta ao normal
        yield return StartCoroutine(FadeAudioListenerVolume(0f, 1f, 1f));
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
