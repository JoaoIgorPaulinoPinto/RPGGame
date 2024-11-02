using System;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI; // Adicione isso para trabalhar com UI

public class TimeController : MonoBehaviour
{
    [SerializeField] private Light2D sun; // Referência à luz
    [SerializeField] private float sunsetStartHour = 20f; // Hora de início do pôr do sol (20:00)
    [SerializeField] private float sunriseStartHour = 6f; // Hora de início do nascer do sol (6:00)

    public static TimeController Instance { get; private set; }

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI clockText; // Referência ao texto do relógio na UI

    [Header("Timer")]
    [Range(0f, 100f)]
    public float TimeScale = 1f;
    public bool stop = false;

    [Space]
    [Header("Day Count")]
    public int dayCount = 0;
    public int dayDuration; // Duration of a day in seconds (24 hours * 60 minutes * 60 seconds)
    public int dayTimer = 0;

    [Space]
    [Header("Week Count")]
    public int weekCount = 0;
    public int weekDuration = 7; // 7 days in a week

    [Space]
    [Header("Month Count")]
    public int monthCount = 0;
    public int monthDuration = 30; // Assume 30 days in a month for simplicity
    public int monthTimer = 0;

    [Space]
    [Header("Year Count")]
    public int yearCount = 0;
    public int yearDuration = 12; // 12 months in a year

    private float lastTime = 0f;

    // Eventos
    public event Action OnDayPassed;
    public event Action OnWeekPassed;
    public event Action OnMonthPassed;
    public event Action OnYearPassed;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destrói a nova instância se outra já existir
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        // Initialize lastTime to the current time
        lastTime = Time.time;
    }

    void Update()
    {
        Cont();
      //  UpdateLightIntensity();
        UpdateClockUI(); // Atualiza a UI do relógio
    }

    void Cont()
    {
        if (!stop)
        {
            Time.timeScale = TimeScale;
            float currentTime = Time.time;

            // Check if one second has passed
            if (Mathf.FloorToInt(currentTime) != Mathf.FloorToInt(lastTime))
            {
                dayTimer++;
                lastTime = currentTime; // Update lastTime to current time

                DayCount();
            }
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    void DayCount()
    {
        if (dayTimer >= dayDuration)
        {
            dayCount++;
            monthTimer++;
            dayTimer = 0; // Reset day timer

            WeekCount();
            MonthCount();
            YearCount();

            // Dispara o evento quando um dia termina
            OnDayPassed?.Invoke();
        }
    }

    void WeekCount()
    {
        if (dayCount >= weekDuration)
        {
            weekCount++;
            dayCount = 0; // Reset day count for the new week

            // Dispara o evento quando uma semana termina
            OnWeekPassed?.Invoke();
        }
    }

    void MonthCount()
    {
        if (monthTimer >= monthDuration)
        {
            monthCount++;
            monthTimer = 0;

            // Dispara o evento quando um mês termina
            OnMonthPassed?.Invoke();
        }
    }

    void YearCount()
    {
        if (monthCount >= yearDuration)
        {
            yearCount++;
            monthCount = 0; // Reset month count for the new year

            // Dispara o evento quando um ano termina
            OnYearPassed?.Invoke();
        }
    }

    private void UpdateLightIntensity()
    {
        // Calcula a hora atual do dia
        float currentHour = (dayCount * 24f + (dayTimer / (float)dayDuration) * 24f) % 24f;

        // Inicializa a intensidade da luz
        if (currentHour >= sunsetStartHour || currentHour < sunriseStartHour)
        {
            // Transição para o pôr do sol (20:00 a 6:00)
            if (currentHour >= sunsetStartHour && currentHour < 24f) // Pôr do sol
            {
                float sunsetProgress = (currentHour - sunsetStartHour) / (24f - sunsetStartHour); // Progresso do pôr do sol
                sun.intensity = Mathf.Lerp(1f, 0f, sunsetProgress); // Diminui a intensidade da luz
            }
            else // Nascer do sol
            {
                float sunriseProgress = (currentHour + 24f - sunriseStartHour) / (24f + (24f - sunriseStartHour)); // Progresso do nascer do sol
                sun.intensity = Mathf.Lerp(0f, 1f, sunriseProgress); // Aumenta a intensidade da luz
            }
        }
        else
        {
            // Durante o dia (6:00 a 20:00), mantenha a intensidade total
            sun.intensity = 1f;
        }
    }

    private void UpdateClockUI()
    {
        // Calcula a hora atual do dia
        float currentHour = (dayCount * 24f + (dayTimer / (float)dayDuration) * 24f) % 24f;
        int hours = Mathf.FloorToInt(currentHour);
        int minutes = Mathf.FloorToInt((currentHour - hours) * 60);

        // Atualiza o texto do relógio na UI
        clockText.text = string.Format("{0:D2}:{1:D2}", hours, minutes);
    }
}
