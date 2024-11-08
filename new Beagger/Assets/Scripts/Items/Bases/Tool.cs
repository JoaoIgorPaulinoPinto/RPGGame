using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Items/Types/Tool")]
public class Tool : ItemData
{

    public ToolType toolType;
    public int damage;

    public float cadence;

    
    public override void UseItem()
    {

    }

}
