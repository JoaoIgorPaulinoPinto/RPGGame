using UnityEngine;

public class CollectibleObject : MonoBehaviour
{
    public Item item; // Isto pode ser um Weapon, Potion, ou qualquer derivado de Item.
    [Tooltip("Nome do Item(muito usado em verificações do inventario)")]
    [HideInInspector]public string itemName;
    [Tooltip("Descricao do item")]
    [HideInInspector]public string description;
    [Tooltip("sprite do item")]
    [HideInInspector]public Sprite icon;
    [Tooltip("Oque ele dropa ao ser retirado do inventario")]
    public GameObject? prefab;
    private void Start()
    {
        itemName = item.itemName;
        description = item.description;
        icon = item.icon;
        prefab = item.prefab;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory playerInventory = collision.GetComponent<Inventory>();
            if (playerInventory != null)
            {
                playerInventory.AddItem(item);
                Destroy(gameObject);
            }
        }
    }
}
