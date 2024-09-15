using TMPro;
using UnityEngine;

public class PlayerStts : MonoBehaviour
{
    public Transform playerBody;
    public string playerName;
    public float money;
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
    }

    void UpdatePlayerMoney()
    {
       txtdinheiro.text = "R$" + money.ToString("00.00");
    }
}
