using Unity.VisualScripting;
using UnityEngine;
interface IHitable 
{
    public void Hited(int damage, Transform i, float stanTime);
    public void Destroyed();
}
public class HitableGameObject : MonoBehaviour
{
    public int health;
}