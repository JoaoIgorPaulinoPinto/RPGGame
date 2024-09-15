using UnityEngine;

public class GameItem : MonoBehaviour
{
    public ItemData itemData;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Inventory.Instance.AddItem(itemData))
            {
                Destroy(gameObject);
            }
            else
            {
                print("Você não possui espaço suficiente no inventário para carregar este item");
            }
        }
    }
}
