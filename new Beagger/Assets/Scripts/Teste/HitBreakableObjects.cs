using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;

public class HitBreakableObjects : MonoBehaviour
{
    [SerializeField] EquipedToolsManager toolsManager;
    [SerializeField] InteractionsCapter interactionsManager;
    private void Update()
    {
        if (interactionsManager.objTarget != null && interactionsManager.objTarget.TryGetComponent<BreakableObject>(out BreakableObject target))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(AtackRotine(interactionsManager.objTarget.gameObject));
            }
        }
    }

    IEnumerator AtackRotine(GameObject target)
    {
        if (toolsManager.equipedTool.canHit)
        {
            toolsManager.equipedTool.canHit = false;

            toolsManager.equipedTool.Atack(target.gameObject);
            print("atacou");
            yield return new WaitForSeconds(toolsManager.equipedTool.fireRate);
            toolsManager.equipedTool.canHit = true;
        }
    }
}
