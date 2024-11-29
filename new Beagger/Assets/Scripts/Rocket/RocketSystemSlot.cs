using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RocketSystemSlot : MonoBehaviour
{
    public Image icon;
    public TextMeshProUGUI itemName;
    public ItemData itemData;
    Rocket roc;

    // Configura o slot com as informações do item
    public void SetupSlot(ItemData data, Rocket rocket )
    {
        roc = rocket;
        itemData = data;
        itemName.text = data.itemName;
        icon.sprite = data.icon;
    }

    public void Deliver()
    {
        roc.DeliverItem(itemData);
    }
    public void UnDeliver()
    {
        roc.UnDeliveItems(itemData);
    }
}
