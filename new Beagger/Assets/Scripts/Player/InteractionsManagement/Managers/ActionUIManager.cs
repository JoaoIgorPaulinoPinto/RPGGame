using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionUIManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI txt;

    private void Start()
    {
        slider.gameObject.SetActive(false); // Esconde o slider até a coleta começar
    }

    public void StartProgress(float duration, string msg )
    {
        slider.maxValue = duration;
        slider.value = 0;
        slider.gameObject.SetActive(true); // Mostra o slider quando a coleta começa
        StartCoroutine(UpdateSlider(duration));
        txt.text = msg;
    }

    public void StopProgress()
    {
        StopAllCoroutines();
        slider.value = 0;
        slider.gameObject.SetActive(false); // Esconde o slider quando a coleta é cancelada
    }

    private IEnumerator UpdateSlider(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            slider.value = elapsedTime;
            yield return null;
        }

        // Oculta o slider ao terminar
        slider.gameObject.SetActive(false);
    }
}
