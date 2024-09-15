using System.ComponentModel.Design;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public bool isSelected = false;
    
    public ItemData cellItem;
    [SerializeField] Image spriteRender;

    public TextMeshProUGUI? labelQuant;

    public float quant = 0;

    public void SetValues(ItemData item, float qnt)
    {
        // Definir a cor do sprite com base na presença do item
        Color color = spriteRender.color;
        color.a = item != null ? 1f : 0f; // Alfa = 1 se o item estiver presente, 0 se não estiver
        spriteRender.color = color;

        // Configurar o sprite e os rótulos
        spriteRender.sprite = item.icon;

        labelQuant.text = qnt.ToString();

        cellItem = item;
        quant = qnt; 
    }

    private void Awake()
    {
        if (cellItem == null)
        {
            ClearValues();
        }
    }
    private void Update()
    {

    }

    public void ClearValues()
    {
        this.cellItem = null;
        if (labelQuant != null) labelQuant.text = "";
        if (spriteRender != null)
        {
            Color color = spriteRender.color;
            color.a = 0; // Alfa = 0 para esconder o sprite
            spriteRender.color = color;
            //spriteRender.enabled = false; // Opcional: desativa o componente Image
        }
    }

    
}
