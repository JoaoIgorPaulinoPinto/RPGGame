using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
interface IHitable 
{
    public void Hited(int damage, Transform i, float stanTime, ItemData itemData);
    public void Destroyed();
}
public class HitableGameObject : MonoBehaviour
{

    public Slider slider;
    public int health;
    public List<ItemData> drops;
    public Slider slider_health;
    public AudioClip hitedClip;
    public AudioSource audioSource;
}