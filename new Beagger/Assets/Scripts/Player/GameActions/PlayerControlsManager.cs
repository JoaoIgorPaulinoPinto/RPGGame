using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControlsManager : MonoBehaviour
{
   
    public static PlayerControlsManager Instance { get; private set; }
    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
        }
        else
        {
            Instance = this; 
            DontDestroyOnLoad(gameObject); 
        }
    }
    public bool realease = true;

    public KeyBinding KeyBinding;
    public GeneralReferences references;

  

    private void Update()
    {
        if (realease)
        {
            Interact();
            Inventory();
            UseEquipedItem();
            references.PlayerMovementation.canMove = true;
        }
        else
        {
            InteractionUIManager.Instance.HideUI();
            if (references.inventoryUIManager.isOpen)
            {
                Inventory();
            }
            references.PlayerMovementation.canMove = false;
        }
    }

    void Inventory()
    {
        if (Input.GetKeyDown(KeyBinding.key_inventory))
        {
            if (references.inventoryUIManager.isOpen)
            {
                references.inventoryUIManager.CloseInventory();
                references.GameHUD.SetActive(true);
            }
            else
            {
                references.inventoryUIManager.OpenInventory();
                references.GameHUD.SetActive(false);
            }
        }
    }

    void Interact()
    {
        if (Input.GetKeyDown(KeyBinding.key_interact))
        {
            if (references.InteractionCapter.objTarget)
            {
                references.InteractionCapter.objTarget.TryGetComponent(out IInteractable obj);
                obj?.Interact();
            }
        }
    }

    void UseEquipedItem()
    {
        ItemData item = references.EquipedItemsManager.EquipedItem;
        if (references.EquipedItemsManager.EquipedItem != null)
        {
            if ( item is Tool || item is Weapon )
            {
                if (Input.GetMouseButton(KeyBinding.key_atack))
                {
                    UseItemsSystem.Instance.Atack(references.EquipedItemsManager.EquipedItem);
                }
            }
            else
            {
                if (Input.GetKey(KeyBinding.key_use))
                {
                    UseItemsSystem.Instance.Use(references.EquipedItemsManager.EquipedItem);
                }
            }
          
        }
       
    }
}
