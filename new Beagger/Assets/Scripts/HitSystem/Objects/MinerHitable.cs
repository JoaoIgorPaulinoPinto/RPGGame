using System.Collections;
using UnityEngine;

public class MinerHitable : HitableGameObject, IHitable
{
    public Color hitColor = Color.red;
    private Color alternateColor = Color.white;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float shakeIntensity = 0.1f; // Intensidade do shake
    [SerializeField] private int shakeCount = 10; // Número de shakes

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Hited(int damage, Transform i, float stanTime, ItemData itemData)
    {
        if (itemData is Tool)
        {
            Tool item = itemData as Tool;
            if (item.toolType == ToolType.Picaxe)
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
        Vector3 originalPosition = transform.position;
        spriteRenderer.color = hitColor;

        for (int i = 0; i < shakeCount; i++)
        {
            Vector3 shakeOffset = new Vector3(
                Random.Range(-shakeIntensity, shakeIntensity),
                Random.Range(-shakeIntensity, shakeIntensity),
                0
            );
            transform.position = originalPosition + shakeOffset;
            yield return new WaitForSeconds(0.02f);
        }

        transform.position = originalPosition;
        spriteRenderer.color = alternateColor;
        yield return new WaitForSeconds(stanTime);
    }

    public void Destroyed()
    {
        int numberOfDrops = Random.Range(1, drops.Count + 1);
        var randomItem = drops[Random.Range(0, drops.Count)];
        ItemsManager.Instance.DropItem(randomItem, numberOfDrops, transform);
        Destroy(gameObject, 0.3f);
    }
}
