using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public LocalEconomyManager[]localEconomyManager;
    public float generalIndexChange;
    public void UpdateComerceEconomyValues()
    {
        for (int i = 0; i < localEconomyManager.Length; i++)
        {
            localEconomyManager[i].SaveChanges();
        }
    }
}