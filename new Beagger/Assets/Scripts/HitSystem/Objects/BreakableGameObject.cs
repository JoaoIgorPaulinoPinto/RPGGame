using System.Collections;
using UnityEngine;

public class BreakableGameObject : HitableGameObject, IHitable
{
    public Color hitColor = Color.red; // Cor ao ser atingido
    private Color alternateColor = Color.white; // Cor alternada (branco)
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Hited(int damage, Transform i, float stanTime, ItemData itemData)
    {
        if (itemData is Tool)
        {
            Tool item = itemData as Tool;
            if (item.toolType == ToolType.Axe)
            {
                audioSource.clip = hitedClip;
                audioSource.Play();
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
        int numberOfDrops = Random.Range(1, drops.Count + 1);
        var randomItem = drops[Random.Range(0, drops.Count)];
        ItemsManager.Instance.DropItem(randomItem, numberOfDrops, transform);
        Destroy(gameObject, 0.3f); 
    }
}
