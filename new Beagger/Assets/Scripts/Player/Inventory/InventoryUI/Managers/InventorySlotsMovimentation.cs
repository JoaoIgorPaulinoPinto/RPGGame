
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventorySlotsMovimentation : MonoBehaviour
{

    [SerializeField] Sprite selectedSlotSprite;
    [SerializeField] Sprite NselectedSpriteSlot;
    [SerializeField] InventoryUIManager inventoryUIManager;
    [SerializeField] ItemExpUIManager exp;


    public GameObject mouseSlot;
    public GameObject selectedSlot;


    public void SelectSlot(GameObject button)
    {
        selectedSlot = null;

            if (button.TryGetComponent<InventorySlot>(out InventorySlot selectedSlotComponent))
            {
                foreach (var slotUI in Inventory.Instance.UIManager.slots)
                {
                    if (slotUI.transform.GetChild(0).TryGetComponent(out InventorySlot currentSlot))
                    {
                        bool isCurrentSlotSelected = currentSlot == selectedSlotComponent || currentSlot.gameObject == selectedSlot;

                        if (isCurrentSlotSelected && currentSlot.cellItem != null)
                        {
                            currentSlot.isSelected = !currentSlot.isSelected;
                            selectedSlot = currentSlot.isSelected ? button : null;
                            exp.Show(button.gameObject);
                            selectedSlot = button;
                        }
                        else
                        {
                            currentSlot.isSelected = false;
                        }
                    }
                }

                UpdateSlotsParentSprite(button.transform);
            }
    }
    public void onPointerExit(GameObject button)
    {
        UnSetSelectedSlotParentSprite(button.transform);

        if (!selectedSlot)
        {
            exp.gameObject.SetActive(false);
        }
        else
        {
           exp.Show(selectedSlot.gameObject);
        }

    }
    public void DragItem(GameObject button)
    {
        selectedSlot = button;
        mouseSlot = button;
        mouseSlot.transform.position = Input.mousePosition;
    }
    public void DropItem(GameObject button)
    {
        Transform aux = null;

        InventorySlot MS; mouseSlot.TryGetComponent<InventorySlot>(out MS);
        InventorySlot BS; button.TryGetComponent<InventorySlot>(out BS);
        aux = mouseSlot.transform.parent;
        mouseSlot.transform.SetParent(button.transform.parent);
        button.transform.SetParent(aux);
        /*
        if(MS != null && BS != null)
        {
            switch (BS.GetComponentInParent<InventorySlotInfo>().slotType)
            {
                case InventorySlotType.ToolsBarSlot:

                    if (MS.cellItem.itemType == ItemType.Tool)
                    {
                        if (BS.cellItem != null)
                        {
                            if (MS.GetComponentInParent<InventorySlotInfo>().slotType == InventorySlotType.ToolsBarSlot)
                            {
                                aux = mouseSlot.transform.parent;
                                mouseSlot.transform.SetParent(button.transform.parent);
                                button.transform.SetParent(aux);
                            }
                        }
                        else
                        {
                            aux = mouseSlot.transform.parent;
                            mouseSlot.transform.SetParent(button.transform.parent);
                            button.transform.SetParent(aux);
                        }

                    }
                    break;
                case InventorySlotType.ArmorSlot:
                    if (MS.cellItem.itemType == ItemType.Armor)
                    {
                        if (BS.cellItem != null)
                        {
                            if (MS.GetComponentInParent<InventorySlotInfo>().slotType == InventorySlotType.ToolsBarSlot)
                            {
                                aux = mouseSlot.transform.parent;
                                mouseSlot.transform.SetParent(button.transform.parent);
                                button.transform.SetParent(aux);
                            }
                        }
                        else
                        {
                            aux = mouseSlot.transform.parent;
                            mouseSlot.transform.SetParent(button.transform.parent);
                            button.transform.SetParent(aux);
                        }

                    }
                    break;

                case InventorySlotType.defaultType:
                    if (BS.cellItem != null)
                    {
                        if (MS.GetComponentInParent<InventorySlotInfo>().slotType == InventorySlotType.defaultType)
                        {
                            aux = mouseSlot.transform.parent;
                            mouseSlot.transform.SetParent(button.transform.parent);
                            button.transform.SetParent(aux);
                        }
                    }
                    else
                    {
                        aux = mouseSlot.transform.parent;
                        mouseSlot.transform.SetParent(button.transform.parent);
                        button.transform.SetParent(aux);
                    }

                    
                break;
            }
       

        }
         */
        mouseSlot = null;
        selectedSlot = null;
    }
    public void DropOut()
    {
        if(mouseSlot != null)
        {
            InventorySlot item;
            mouseSlot.TryGetComponent<InventorySlot>(out item);
            ItemsManager.Instance.DropItem(item.cellItem, 1);
            Inventory.Instance.RemoveItem(item.cellItem, item);

        }
        else if(selectedSlot)
        {
            InventorySlot item;
            selectedSlot.TryGetComponent<InventorySlot>(out item);
            ItemsManager.Instance.DropItem(item.cellItem, 1);
            Inventory.Instance.RemoveItem(item.cellItem, item);
            if (Inventory.Instance.SearchItem(item.cellItem) != null)
            {

            }
            else
            {
            
                selectedSlot = null;
                StartSlotsSprite();
                exp.gameObject.SetActive(false);    
            }
        }
    }
    public void UpdateSlotsParentSprite(Transform button)
    {
        foreach (var slot in Inventory.Instance.UIManager.slots)
        {   
            if (slot != button.parent.gameObject)
            {
                InventorySlot i;
                slot.transform.GetChild(0).TryGetComponent<InventorySlot>(out i);
                if (!i.isSelected)
                {
                    i.transform.parent.GetComponent<Image>().sprite = NselectedSpriteSlot;  
                }
                else
                {
                    i.transform.parent.GetComponent<Image>().sprite = selectedSlotSprite;
                }
            }
            else if (slot == button.parent.gameObject)
            {

                slot.GetComponent<Image>().sprite = selectedSlotSprite;
                
            }
        }
    }
    public void UnSetSelectedSlotParentSprite(Transform button) {

        InventorySlot i;
        button.TryGetComponent<InventorySlot>(out i);
        if (i != null)
        {
            if (i.isSelected)
            {
                button.parent.GetComponent<Image>().sprite = selectedSlotSprite;
            }
            else
            {
                button.parent.GetComponent<Image>().sprite = NselectedSpriteSlot;
            }
        
        }
    
    }
    public void StartSlotsSprite()
    {
         foreach (var slot in Inventory.Instance.UIManager.slots)
         {
            slot.transform.GetChild(0).TryGetComponent<InventorySlot>(out InventorySlot i);
            i.isSelected = false;
            slot.GetComponent<Image>().sprite = NselectedSpriteSlot;
         }   
    }
}