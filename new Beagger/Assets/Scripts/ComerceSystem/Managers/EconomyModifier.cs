using UnityEngine;

public class EconomyModifier : MonoBehaviour
{
    [SerializeField]LocalEconomyManager localEconomyManager;

    public void UpdateCriminalityValues(float i )
    {
        localEconomyManager.localCrimanlity += i;
    }
    public void UpdateRecoursesAbundanceValues(float i)
    {
        localEconomyManager.abundanceOfResources += i;
    }
    public void UpdateClimateValues(float i)
    {
        localEconomyManager.climate += i;
    }
}