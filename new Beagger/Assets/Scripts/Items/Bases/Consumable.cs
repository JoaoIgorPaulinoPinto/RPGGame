using System.Collections;
using System.Drawing.Printing;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Types/Food")]
public class Consumable : ItemData
{
    public int efectPower;
    public float time;
    public ConsumableType type;

    public override void UseItem()
    {
        switch (type)
        {
            case ConsumableType.Food: PlayerStts.Instance.hunger += efectPower; break;
            case ConsumableType.Water: PlayerStts.Instance.thirst += efectPower; break;
            case ConsumableType.Sweet: PlayerStts.Instance.happy += efectPower; break;
            case ConsumableType.Poison: PlayerStts.Instance.heath -= efectPower; break;
        }
    }
}