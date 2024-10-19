using System.Collections;
using UnityEngine;

public class Tree : HitableGameObject, IHitable
{
    public Color hitColor = Color.red; // Cor ao ser atingido
    private Color alternateColor = Color.white; // Cor alternada (branco)
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Hited(int damage, Transform i, float stanTime)
    {
        health -= damage;
        if (spriteRenderer != null)
        {
            StartCoroutine(IEHited(stanTime));
        }
        if (health <= 0)
        {
            Destroyed();
        }
    }

    private IEnumerator IEHited(float stanTime)
    {
        
        spriteRenderer.color = hitColor; // Troca para a cor de hit
        yield return new WaitForSeconds(0.1f); // Espera
        spriteRenderer.color = alternateColor; // Troca para a cor alternada (branco)
        yield return new WaitForSeconds(stanTime); // Tempo de stun
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
