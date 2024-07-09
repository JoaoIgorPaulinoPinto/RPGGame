using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractionsTrigger : MonoBehaviour
{   
    public NPCInteraction NPCInteraction;
    public NPCMovimentation NPCMovimentation;
    [Range(0, 5)]
    public float minDistance;
    private void Update()
    {
        check();
    }
    void check()
    {
        if (NPCMovimentation.currentTarget != null)
        {
            float distance = Vector2.Distance(transform.position, NPCMovimentation.currentTarget.position);

            // Verifica se a dist�ncia � menor ou igual a minDistance e se o NPC n�o est� em a��o
            if (distance <= minDistance && !NPCInteraction.inAction)
            {
                NPCInteractionCompatibilities type = NPCMovimentation.currentTarget.GetComponent<NPCLocomotionPoint>().locomotionPointType;
                NPCInteraction.StartInteraction(type);
            }
        }
    }

}

