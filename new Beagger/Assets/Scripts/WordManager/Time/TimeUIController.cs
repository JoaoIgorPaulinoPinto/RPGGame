using TMPro;
using UnityEngine;

public class TimeUIController : MonoBehaviour
{
    [SerializeField] private TimeController timeController;

    [Header("UI Text Elements")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI weekText;
    public TextMeshProUGUI monthText;
    public TextMeshProUGUI yearText;

    void Update()
    {
        UpdateClockUI();
    }

    void UpdateClockUI()
    {
        // Calcula a hora atual do dia
        float currentHour = (timeController.dayCount * 24f + (timeController.dayTimer / (float)timeController.dayDuration) * 24f) % 24f;

        // Extrai as horas, minutos e segundos
        int hours = Mathf.FloorToInt(currentHour);
        int minutes = Mathf.FloorToInt((currentHour - hours) * 60);
       

        // Atualiza o texto na UI com os valores atuais
        timeText.text = $"Time: {hours:00}:{minutes:00}";
        dayText.text = "Day: " + timeController.dayCount.ToString();
        weekText.text = "Week: " + timeController.weekCount.ToString();
        monthText.text = "Month: " + timeController.monthCount.ToString();
        yearText.text = "Year: " + timeController.yearCount.ToString();
    }
}
