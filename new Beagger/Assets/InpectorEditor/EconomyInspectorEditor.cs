using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EconomyManager))]
public class EconomyInspectorEditor : Editor
{
    float generalPricesModi = 0;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EconomyManager economyManager = (EconomyManager)target;

        generalPricesModi = EditorGUILayout.Slider("Change General values", generalPricesModi, -10, 10);

        if (GUILayout.Button("Alterar"))
        {
            economyManager.ChangeGeneralPrice();
        }
        if (GUI.changed)
        {
            EditorUtility.SetDirty(economyManager);
        }
    }
}
[CustomEditor(typeof(LocalEconomyManager))]
public class LocalEconomyEditor : Editor
{
    float changeClimateValue = 0f;
    float changeRelationShipValue = 0f;
    float changeAbundanceOfResourcesValue = 0f;
    float changeCriminalityValue = 0f;
    float changeGeneralValue = 0f;

    public override void OnInspectorGUI()
    {
        // Obtém uma referência ao script alvo (LocalEconomyManager)
        LocalEconomyManager localEconomyManager = (LocalEconomyManager)target;

        // Desenha o Inspector padrão
        DrawDefaultInspector();

        // Cria o slider e o botão para a função ChangeClimateValues
        changeClimateValue = EditorGUILayout.Slider("Change Climate Value", changeClimateValue, -10, 10);
        if (GUILayout.Button("Update Climate"))
        {
            localEconomyManager.ChangeClimateValues(changeClimateValue);
        }


        // Cria o slider e o botão para a função ChangeAbundanceOfResourcesValues
        changeAbundanceOfResourcesValue = EditorGUILayout.Slider("Change Abundance Of Resources Value", changeAbundanceOfResourcesValue, -10, 10);
        if (GUILayout.Button("Update Abundance Of Resources"))
        {
            localEconomyManager.ChangeAbundanceOfResourcesValues(changeAbundanceOfResourcesValue);
        }

        // Cria o slider e o botão para a função ChangeCrimanalityValues
        changeCriminalityValue = EditorGUILayout.Slider("Change Criminality Value", changeCriminalityValue, -10, 10);
        if (GUILayout.Button("Update Criminality"))
        {
            localEconomyManager.ChangeCrimanalityValues(changeCriminalityValue);
        }

        // Cria o slider e o botão para a função ChangeGeneralValues
        changeGeneralValue = EditorGUILayout.Slider("Change General Value", changeGeneralValue, -10, 10);
        if (GUILayout.Button("Update All Values"))
        {
            localEconomyManager.ChangeGeneralValues(changeGeneralValue);
        }

        // Adiciona um botão para a função SaveChanges
        if (GUILayout.Button("Save Changes"))
        {
            localEconomyManager.SaveChanges();
        }

        // Salva as mudanças feitas nas variáveis
        if (GUI.changed)
        {
            EditorUtility.SetDirty(localEconomyManager);
        }
    }
}
[CustomEditor(typeof(PricesManager))]
public class PricesManagerEditor : Editor
{
    float sellChancePriceValue = 0f;
    float buyChancePriceValue = 0f;
    float setGeneralChangesValue = 0f;
    float changePricesOfTypeValue = 0f;
    string changePricesOfTypeName = "";

    public override void OnInspectorGUI()
    {
        // Obtém uma referência ao script alvo (PricesManager)
        PricesManager pricesManager = (PricesManager)target;

        // Desenha o Inspector padrão
        DrawDefaultInspector();

        // Cria o slider e o botão para a função SellChancePrices
        sellChancePriceValue = EditorGUILayout.Slider("Sell Chance Prices", sellChancePriceValue, -10, 10);
        if (GUILayout.Button("Update Sell Prices"))
        {
            pricesManager.SellChancePrices(sellChancePriceValue);
        }

        // Cria o slider e o botão para a função BuyChancePrices
        buyChancePriceValue = EditorGUILayout.Slider("Buy Chance Prices", buyChancePriceValue, -10, 10);
        if (GUILayout.Button("Update Buy Prices"))
        {
            pricesManager.BuyChancePrices(buyChancePriceValue);
        }

        // Cria o campo de texto, slider e botão para a função ChangePricesOfType
        changePricesOfTypeName = EditorGUILayout.TextField("Item Name", changePricesOfTypeName);
        changePricesOfTypeValue = EditorGUILayout.Slider("Change Prices Of Type", changePricesOfTypeValue, -10, 10);
        if (GUILayout.Button("Update Prices Of Type"))
        {
            pricesManager.ChangePricesOfType(changePricesOfTypeName, changePricesOfTypeValue);
        }

        // Cria o slider e o botão para a função SetGeneralChanges
        setGeneralChangesValue = EditorGUILayout.Slider("Set General Changes", setGeneralChangesValue, -10, 10);
        if (GUILayout.Button("Set General Price Changes"))
        {
            pricesManager.SetGeneralChanges(setGeneralChangesValue);
        }

        // Salva as mudanças feitas nas variáveis
        if (GUI.changed)
        {
            pricesManager.DEBUG();
            EditorUtility.SetDirty(pricesManager);
        }
    }
}
