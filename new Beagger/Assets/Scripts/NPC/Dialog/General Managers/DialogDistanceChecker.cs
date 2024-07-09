using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogDistanceChecker : MonoBehaviour
{

    [SerializeField] float minDistance;
    [SerializeField] LayerMask targetLayer;
    [SerializeField] DialogCanvasManager manager;


    private void Update()
    {
        Collider2D npc = Physics2D.OverlapCircle(transform.position, minDistance, targetLayer);

        if (npc != null)
        {
            if (Vector2.Distance(transform.position, npc.transform.position) <= minDistance)
            {
                manager.EnableStartDialogButton(npc.GetComponent<DialogManager>()); print("Menor");
            }

            else if (Vector2.Distance(transform.position, npc.transform.position) >= (minDistance - 1f))
            {
                manager.DisableStartDialogButton(npc.GetComponent<DialogManager>()); print("Maior");
            }
        }
    }

    void OnDrawGizmos()
    {
        // Desenha o círculo com um raio de minDistance
        Gizmos.color = Color.white; // Cor do círculo (aqui vermelho)
        Gizmos.DrawWireSphere(transform.position, minDistance);
    }
}
