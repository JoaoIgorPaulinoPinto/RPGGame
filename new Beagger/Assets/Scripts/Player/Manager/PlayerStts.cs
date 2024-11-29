using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStts : MonoBehaviour
{
    public bool alive = true;
    [Space]
    public Slider sliderHeath;
    public Slider sliderThirst;
    public Slider sliderHunger;
    [Space]

    public Transform playerBody;
    public string playerName;
    [Space]

    public float money;
    [Range(0, 100)]
    public float health;
    [Range(0, 100)]
    public float thirst;
    [Range(0, 100)]
    public float hunger;
    [Range(0, 100)]
    public float happy;

    [SerializeField] private TextMeshProUGUI txtdinheiro;
    private static PlayerStts _instance;

    private Coroutine almostDeadCoroutine;

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

    IEnumerator IEAlmostDead(SpriteRenderer spriteRenderer)
    {
        float pulseDuration = 1f; // Duração do ciclo de pulsação (ir e voltar)
        float elapsedTime = 0f;

        while (health < 25 && alive)
        {
            // Interpolação gradativa entre vermelho e branco
            float lerpValue = Mathf.PingPong(elapsedTime / pulseDuration, 0.3f); // Valor entre 0 e 1
            spriteRenderer.color = Color.Lerp(Color.white, Color.red, lerpValue);

            elapsedTime += Time.deltaTime;
            yield return null; // Espera até o próximo frame
        }

        // Garantir que a cor volte ao normal quando sair do loop
        spriteRenderer.color = Color.white;
    }

    void UpdateSliders()
    {
        SpriteRenderer playerspriterender = playerBody.GetComponent<SpriteRenderer>();

        if (health < 25 && almostDeadCoroutine == null)
        {
            almostDeadCoroutine = StartCoroutine(IEAlmostDead(playerspriterender));
        }
        else if (health >= 25 && almostDeadCoroutine != null)
        {
            StopCoroutine(almostDeadCoroutine);
            almostDeadCoroutine = null;
            playerspriterender.color = Color.white;
        }

        sliderHeath.value = health;
        sliderThirst.value = thirst;
        sliderHunger.value = hunger;
    }

    void sttsDown()
    {
        thirst -= 0.05f * Time.deltaTime;
        hunger -= 0.05f * Time.deltaTime;

        thirst = Mathf.Clamp(thirst, 0, 100);
        hunger = Mathf.Clamp(hunger, 0, 100);
    }

    void MayDead()
    {
        if (thirst <= 0 || hunger <= 0)
        {
            health -= 0.25f * Time.deltaTime;
        }
        if (hunger > 50 && thirst > 50)
        {
            health += 0.25f * Time.deltaTime;
        }

        health = Mathf.Clamp(health, 0, 100);

        if (health <= 0)
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
