using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStts : MonoBehaviour
{
    

    [SerializeField]GameObject img;
    public bool alive = true;
    [Space]
    public Slider sliderHeath;

    public Slider sliderThirst;

    public Slider sliderHunger;
    // public Slider sliderHappy;
    [Space]

    public Transform playerBody;
    public string playerName;
    [Space]

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

        sttsDown();

        MayDead();
    }

    void UpdatePlayerMoney()
    {
        txtdinheiro.text = "R$" + money.ToString("00.00");
    }

    void UpdateSliders()
    {
        if (heath < 25)
        {
            img.SetActive(true);
        }
        else if (heath >= 25)
        {
            img.SetActive(false);
        }

        // Atualizando o valor do slider de saúde (não invertido)
        sliderHeath.value = heath;

        // Atualizando o valor do slider de sede (invertido)
        sliderThirst.value = thirst;

        // Atualizando o valor do slider de fome (invertido)
        sliderHunger.value = hunger;
    }

    void sttsDown()
    {
        // Diminuindo a sede e a fome
        thirst -= 0.25f * Time.deltaTime;
        hunger -= 0.25f * Time.deltaTime;

        // Garantindo que os valores de sede e fome fiquem entre 0 e 100
        thirst = Mathf.Clamp(thirst, 0, 100);
        hunger = Mathf.Clamp(hunger, 0, 100);
    }

    void MayDead()
    {
        if (thirst <= 0 || hunger <= 0)
        {
            heath -= 0.25f * Time.deltaTime;
        }

        // Garantindo que a saúde não fique abaixo de 0
        heath = Mathf.Clamp(heath, 0, 100);

        if (heath <= 0)
        {
            PlayerDied();
        }
    }

    public void PlayerDied()
    {
        playerBody.gameObject.SetActive(false);
        gameObject.SetActive(false);
        alive = false;
        GeneralReferences.Instance.DethScreenManager.Open();
    }
}
