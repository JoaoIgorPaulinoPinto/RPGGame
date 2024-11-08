using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHitable : HitableGameObject, IHitable
{
    public Color hitColor = Color.red; // Cor ao ser atingido
    private Color alternateColor = Color.white; // Cor alternada (branco)
    private SpriteRenderer spriteRenderer;

    [SerializeField] private GameObject toco;
    [SerializeField] private float shakeIntensity = 0.1f; // Intensidade da chacoalhada
    [SerializeField] private int shakeCount = 10; // Número de vezes que o shake ocorre

    private bool isDestroyed = false; // Para evitar chamadas repetidas de Destroyed()

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Hited(int damage, Transform i, float stanTime, ItemData itemData)
    {
        
        if (itemData is Tool)
        {
            Tool item = itemData as Tool;
            if(item.toolType == ToolType.Axe)
            {
                if (isDestroyed) return; // Evita execução se a árvore já está destruída

                audioSource.clip = hitedClip;
                audioSource.Play();
                health -= damage;

                if (spriteRenderer != null)
                {
                    StartCoroutine(IEHited(stanTime));
                }

                if (health <= 0 && !isDestroyed)
                {
                    isDestroyed = true;
                    Destroyed();
                }
            }
        }
    }

    private IEnumerator IEHited(float stanTime)
    {
        Vector3 originalPosition = transform.position;

        // Muda para a cor de hit
        spriteRenderer.color = hitColor;

        // Shake effect
        for (int i = 0; i < shakeCount; i++)
        {
            Vector3 shakeOffset = new Vector3(
                Random.Range(-shakeIntensity, shakeIntensity),
                Random.Range(-shakeIntensity, shakeIntensity),
                0
            );
            transform.position = originalPosition + shakeOffset;
            yield return new WaitForSeconds(0.02f); // Pequeno intervalo entre cada shake
        }

        // Retorna à posição original e cor alternada (branco)
        transform.position = originalPosition;
        spriteRenderer.color = alternateColor;

        // Tempo de stun
        yield return new WaitForSeconds(stanTime);
    }

    public void Destroyed()
    {
        if(drops.Count > 0)
        {
            int numberOfDrops = Random.Range(1, drops.Count + 1);
            var randomItem = drops[Random.Range(0, drops.Count)];
            ItemsManager.Instance.DropItem(randomItem, numberOfDrops, transform);

            // Instancia o toco antes de destruir a árvore
            Instantiate(toco, transform.position, Quaternion.identity);

            // Aguarda um tempo curto para finalizar efeitos antes de destruir
            Destroy(gameObject, 0.3f);
        }
      
    }
}
