using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlsManager : MonoBehaviour
{
    public KeyBinding KeyBinding;

    public GeneralUIReferences references;

    private void Update()
    {
        Inventory();
    }

    void Inventory()
    {
        if (Input.GetKeyDown(KeyBinding.key_inventory))
        {
            if (references.inventoryUIManager.isOpen == true)
            {
                references.inventoryUIManager.CloseInventory();
                references.GameHUD.SetActive(true);
            }
            else
            {
                references.inventoryUIManager.OpenInventory();
                references.GameHUD.SetActive(false);
            }

        }
    }
}
