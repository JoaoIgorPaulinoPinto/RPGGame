using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AimSystem : MonoBehaviour
{
    public static AimSystem Instance { get; set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    [SerializeField] float maxDistance; // Dist�ncia m�xima da arma ao piv�
    [SerializeField] float Ymultplyer; // Dist�ncia m�xima da arma ao piv�
    [SerializeField] Transform pivot;
    [SerializeField] public Transform gun;

    [Tooltip("Alcance da checagem")]
    [SerializeField] float distance;
    [SerializeField] float radius;
    [SerializeField] int parts;

    [SerializeField] LayerMask targetLayer;

    private void Update()
    {
        RotateTowardsMouse();
        AdjustGunDistance();
    }

    private void RotateTowardsMouse()
    {
        Vector2 direction = VerifyRotation();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        pivot.rotation = Quaternion.Euler(0, 0, angle);

    }
    private void AdjustGunDistance()
    {
        // Dist�ncia do piv� at� a posi��o Y zero (eixo horizontal)
        float pivotDistanceFromCenter = Mathf.Abs(VerifyRotation().y * Ymultplyer);

        // Calcula a nova dist�ncia da arma com base na posi��o Y do piv�
        float newDistance = Mathf.Lerp(maxDistance, 0, pivotDistanceFromCenter / 5f); // 5f pode ser ajustado conforme necess�rio

        // Ajusta a posi��o da arma em rela��o ao piv�
        Vector3 realNewPos = new Vector3(newDistance, gun.localPosition.y, gun.localPosition.z);
        gun.localPosition = realNewPos;

        gun.LookAt(pivot);

    }
    public Transform[] Check()
    {
        List<Transform> hitTargets = new List<Transform>();

        for (int i = 0; i < parts; i++)
        {
            float angleOffset = i * (radius / parts) * -1;
            Vector2 direction = Quaternion.Euler(0, 0, angleOffset + radius / 2) * VerifyRotation();
            Vector2 origin = new Vector2(transform.position.x, transform.position.y);

            // Usando RaycastAll para detectar m�ltiplos alvos
            RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction, distance, targetLayer);
            Debug.DrawLine(origin, origin + direction * distance, hits.Length > 0 ? Color.green : Color.red);

            foreach (var hit in hits)
            {
                if (hit.transform != null  && hitTargets.Contains(hit.transform) == false)
                {
                    print(hit.transform.name);
                    hitTargets.Add(hit.transform); // Armazena o alvo atingido
                }
            }
        }

        return hitTargets.ToArray(); // Retorna todos os alvos detectados
    }


    public Vector2 VerifyRotation() 
    {
        if (PlayerControlsManager.Instance.realease)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - (Vector2)transform.position;
            return direction.normalized;
        }
        return Vector2.zero;
    }
}
