using System.Collections;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using UnityEngine;

public class EnemyHitacble : HitableGameObject, IHitable
{
    public Color hitColor = Color.red; // Cor ao ser atingido
    private Color originalColor;
    private SpriteRenderer spriteRenderer;


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

    public void Hited(int d, Transform i, float stanTime)
    {
        health -= d;
       

        if (spriteRenderer != null)
        {
            StartCoroutine(IEHited(d,i,stanTime));
        }

        if (health <= 0)
        {
            Destroyed();
        }
    }
    private IEnumerator IEHited(int d, Transform i, float stanTime)
    {
        TryGetComponent<Rigidbody2D>(out Rigidbody2D rb);
        if (rb)
        {
            audioSource.clip = hitedClip;
            audioSource.Play();
            // Calcula a direção da força (do ponto de impacto para o inimigo)
            Vector2 forceDir = (transform.position - i.position).normalized;

            // Ajusta a velocidade diretamente, criando um "impulso" na direção contrária ao impacto
            rb.velocity = forceDir * d;
        }
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
        yield return new WaitForSeconds(stanTime); // Tempo em que a cor ficará mudada
        rb.velocity = Vector2.zero;

    }

    public void Destroyed()
    {
        foreach (var item in drops)
        {
            ItemsManager.Instance.DropItem(item, 1, transform);
        }
        Destroy(gameObject, 0.3f); // Destroi o objeto com um pequeno delay
    }
}
