using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryToolsBar : MonoBehaviour
{
    InventorySlot cell;
    ToolsBarCell tollsBarCell;

    private void Start()
    {
        cell = GetComponent<InventorySlot>(); 
    }
    
}
