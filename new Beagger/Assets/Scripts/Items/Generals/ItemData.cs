using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Items/ItemData")]
public class ItemData : ScriptableObject
{
    public string origin; // mudar para Enum quando houver os lugares

    public float weight;    

    public ItemType itemType;

    public RarityLevel rarityLevel;

    public float condition;

    public string itemName;

    public string description;

    public Sprite icon;

    public GameObject? prefab;
    public GameObject? InHandItemprefab;

    public virtual void UseItem() { }
}