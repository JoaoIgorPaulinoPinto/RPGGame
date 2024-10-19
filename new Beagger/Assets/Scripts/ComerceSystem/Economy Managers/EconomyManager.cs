using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public static EconomyManager instance;
    public ProductsGeneralTable ProductsGeneralTable;

    private void Start()
    {
        instance = this;
    }

    // M�todo para ajustar os pre�os
    public void AjustPrices()
    {
        ProductsGeneralTable.AdjustPrices();
    }
    public void ReplaceProducts()
    {
        ProductsGeneralTable.ReplaceProducts();
    }

    private void Update()
    {
        // Pressionar a tecla 'V' recalcula os pre�os
        if (Input.GetKeyUp(KeyCode.V))
        {
            AjustPrices();
        }
    }
}
