using TMPro;
using UnityEngine;

public class PlayerStts : MonoBehaviour
{
    public float money;
    [SerializeField] private TextMeshProUGUI txtdinheiro;
    private void Update()
    {

        UpdatePlayerMoney();
    }

    void UpdatePlayerMoney()
    {
        txtdinheiro.text = "R$" + money.ToString("00.00");
    }
}
