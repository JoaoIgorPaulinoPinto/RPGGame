using System.Collections.Generic;
using UnityEngine;

public class EquipedItemsManager : MonoBehaviour
{
    List<ToolsBarSlot> slots;

    [SerializeField] Transform pivot;

    [Tooltip("Ferramenta que esta na mao do jogador")]
    public ItemData EquipedItem;
    public GameObject button;

    public void ChangeEquipedTool(GameObject? button)
    {
        // Obtém o item do botão
        this.button = button;
        ItemData item = button.GetComponent<ToolsBarSlot>().item;

        // Atualiza o item equipado
        EquipedItem = item;

        // Remove o item atual, se existir
        if (pivot.childCount > 0)
        {
            Transform currentItem = pivot.GetChild(0);
            Destroy(currentItem.gameObject);
        }

        // Instancia o novo item, se houver um prefab
        if (EquipedItem != null && EquipedItem.prefab != null)
        {
            Instantiate(EquipedItem.prefab, pivot);
        }
    }
    private void Update()
    {
        //if (button)
        //{
        //    button.TryGetComponent<ToolsBarSlot>(out ToolsBarSlot item);
        //    if (item == null) EquipedItem = null; button = null; 
        //}

        if (button)
        { ChangeEquipedTool(button); }
    }
}
