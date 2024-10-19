
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InventorySlotsMovimentation : MonoBehaviour
{
    [SerializeField] AudioClip sfx_pointerEnter;
    [SerializeField] AudioClip sfx_pointerClick;
    [SerializeField] AudioClip sfx_dropItem;
   // [SerializeField] AudioClip sfx_drag;
    [SerializeField] AudioClip sfx_drop ;
    
    [SerializeField] Sprite selectedSlotSprite;
    [SerializeField] Sprite NselectedSpriteSlot;
    [SerializeField] InventoryUIManager inventoryUIManager;
    [SerializeField] ItemExpUIManager exp;
    [SerializeField] InventoryToolsBarManager invToolsBarManager;

    public GameObject mouseSlot;
    public GameObject selectedSlot;

    AudioSource ad;
    public void SelectSlot(GameObject button)
    {
        ad.clip = sfx_pointerClick;
        ad.Play();
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
        ad.clip = sfx_pointerEnter;
        ad.Play();
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
        ad.clip = sfx_drop;
        ad.Play();
        invToolsBarManager.UpdateToolsBarData();
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
        // Verificação para mouseSlot
        if (mouseSlot != null)
        {
            Debug.Log("mouseSlot está presente");

            // Tenta pegar o componente InventorySlot
            if (mouseSlot.TryGetComponent<InventorySlot>(out InventorySlot item))
            {
                Debug.Log("InventorySlot encontrado no mouseSlot");

                // Chama o método DropItem e RemoveItem
                ItemsManager.Instance.DropItem(item.cellItem, 1, PlayerStts.Instance.playerBody);
                Inventory.Instance.RemoveItem(item.cellItem, item);
            }
            else
            {
                Debug.LogError("InventorySlot não encontrado no mouseSlot");
            }
        }
        // Verificação para selectedSlot
        else if (selectedSlot != null)
        {
            Debug.Log("selectedSlot está presente");
     

            // Tenta pegar o componente InventorySlot
            if (selectedSlot.TryGetComponent<InventorySlot>(out InventorySlot item))
            {
                Debug.Log("InventorySlot encontrado no selectedSlot");

                // Chama o método DropItem e RemoveItem
                ItemsManager.Instance.DropItem(item.cellItem, 1, PlayerStts.Instance.playerBody);
                Inventory.Instance.RemoveItem(item.cellItem, item);

                // Verifica se o item ainda existe no inventário
                if (Inventory.Instance.SearchItem(item.cellItem) != null)
                {
                    Debug.Log("Item ainda presente no inventário");
                }
                else
                {
                    Debug.Log("Item não encontrado no inventário, limpando selectedSlot");
                    selectedSlot = null;

                    // Reinicia sprites dos slots
                    StartSlotsSprite();

                    // Desativa o objeto exp
                    if (exp != null)
                    {
                        exp.gameObject.SetActive(false);
                        Debug.Log("exp desativado");
                    }
                    else
                    {
                        Debug.LogWarning("exp não foi atribuído!");
                    }
                }
            }
            else
            {
                Debug.LogError("InventorySlot não encontrado no selectedSlot");
            }
        }
        else
        {
            Debug.LogWarning("Nem mouseSlot nem selectedSlot estão presentes");
        }

        // Verifica se o áudio está configurado corretamente antes de tocar
        if (ad != null)
        {
            ad.clip = sfx_dropItem;

            if (ad.clip != null)
            {
                ad.Play();
                Debug.Log("Som de dropItem tocado");
            }
            else
            {
                Debug.LogError("sfx_dropItem não atribuído ao AudioSource");
            }
        }
        else
        {
            Debug.LogError("AudioSource 'ad' não atribuído");
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
    private void Start()
    {
        ad = GeneralReferences.Instance.UIAudioSource;
    }
}