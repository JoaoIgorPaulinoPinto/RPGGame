using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Inventory;
using static InventoryToolsBarManager;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField]InventoryToolsBarManager inventoryToolsBarManager;



    [SerializeField] TextMeshProUGUI lbl_currentWeight;
    [SerializeField] TextMeshProUGUI lbl_limitWeight;
    
    [SerializeField] Transform UI;
    [SerializeField] Transform[] slotsParents;
    public List<GameObject> slots = new List<GameObject>();
    [SerializeField] InventorySlotsMovimentation inventorySlotsMovimentation;

    public bool isOpen;

    public bool AddToUI(InventoryItems item)
    {
        bool someNull = false;
        foreach (var slot in slots)
        {
            InventorySlot i = slot.transform.GetChild(0).GetComponent<InventorySlot>();
            if (i.cellItem == null)
            {
                someNull = true;
                i.SetValues(item.item, item.quant);
                break;
            }
            else
            {
                

                someNull = false;
            }
        }
        
        inventoryToolsBarManager.UpdateToolsBarData();
        return someNull;
    }
    public void UpdateWightLabel()
    {
        lbl_currentWeight.text = "Peso atual: " + Inventory.Instance.currentWeight.ToString("00.00");
        lbl_limitWeight.text = "Peso limite: " + Inventory.Instance.limitWeight.ToString("00.00");
    }
    public void RemoveFromUI(InventoryItems item, InventorySlot? selectedSlot)
    {
        if (selectedSlot != null)
        {
            selectedSlot.ClearValues();
        }
        else
        {
            foreach (var slot in slots)
            {
                InventorySlot i = slot.transform.GetChild(0).TryGetComponent<InventorySlot>(out i) ? i : null;
                if (i != null && i.cellItem != null)
                {
                    if (item.item.itemName == i.cellItem.itemName)
                    {
                        i.ClearValues();
                        break;
                    }
                }
            }
        }
        inventoryToolsBarManager.UpdateToolsBarData();
    }

    public void UpdateSlotValues(InventoryItems item, InventorySlot? selectedSlot)
    {
        if (selectedSlot != null)
        {
            selectedSlot.ClearValues();
            selectedSlot.SetValues(item.item, item.quant);
        }
        else
        {
            foreach (var slot in slots)
            {
                InventorySlot i = slot.transform.GetChild(0).TryGetComponent<InventorySlot>(out i) ? i : null;
                if (i != null && i.cellItem != null)
                {
                    if (i.cellItem.itemName == item.item.itemName)
                    {
                        i.ClearValues();
                        i.SetValues(item.item, item.quant);
                        break;
                    }
                }
            }
        }
        inventoryToolsBarManager.UpdateToolsBarData();

    }

    public void OpenInventory()
    {
        PlayerControlsManager.Instance.realease = false;
        GeneralUIManager.Instance.animator.SetBool("Inventory", true);
        inventorySlotsMovimentation.StartSlotsSprite();
        UI.gameObject.SetActive(true);
        isOpen = true;
        inventorySlotsMovimentation.StartSlotsSprite();

    }

    public void CloseInventory()
    {
        StartCoroutine(waitCloseInventory());
    }
    IEnumerator waitCloseInventory()
    {
        GeneralUIManager.Instance.animator.SetBool("Inventory", false);
        yield return new WaitForSeconds(0.35f);
        isOpen = false;
        UI.gameObject.SetActive(false);
        PlayerControlsManager.Instance.realease = true;

    }

    private void Start()
    {
        UpdateWightLabel();
        InitializeSlots();
        inventorySlotsMovimentation.StartSlotsSprite();
    }

    private void InitializeSlots()
    {
        slots.Clear(); // Limpa a lista existente
        for (int i = 0; i < slotsParents.Length; i++)
        {
            foreach (Transform child in slotsParents[i])
            {
                if (child.gameObject)
                {
                    slots.Add(child.gameObject);
                }
            }
        }
    }

    public bool VerifyEmpSlots()
    {
        bool retorno = false;
        foreach (var item in slots)
        {
            InventorySlot i = item.transform.GetChild(0).GetComponent<InventorySlot>();
            if(i.cellItem == null)
            {
                return true;
            }
            else
            {
                retorno = false;
            }
        }
        return retorno;
    }


}
