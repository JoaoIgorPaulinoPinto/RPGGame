using UnityEngine;

public class InteractionsCapter : MonoBehaviour
{
    [SerializeField] InteractionUIManager InteractionUIManager;
    public PlayerMovementation playerMovimentation;
    public Transform objTarget;

    [Tooltip("Alcance da checagem")]
    [SerializeField] float preLoaderRadius;
    [SerializeField] float distance;
    [SerializeField] float radius;
    [SerializeField] int parts;

    [SerializeField] float YRaysOffset;
    [SerializeField] LayerMask targetLayer;

    void Checker()
    {
        objTarget = null; // Começa assumindo que não há nenhum objeto próximo


        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < parts; i++)
        {
            float angleOffset = i * (radius / parts) * -1;
            Vector2 direction = Quaternion.Euler(0, 0, angleOffset + radius / 2) * playerMovimentation.Direction.normalized;
            Vector2 origin = new Vector2(transform.position.x, transform.position.y + YRaysOffset);

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, distance, targetLayer);
            Debug.DrawLine(origin, origin + direction * distance, hit ? Color.green : Color.red);

            if (hit.transform != null)
            {
                hit.transform.TryGetComponent(out IInteractable hitObj);

                if (hitObj != null)
                {
                    // Verifica se o objeto atingido está mais próximo do jogador
                    float hitDistance = Vector2.Distance(origin, hit.transform.position);
                    if (hitDistance < closestDistance )
                    {
                        closestDistance = hitDistance;
                        objTarget = hit.transform;
                        InteractionUIManager.ShowUI(objTarget.gameObject);
                    }
                }
            }
        }
    }

    private void Update()
    {
        Checker();
        if (!objTarget)
        {
            InteractionUIManager.HideUI();
        }
    }
}
