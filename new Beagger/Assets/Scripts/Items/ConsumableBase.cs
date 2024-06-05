using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Resource/Consumable")]
public class ConsumableBase : Item
{
    public int lifeRealer;

    public void UseConsumableItem()
    {
        Debug.Log("lifeRealer:  " + lifeRealer);
    }
}