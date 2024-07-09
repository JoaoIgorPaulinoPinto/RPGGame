using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LocalEconomyManager : MonoBehaviour
{
    public PricesManager pricesManager;

    public float localCrimanlity;
    public float abundanceOfResources;
    public float climate;

    public void SaveChanges()
    {
        float valor;
        valor = (localCrimanlity + abundanceOfResources + climate) / 3;

        pricesManager.SetGeneralChanges(valor);
    }
}
