

using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Items/Resource/Tool")]
public class Tool : Item
{
    public Tool tool;
    public ToolType toolType;
    public int damege;

    public float fireRate;

    public bool canHit = true;
    public void Atack(GameObject target)
    {
        if (target.TryGetComponent<BreakableObject>(out BreakableObject objeto))
        {
            objeto.TakeDamege(damege);
        }
    }

}
