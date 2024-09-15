    using UnityEngine;
public class BreakableObject : MonoBehaviour
{
    [Tooltip("Oque sera dropado")]

    public GameObject[] dropPrefabs;
    [Tooltip("Saude do objeto")]

    public int health;
    [Tooltip("Tipo do objeto(Pedra, Arvore etc...)")]

    public BreakableGameObjectsType ObjectType;
    [Tooltip("Quantos items serao dropados")]

    public int dropNumber;

    public void Destroyed() 
    {
        for (int i = 0; i < dropNumber; i++)
        {

            Instantiate(dropPrefabs[Random.Range(0, dropPrefabs.Length)], transform.position, transform.rotation);
            this.gameObject.SetActive(false);
        }
    }
    public void TakeDamege(int damege)
    {
        if (health <= 0)
        {
            Destroyed();
        }
        else
        {
            health -= damege;
        }
    }
}
