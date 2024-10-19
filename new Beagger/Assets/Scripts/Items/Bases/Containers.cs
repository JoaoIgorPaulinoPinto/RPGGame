using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Items/Types/Container")]

public class Containers : ItemData
{
    public float size;
    public ItemType[] acceptedItems;
    public override void UseItem()
    {
    }
}
