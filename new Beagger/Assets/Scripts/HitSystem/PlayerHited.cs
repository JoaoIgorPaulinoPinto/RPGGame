using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class PlayerHited : HitableGameObject , IHitable
{

    public Color hitColor = Color.red; // Cor ao ser atingido
    private Color originalColor;
    [SerializeField] SpriteRenderer spriteRenderer;

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

    public void Hited(int d, Transform i, float stanTime)
    {
        health -= d;
        PlayerStts.Instance.heath -= d;

        StartCoroutine(IEHited(d, i, stanTime));
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
            PlayerControlsManager.Instance.realease = false;

            // Calcula a direção da força (do ponto de impacto para o inimigo)
            Vector2 forceDir = (transform.position - i.position).normalized;

            // Ajusta a velocidade diretamente, criando um "impulso" na direção contrária ao impacto
            rb.velocity = forceDir * d * 2;
        }
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(0.15f);
        spriteRenderer.color = originalColor;
        yield return new WaitForSeconds(stanTime); // Tempo em que a cor ficará mudada
       
        rb.velocity = Vector2.zero;

        PlayerControlsManager.Instance.realease = true;
    }

    public void Destroyed()
    {
        print("Game Over!!!");
        PlayerStts.Instance.PlayerDied();
    }
}
