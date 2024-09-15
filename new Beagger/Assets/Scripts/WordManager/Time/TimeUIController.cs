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
        // Calcula os minutos e segundos a partir do temporizador de dias.
        int totalSeconds = timeController.dayTimer % timeController.dayDuration;
        int minutes = (totalSeconds / 60) % 60;
        int seconds = totalSeconds % 60;

        // Atualiza o texto na UI com os valores atuais
        timeText.text = $"Time: {minutes:00}:{seconds:00}";
        dayText.text = "Day: " + timeController.dayCount.ToString();
        weekText.text = "Week: " + timeController.weekCount.ToString();
        monthText.text = "Month: " + timeController.monthCount.ToString();
        yearText.text = "Year: " + timeController.yearCount.ToString();
    }
}
