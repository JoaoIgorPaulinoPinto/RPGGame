using System.Collections.Generic;
using UnityEngine;

public class InteractionsCapter : MonoBehaviour
{
    [Tooltip("Foco, objeto interagivel mais proximo do jogador")]
    public InteractableGameObject objTarget;
   
    [Tooltip("Lista de objetos interagiveis proximos")]
    [SerializeField] List<InteractableGameObject> targetsAround = new List<InteractableGameObject>();
    
    [Tooltip("Alcance da checagem")]
    [SerializeField] float radius;
    [SerializeField] LayerMask targetLayer;

    private void Update()
    {
        CaptTargetsAround();
        objTarget = FindFocus();

    }

    void CaptTargetsAround()
    {
        // Limpa a lista de alvos ao redor antes de detectar novos alvos
        targetsAround.Clear();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<InteractableGameObject>(out InteractableGameObject interactableObject))
            {
                targetsAround.Add(interactableObject);
            }
            else
            {
                Debug.Log("Objeto não interativo detectado: " + collider.name);
            }
        }
    }

    InteractableGameObject FindFocus()
    {
        InteractableGameObject objectFocus = null; 
        float minDist = float.MaxValue;

        foreach (var target in targetsAround)
        {
            float distance = Vector2.Distance(target.transform.position, transform.position);
            if (distance < minDist)
            {
                minDist = distance;
                objectFocus = target;
            }
        }


        return objectFocus;
       
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
       /* if (!objTarget)
        {
            Debug.DrawRay(transform.position, objTarget.transform.position, color: Color.green);
        }*/
    }
}
