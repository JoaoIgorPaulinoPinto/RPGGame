using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStts : MonoBehaviour
{
    public Slider sliderHeath;

    public Slider sliderThirst;
   
    public Slider sliderHunger;
    // public Slider sliderHappy;

    public Transform playerBody;
    public string playerName;
    public float money;
    [Range(0, 100)]
    public float heath;
    [Range(0, 100)]
    public float thirst;
    [Range(0, 100)]
    public float hunger;
    [Range(0, 100)]
    public float happy;

    [SerializeField] private TextMeshProUGUI txtdinheiro;
    private static PlayerStts _instance;

    public static PlayerStts Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Instance of Inventory is being accessed before it's initialized.");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Update()
    {
        UpdatePlayerMoney();
        UpdateSliders();
    }

    void UpdatePlayerMoney()
    {



        txtdinheiro.text = "R$" + money.ToString("00.00");
    }

    void UpdateSliders()
    {
        // Atualizando o valor do slider de saúde (não invertido)
        sliderHeath.value = heath;

        // Atualizando o valor do slider de sede (invertido)
        sliderThirst.value = sliderThirst.maxValue - thirst;

        // Atualizando o valor do slider de fome (invertido)
        sliderHunger.value = sliderHunger.maxValue - hunger;
    }
}