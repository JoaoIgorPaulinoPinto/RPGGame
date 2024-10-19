using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

public class EquipedItemsManager : MonoBehaviour
{


    [SerializeField] ToolsBarManager ToolsBarManager;

    List<ToolsBarSlot> slots;

    [SerializeField] Transform pivot;

    [Tooltip("Ferramenta que esta na mao do jogador")]
    public ItemData EquipedItem;
    public GameObject button;
    public GameObject prefab;
    public static EquipedItemsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destrói a nova instância se outra já existir
        }
        else
        {
            Instance = this;

        }
    }
    public void ChangeEquipedTool(GameObject? button)
    {
        if (button == null)
        {
            if (pivot.childCount > 0)
            {
                Transform currentItem = pivot.GetChild(0);
                Destroy(currentItem.gameObject);
            }
        }
        else
        {
            // Obtém o item do botão
            this.button = button;
            ItemData item = button.GetComponent<ToolsBarSlot>().item;
            button.TryGetComponent<ToolsBarSlot>(out ToolsBarSlot toolsBarSlot);
            ToolsBarManager.currentSlotIndex = toolsBarSlot.slotindex;
            ToolsBarManager.UpdateSelectedSlotUI(toolsBarSlot);
            // Atualiza o item equipado
            EquipedItem = item;

            // Remove o item atual, se existir
            if (pivot.childCount > 0)
            {
                Transform currentItem = pivot.GetChild(0);
                Destroy(currentItem.gameObject);
                UseItemsSystem.Instance.equipedItemAnimator = null;
            }

            // Instancia o novo item, se houver um prefab
            if (EquipedItem != null && EquipedItem.prefab != null)
            {
                GameObject i = Instantiate(EquipedItem.prefab, pivot);

                i.GetComponent<SpriteRenderer>().sortingOrder = 3;
                 
                prefab = i;
                i.TryGetComponent<Animator>(out Animator animator);
                UseItemsSystem.Instance.equipedItemAnimator = animator;
            }
        }
    }

}
