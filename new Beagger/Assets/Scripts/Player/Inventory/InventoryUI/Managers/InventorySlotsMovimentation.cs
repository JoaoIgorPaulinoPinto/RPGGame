using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotsMovimentation : MonoBehaviour
{
    [SerializeField] AudioClip sfx_pointerEnter;
    [SerializeField] AudioClip sfx_pointerClick;
    [SerializeField] AudioClip sfx_dropItem;
    [SerializeField] AudioClip sfx_drop;

    [SerializeField] Sprite selectedSlotSprite;
    [SerializeField] Sprite NselectedSpriteSlot;
    [SerializeField] InventoryUIManager inventoryUIManager;
    [SerializeField] ItemExpUIManager exp;
    [SerializeField] InventoryToolsBarManager invToolsBarManager;

    public GameObject mouseSlot;
    public GameObject selectedSlot;

    private AudioSource ad;

    private void Start()
    {
        ad = GeneralReferences.Instance.UIAudioSource;
    }

    private void PlaySound(AudioClip clip)
    {
        // Evita que o mesmo som toque sobre si mesmo
        if (ad.isPlaying)
        {
            // Se um som estiver tocando, pare-o
            ad.Stop();
        }

        ad.clip = clip;
        ad.Play();
    }

    public void SelectSlot(GameObject button)
    {
        PlaySound(sfx_pointerClick);
        selectedSlot = button;
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

    public void OnPointerEnter()
    {
        if (mouseSlot == null && !ad.isPlaying) // Toca o som se nenhum som estiver tocando
        {
            PlaySound(sfx_pointerEnter);
        }
    }

    public void DragItem(GameObject button)
    {
        selectedSlot = button;
        mouseSlot = button;
        mouseSlot.transform.position = Input.mousePosition;
        invToolsBarManager.UpdateToolsBarData();
    }

    public void DropItem(GameObject button)
    {
        if (button != null)
        {
            PlaySound(sfx_drop);
        }

        Transform aux = null;

        if (mouseSlot.TryGetComponent(out InventorySlot MS) && button.TryGetComponent(out InventorySlot BS))
        {
            if (BS.transform.parent.TryGetComponent<InventorySlotInfo>(out InventorySlotInfo i))
            {
                if (i.slotType == InventorySlotType.ArmorSlot)
                {
                    if (MS.cellItem.itemType == ItemType.Bag)
                    {
                        aux = mouseSlot.transform.parent;
                        mouseSlot.transform.SetParent(button.transform.parent);
                        button.transform.SetParent(aux);
                        mouseSlot = null;
                        selectedSlot = null;
                    }
                    else
                    {
                        mouseSlot = null;
                        selectedSlot = null;
                    }
                }
                else
                {
                    aux = mouseSlot.transform.parent;
                    mouseSlot.transform.SetParent(button.transform.parent);
                    button.transform.SetParent(aux);
                    mouseSlot = null;
                    selectedSlot = null;
                }
            }
        }
        invToolsBarManager.UpdateToolsBarData();
    }

    public void DropOut()
    {
        if (selectedSlot != null)
        {
            if (selectedSlot.TryGetComponent(out InventorySlot item))
            {
                ItemsManager.Instance.DropItem(item.cellItem, 1, PlayerStts.Instance.playerBody);
                Inventory.Instance.RemoveItem(item.cellItem, item);
                PlaySound(sfx_dropItem);

                if (Inventory.Instance.SearchItem(item.cellItem) == null)
                {
                    exp.gameObject.SetActive(false);
                    selectedSlot = null;
                    StartSlotsSprite();
                }
            }
        }
        else if (mouseSlot != null)
        {
            if (mouseSlot.TryGetComponent(out InventorySlot item))
            {
                ItemsManager.Instance.DropItem(item.cellItem, 1, PlayerStts.Instance.playerBody);
                Inventory.Instance.RemoveItem(item.cellItem, item);
                PlaySound(sfx_dropItem);
            }
        }
    }

    public void UpdateSlotsParentSprite(Transform button)
    {
        foreach (var slot in Inventory.Instance.UIManager.slots)
        {
            if (slot != button.parent.gameObject)
            {
                slot.transform.GetChild(0).TryGetComponent(out InventorySlot i);
                slot.GetComponent<Image>().sprite = i.isSelected ? selectedSlotSprite : NselectedSpriteSlot;
            }
            else
            {
                slot.GetComponent<Image>().sprite = selectedSlotSprite;
            }
        }
    }

    public void UnSetSelectedSlotParentSprite(Transform button)
    {
        if (button.TryGetComponent(out InventorySlot i))
        {
            button.parent.GetComponent<Image>().sprite = i.isSelected ? selectedSlotSprite : NselectedSpriteSlot;
        }
    }

    public void StartSlotsSprite()
    {
        foreach (var slot in Inventory.Instance.UIManager.slots)
        {
            slot.transform.GetChild(0).TryGetComponent(out InventorySlot i);
            i.isSelected = false;
            slot.GetComponent<Image>().sprite = NselectedSpriteSlot;
        }
    }
}
