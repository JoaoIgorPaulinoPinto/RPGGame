using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryToolsBar : MonoBehaviour
{
    InventoryCell cell;
    ToolsBarCell tollsBarCell;

    private void Start()
    {
        cell = GetComponent<InventoryCell>(); 
    }
    
}
