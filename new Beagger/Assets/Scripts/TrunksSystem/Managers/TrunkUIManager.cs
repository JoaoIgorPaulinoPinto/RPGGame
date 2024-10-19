using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI; // Adicione isso para usar o Dropdown

public class TrunkUIManager : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] GameObject GameHUD;

    [SerializeField] TextMeshProUGUI trunk_lbl_limitWeight;
    [SerializeField] TextMeshProUGUI trunk_lbl_currentWeight;

    [SerializeField] TextMeshProUGUI inv_lbl_limitWeight;
    [SerializeField] TextMeshProUGUI inv_lbl_currentWeight;

    [SerializeField] TrunkSystem trunkSystem;

    [SerializeField] List<TrunkSlot> trunkSlots = new List<TrunkSlot>();
    [SerializeField] Transform slotsParent;
    [SerializeField] GameObject slotsPrefab;

    [SerializeField] List<TrunkSlot> inventorySlots = new List<TrunkSlot>();
    [SerializeField] Transform invSlotsParent;
    [SerializeField] GameObject invSlotsPrefab;

    [SerializeField] TMP_Dropdown trunkFilterDropdown; // Dropdown para filtrar o baú
    [SerializeField] TMP_Dropdown inventoryFilterDropdown; // Dropdown para filtrar o inventário

    private void Start()
    {

    }

    // Configura o dropdown com as opções de filtro baseadas em ItemType
    private void SetupFilterDropdown(TMP_Dropdown dropdown, System.Action<List<TrunkItems>> updateUIAction)
    {
        dropdown.ClearOptions();
        List<string> options = new List<string>(System.Enum.GetNames(typeof(ItemType)));
        dropdown.AddOptions(options);

        // Adiciona listener para o dropdown
        dropdown.onValueChanged.AddListener(delegate { UpdateUI(trunkSystem.items); });
    }

    public void UpdateUI(List<TrunkItems> trunkItems)
    {
        trunk_lbl_limitWeight.text = "Peso limite: " + trunkSystem.maxWeight.ToString("F2");
        trunk_lbl_currentWeight.text = "Peso atual: " + trunkSystem.currentWeight.ToString("F2");
        inv_lbl_limitWeight.text = "Peso limite: " + Inventory.Instance.limitWeight.ToString("F2");
        inv_lbl_currentWeight.text = "Peso atual: " + Inventory.Instance.currentWeight.ToString("F2");


        // Limpar e destruir slots do baú
        ClearAndDestroySlots(trunkSlots);

        ItemType trunkFilter = (ItemType)trunkFilterDropdown.value; // Obtém o filtro do baú
        foreach (var item in trunkItems)
        {
            if (item.item.itemType == trunkFilter || trunkFilter == ItemType.None) // Aplica o filtro
            {
                GameObject newSlot = Instantiate(slotsPrefab, slotsParent);
                if (newSlot.TryGetComponent<TrunkSlot>(out TrunkSlot newTrunksSlot))
                {
                    newTrunksSlot.SetSlot(item, trunkSystem);
                    trunkSlots.Add(newTrunksSlot);
                }
                else
                {
                    Debug.LogError("O prefab não possui o componente TrunkSlot.");
                }
            }
        }

        // Atualizar inventário
        ClearAndDestroySlots(inventorySlots);

        ItemType inventoryFilter = (ItemType)inventoryFilterDropdown.value; // Obtém o filtro do inventário
        List<InventoryItems> inventoryItems = Inventory.Instance.inventory;
        foreach (var item in inventoryItems)
        {
            if (item.item.itemType == inventoryFilter || inventoryFilter == ItemType.None) // Aplica o filtro
            {
                GameObject newSlot = Instantiate(invSlotsPrefab, invSlotsParent);
                if (newSlot.TryGetComponent<TrunkSlot>(out TrunkSlot newInventorySlot))
                {
                    TrunkItems trunkItem = new TrunkItems(item.item, item.quant);
                    newInventorySlot.SetSlot(trunkItem, trunkSystem);
                    inventorySlots.Add(newInventorySlot);
                }
                else
                {
                    Debug.LogError("O prefab não possui o componente TrunkSlot.");
                }
            }    // Atualizar informações de peso
           
        }

    


        trunkSystem.UpdateCallerData();
    }

    // Método para limpar e destruir slots sem modificar a coleção durante a iteração
    private void ClearAndDestroySlots(List<TrunkSlot> slots)
    {
        for (int i = slots.Count - 1; i >= 0; i--)
        {
            if (slots[i] != null)
            {
                Destroy(slots[i].gameObject);
            }
        }
        slots.Clear();
    }

    public void OpenUI()
    {
        // Configura os dropdowns e adiciona listeners
        SetupFilterDropdown(trunkFilterDropdown, UpdateUI);
        SetupFilterDropdown(inventoryFilterDropdown, UpdateUI);

        UpdateUI(trunkSystem.items);
        UI.SetActive(true);
        GameHUD.SetActive(false);
        GeneralUIManager.Instance.animator.SetBool("Trunk", true);
        PlayerControlsManager.Instance.realease = false;
    }
    public void CloseUI()
    {
        StartCoroutine(closeRotine());
    }
    
    IEnumerator closeRotine()
    {
        GeneralUIManager.Instance.animator.SetBool("Trunk", false);
        yield return new WaitForSeconds(0.4f);
        PlayerControlsManager.Instance.realease = true;
        UI.SetActive(false);
        GameHUD.SetActive(true);
    }
}
