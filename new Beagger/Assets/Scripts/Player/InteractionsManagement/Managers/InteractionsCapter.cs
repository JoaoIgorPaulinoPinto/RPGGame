using UnityEngine;

public class InteractionsCapter : MonoBehaviour
{
    public PlayerMovementation playerMovimentation;

    [Tooltip("Foco, objeto interagível mais próximo do jogador")]
    public InteractableGameObject objTarget;
    public Collider2D[] objtsAround;

    [Tooltip("Alcance da checagem")]
    [SerializeField] float preLoaderRadius;
    [SerializeField] float distance;
    [SerializeField] float radius;
    [SerializeField] int parts; 
   
    [SerializeField] LayerMask targetLayer;
    void Checker()
    {
        objtsAround = Physics2D.OverlapCircleAll(transform.position, preLoaderRadius, targetLayer);

        foreach (var obj in objtsAround)
        {
            obj.GetComponent<InteractableGameObject>().isOnFocus = false;
        }

        for (int i = 0; i < parts; i++)
        {
            float angleOffset = i * (radius / parts) * -1; 
            Vector2 direction = Quaternion.Euler(0, 0, angleOffset + radius / 2) * playerMovimentation.Direction.normalized;

           Vector2 origin = (Vector2)transform.position;


            RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, targetLayer);
            Debug.DrawLine(origin, origin + direction * distance, Color.red);

            if (hit.transform != null)
            {
                Debug.DrawLine(origin, origin + direction * distance, Color.green);


                InteractableGameObject hitObj = hit.transform.GetComponent<InteractableGameObject>();
                if (hitObj != null)
                {
                    hitObj.isOnFocus = true;
                    objTarget = hitObj;
                    print(hitObj.name);
                }
            }
        }
    }
    private void Update()
    {
        Checker();
    }
}
