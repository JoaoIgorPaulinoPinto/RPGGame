using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUIReferences : MonoBehaviour
{
    public InventoryUIManager inventoryUIManager;
    public PurchaseSystemUIManager purchaseSystemUIManager;
    public SaleSystemUIManager saleSystemUIManager;
    public GameObject GameHUD;

    public bool isOpen { get; internal set; }
}
