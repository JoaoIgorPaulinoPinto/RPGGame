using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHited : HitableGameObject, IHitable
{
    public Color hitColor = Color.red; // Cor ao ser atingido
    private Color originalColor;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float shakeIntensity = 0.1f; // Intensidade do shake
    [SerializeField] private int shakeCount = 10; // Número de shakes

    void Start()
    {
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
            PlayerStts.Instance.heath -= d;

            StartCoroutine(IEHited(d, i, stanTime));
            if (health <= 0)
            {
                Destroyed();
            }
        }
    }

    private IEnumerator IEHited(int d, Transform i, float stanTime)
    {
        Vector3 originalPosition = transform.position; // Posição inicial para o efeito de shake

        TryGetComponent<Rigidbody2D>(out Rigidbody2D rb);
        if (rb)
        {
            audioSource.clip = hitedClip;
            audioSource.Play();

            PlayerControlsManager.Instance.realease = false;

            // Calcula a direção da força (do ponto de impacto para o jogador)
            Vector2 forceDir = (transform.position - i.position).normalized;

            // Ajuste a força do impacto para um efeito de "jogar mais longe"
            float forceMultiplier = 10f; // Aumente este valor para intensificar o impulso
            rb.velocity = forceDir * d * forceMultiplier; // Aumenta o valor para impulsionar o jogador
        }

        spriteRenderer.color = hitColor; // Altera a cor para a cor de hit

        // Shake effect
        for (int o = 0; o < shakeCount; o++)
        {
            Vector3 shakeOffset = new Vector3(
                Random.Range(-shakeIntensity, shakeIntensity),
                Random.Range(-shakeIntensity, shakeIntensity),
                0
            );
            transform.position = originalPosition + shakeOffset; // Move o jogador de forma aleatória
            yield return new WaitForSeconds(0.02f); // Pequeno intervalo entre cada shake
        }

        // Retorna à posição original após o shake
        transform.position = originalPosition;

        spriteRenderer.color = originalColor; // Retorna à cor original
        yield return new WaitForSeconds(0.15f); // Duração da cor alterada

        // Espera o tempo de stun
        yield return new WaitForSeconds(stanTime);

        // Reseta a velocidade do Rigidbody
        rb.velocity = Vector2.zero;

        PlayerControlsManager.Instance.realease = true;
    }

    public void Destroyed()
    {
        print("Game Over!!!");
        PlayerStts.Instance.PlayerDied(); // Chama a função de morte do jogador
    }
}
