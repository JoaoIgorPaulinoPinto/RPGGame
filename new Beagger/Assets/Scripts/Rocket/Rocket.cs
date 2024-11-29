using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Rocket : InteractableGameObject, IInteractable
{
    public List<ItemData> necesseryItems = new List<ItemData>();
    public List<ItemData> deliveredItems = new List<ItemData>();

    public bool isCompleted;

    [SerializeField] private GameObject INVParent; // Parent dos itens entregues
    [SerializeField] private GameObject prefbSlotINV; // Prefab do slot para itens necessários

    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject NIParent; // Parent dos itens necessários
    [SerializeField] private GameObject DIParent; // Parent dos itens entregues
    [SerializeField] private GameObject prefbSlotNI; // Prefab do slot para itens necessários
    [SerializeField] private GameObject prefbSlotDI; // Prefab do slot para itens entregues
    [SerializeField] private Button finalizeButton;

    public void Interact()
    {
        OpenUI();
    }

    public void DeliverItem(ItemData item)
    {
        if (necesseryItems.Contains(item))
        {
            Inventory.Instance.RemoveItem(item, null);
            deliveredItems.Add(item);
            UpdateUI();
        }
    }
    public void UnDeliveItems(ItemData item)
    {
        if (necesseryItems.Contains(item))
        {
            Inventory.Instance.AddItem(item);
            deliveredItems.Remove(item);
            UpdateUI();
        }
    }

   

    private void UpdateUI()
    {
        // Limpa os campos
        foreach (Transform child in NIParent.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in DIParent.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in INVParent.transform)
        {
            Destroy(child.gameObject);
        }

        // Adiciona os itens necessários
        foreach (var item in necesseryItems)
        {
            var slot = Instantiate(prefbSlotNI, NIParent.transform);
            var rocketSlot = slot.GetComponent<RocketSystemSlot>();
            rocketSlot.SetupSlot(item, this);
        }

        // Adiciona os itens entregues
        foreach (var item in deliveredItems)
        {
            var slot = Instantiate(prefbSlotDI, DIParent.transform);
            var rocketSlot = slot.GetComponent<RocketSystemSlot>();
            rocketSlot.SetupSlot(item, this);
        }
        foreach (var item in Inventory.Instance.inventory)
        {
            if (necesseryItems.Contains(item.item))
            {
                var slot = Instantiate(prefbSlotINV, INVParent.transform);
                var rocketSlot = slot.GetComponent<RocketSystemSlot>();
                rocketSlot.SetupSlot(item.item, this);
            }
      
        }

        // Atualiza o botão de finalização
        finalizeButton.interactable = deliveredItems.Count == necesseryItems.Count;
    }

    public void Finalize()
    {
        SceneManager.LoadScene("ComingOutCutscene");
    }
    public void CloseUI()
    {
        StartCoroutine(waitCloseUI());
    }
    public void OpenUI()
    {
        PlayerControlsManager.Instance.realease = false;
        GeneralUIManager.Instance.animator.SetBool("RocketSystem", true);
        UI.SetActive(true);
        UpdateUI();
        finalizeButton.onClick.AddListener(Finalize);
    }
    IEnumerator waitCloseUI()
    {
        GeneralUIManager.Instance.animator.SetBool("RocketSystem", false);
        yield return new WaitForSeconds(0.35f);
        
        UI.gameObject.SetActive(false);
        PlayerControlsManager.Instance.realease = true;

    }
}
