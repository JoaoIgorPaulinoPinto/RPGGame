using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public LocalEconomyManager[]localEconomyManager;
    public float generalIndexChange;

    public void ChangeGeneralPrice()
    {
        for (int i = 0; i < localEconomyManager.Length; i++)
        {
            localEconomyManager[i].ChangeGeneralValues(generalIndexChange);
        }
    }
}
