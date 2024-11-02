using UnityEngine;
using UnityEngine.UIElements;

public class ItemsManager : MonoBehaviour
{
    public static ItemsManager Instance  { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void DropItem(ItemData item, int quant, Transform dropPosition)
    {
        for (int i = 0; i < quant; i++)
        {
            Vector2 pos;
            do
            {
                // Gera uma posição aleatória com um offset entre -0.8 e 0.8
                pos = new Vector2(dropPosition.position.x + Random.Range(-0.8f, 0.8f),
                                  dropPosition.position.y + Random.Range(-0.8f, 0.8f));
            }
            while (Mathf.Abs(pos.x - dropPosition.position.x) < 0.3f || Mathf.Abs(pos.y - dropPosition.position.y) < 0.3f);

            // Instancia o item na posição final válida
            GameObject droppedItem = Instantiate(item.prefab, pos, Quaternion.identity);
        }
    }/*
    public void TakeItem(Transform collision)
    {
        collision.TryGetComponent(out GameItem gameItem);
        if (gameItem)
        {
            if (Inventory.Instance.AddItem(gameItem.ItemInstance))
            {
                print("item pego");
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Item"))
        {
            TakeItem(collision.transform);
        }
    }
    */
}