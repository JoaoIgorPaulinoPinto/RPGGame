using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item", menuName = "Items/Types/Weapon")]
public class Weapon : ItemData
{
    public float range;
    public float cadence; 
    public WeaponType weaponType;
    public int damage;

    public LayerMask mask;
    
    
    public void Atack()
    {

    }
}