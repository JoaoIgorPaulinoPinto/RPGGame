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
      UpdateLightIntensity();
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
    public void PassDay()
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
        // Valores de exemplo:
        float currentHour = (dayCount * 24f + (dayTimer / (float)dayDuration) * 24f) % 24f;

        // Intervalos de transição
        float sunriseStart = sunriseStartHour;        // Início do nascer do sol (6:00)
        float sunriseEnd = sunriseStart + 2f;         // Fim do nascer do sol (8:00)
        float sunsetStart = sunsetStartHour - 2f;     // Início do pôr do sol (18:00)
        float sunsetEnd = sunsetStartHour;            // Fim do pôr do sol (20:00)

        float minIntensity = 0.07f;
        float maxIntensity = 0.6f;

        // Suaviza a intensidade durante o nascer do sol
        if (currentHour >= sunriseStart && currentHour <= sunriseEnd)
        {
            float progress = Mathf.InverseLerp(sunriseStart, sunriseEnd, currentHour);
            sun.intensity = Mathf.SmoothStep(minIntensity, maxIntensity, progress);
        }
        // Suaviza a intensidade durante o pôr do sol
        else if (currentHour >= sunsetStart && currentHour <= sunsetEnd)
        {
            float progress = Mathf.InverseLerp(sunsetStart, sunsetEnd, currentHour);
            sun.intensity = Mathf.SmoothStep(maxIntensity, minIntensity, progress);
        }
        // Manter a intensidade máxima durante o dia
        else if (currentHour > sunriseEnd && currentHour < sunsetStart)
        {
            sun.intensity = maxIntensity;
        }
        // Manter a intensidade mínima durante a noite
        else
        {
            sun.intensity = minIntensity;
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
