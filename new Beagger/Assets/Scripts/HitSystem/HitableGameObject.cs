using System.Collections.Generic;
using UnityEngine;
interface IHitable 
{
    public void Hited(int damage, Transform i, float stanTime);
    public void Destroyed();
}
public class HitableGameObject : MonoBehaviour
{
    public int health;
    public List<ItemData> drops;

    public AudioClip hitedClip;
    public AudioSource audioSource;
}