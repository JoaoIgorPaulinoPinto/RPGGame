using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LocalEconomyManager : MonoBehaviour
{
    [SerializeField]TextMeshProUGUI _txt1, _txt2, _txt3;


    public PricesManager pricesManager;

    [SerializeField] float localCrimanlity = 0;
    [SerializeField] float abundanceOfResources = 0;

    [SerializeField] float climate = 0;


    private void Update()
    {
        _txt1.text = "criminalidade local: " + localCrimanlity.ToString();
        _txt2.text = "abundancia de recursos: " +abundanceOfResources.ToString();
        _txt3.text = "clima: " + climate.ToString();
    }

    public void ChangeClimateValues(float i)
    {
        climate += i;
    }
    public void ChangeAbundanceOfResourcesValues(float i)
    {
        abundanceOfResources += i;
    }
    public void ChangeCrimanalityValues(float i)
    {
        localCrimanlity += i;
    }

    public void ChangeGeneralValues(float i)
    {
        ChangeCrimanalityValues(i);
        ChangeAbundanceOfResourcesValues(i);

        ChangeClimateValues(i);
    }

    public void SaveChanges()
    {
        float valor;
        valor = localCrimanlity + abundanceOfResources + climate / 3;

        pricesManager.SetGeneralChanges(valor);
    }
}
