using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] EconomyManager economyManager;

    [Header("Timer")]
    public float TimeScale = 1f;
    public bool stop = false;

    [Space]

    [Header("Day Count")]
    public int dayCount = 0;
    public int dayDuration = 86400; // Duration of a day in seconds (24 hours * 60 minutes * 60 seconds)
    private int dayTimer = 0;

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

          
        }
    }

    void WeekCount()
    {
        if (dayCount >= weekDuration)
        {
            economyManager.UpdateComerceEconomyValues();
            weekCount++;
            dayCount = 0; // Reset day count for the new week
        }
    }

    void MonthCount()
    {
        if (monthTimer >= monthDuration)
        {
            monthCount++;
           
            monthTimer = 0;
        }
    }

    void YearCount()
    {
        if (monthCount >= yearDuration)
        {
            yearCount++;
            monthCount = 0; // Reset month count for the new year
        }
    }
}
