using System.Collections.Generic;

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
            Destroy(gameObject); // Destr�i a nova inst�ncia se outra j� existir
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
            // Remove o item se n�o houver bot�o
            if (pivot.childCount > 0)
            {
                Transform currentItem = pivot.GetChild(0);
                Destroy(currentItem.gameObject);
            }
            EquipedItem = null;
            prefab = null;
        }
        else
        {
            // Obt�m o item do bot�o
            this.button = button;
            ItemData newItem = button.GetComponent<ToolsBarSlot>().item;
            button.TryGetComponent<ToolsBarSlot>(out ToolsBarSlot toolsBarSlot);
            ToolsBarManager.currentSlotIndex = toolsBarSlot.slotindex;
            ToolsBarManager.UpdateSelectedSlotUI(toolsBarSlot);

            // Verifica se � o mesmo item
            if (EquipedItem != newItem)
            {
                // Atualiza o item equipado
                EquipedItem = newItem;

                // Remove o item atual, se existir
                if (pivot.childCount > 0)
                {
                    Transform currentItem = pivot.GetChild(0);
                    Destroy(currentItem.gameObject);
                    UseItemsSystem.Instance.equipedItemAnimator = null;
                }

                // Instancia o novo item
                if (EquipedItem != null && EquipedItem.prefab != null)
                {
                    GameObject i = Instantiate(EquipedItem.prefab, pivot);
                    i.GetComponent<SpriteRenderer>().sortingOrder = 4;

                    prefab = i;
                    i.TryGetComponent<Animator>(out Animator animator);
                    UseItemsSystem.Instance.equipedItemAnimator = animator;
                }
            }
        }
    }
    private void Update()
    {

    }

}
