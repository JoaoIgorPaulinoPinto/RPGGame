using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralReferences : MonoBehaviour
{
    public AudioSource UIAudioSource;
    public InventoryUIManager inventoryUIManager;
    public PurchaseSystemUIManager purchaseSystemUIManager;
    public SaleSystemUIManager saleSystemUIManager;
    public GameObject GameHUD;
    public InteractionsCapter InteractionCapter;
    public PlayerMovementation PlayerMovementation;
    public DialogSystem DialogSystem;

    public static GeneralReferences Instance {  get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
