using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHitable : HitableGameObject, IHitable
{
    public Color hitColor = Color.red; // Cor ao ser atingido
    public Slider sliderVida;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float shakeIntensity = 0.01f; // Intensidade da chacoalhada
    [SerializeField] private int shakeCount = 10; // Número de shakes

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.drag = 5f; // Ajusta o valor de drag para desacelerar o objeto
        }
    }

    public void Hited(int d, Transform i, float stanTime, ItemData itemData)
    {
       
        if (itemData is Weapon)
        {
            health -= d;
            sliderVida.value = health;

            if (spriteRenderer != null)
            {
                StartCoroutine(IEHited(d, i, stanTime));
            }

            if (health <= 0)
            {
                Destroyed();
            }
        }
    }

    private IEnumerator IEHited(int d, Transform i, float stanTime)
    {
        Vector3 originalPosition = transform.position;
        spriteRenderer.color = hitColor; // Troca para a cor de hit

        // Shake effect
        for (int j = 0; j < shakeCount; j++)
        {
            Vector3 shakeOffset = new Vector3(
                Random.Range(-shakeIntensity, shakeIntensity),
                Random.Range(-shakeIntensity, shakeIntensity),
                0
            );
            transform.position = originalPosition + shakeOffset;
            yield return new WaitForSeconds(0.02f); // Pequeno intervalo entre cada shake
        }

        // Retorna à posição original e cor original
        transform.position = originalPosition;

        // Aplica a força de impacto
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            Vector2 forceDir = (transform.position - i.position).normalized;
            rb.velocity = forceDir * d; // Aplica a força ao inimigo
        }

        spriteRenderer.color = originalColor; // Retorna à cor original
        yield return new WaitForSeconds(stanTime); // Tempo de stun
    }

    public void Destroyed()
    {
        foreach (var item in drops)
        {
            ItemsManager.Instance.DropItem(item, 1, transform); // Droppa o item
        }
        Destroy(gameObject, 0.3f); // Destrói o inimigo após 0.3 segundos
    }
}
