using System;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance { get; private set; }

    private void Awake()
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

    [Header("Timer")]
    [Range(0f, 100f)]
    public float TimeScale = 1f;
    public bool stop = false;

    [Space]
    [Header("Day Count")]
    public int dayCount = 0;
    public int dayDuration = 86400; // Duration of a day in seconds (24 hours * 60 minutes * 60 seconds)
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

    void Start()
    {
        // Initialize lastTime to the current time
        lastTime = Time.time;
    }

    void Update()
    {
        Cont();
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
            dayCount++; monthTimer++;
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
}
