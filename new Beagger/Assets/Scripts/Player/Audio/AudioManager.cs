using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Footstep Sounds")]
    public AudioClip[] glassFootstepClips; // Sons de passos no vidro
    public AudioClip[] concreteFootstepClips; // Sons de passos no concreto
    public AudioClip[] dirtFootstepClips; // Sons de passos na terra
    public AudioClip[] woodFootstepClips; // Sons de passos na madeira

    [SerializeField] AudioSource audioSource;

    [Header("Ground Layer Masks")]
    public LayerMask groundGlassLayer;
    public LayerMask groundConcreteLayer;
    public LayerMask groundDirtLayer;
    public LayerMask groundWoodLayer; // Layer de madeira

    [SerializeField] Transform playerTransform; // Transform do jogador para verificar o ch�o

    // Comprimento do Raycast para detectar o ch�o
    private float rayLength = 0.5f;

    // M�todo para tocar sons de passos, chamado pelo evento de anima��o
    public void PlayFootstepSound()
    {
        AudioClip clip = GetFootstepClipByGround();

        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // M�todo para verificar a layer do ch�o e pegar o som correspondente
    private AudioClip GetFootstepClipByGround()
    {
        // Raycast 2D abaixo do jogador para detectar a layer do ch�o
        RaycastHit2D hit = Physics2D.Raycast(playerTransform.position, Vector2.down, rayLength, groundGlassLayer | groundConcreteLayer | groundDirtLayer | groundWoodLayer);

        // Desenha o Raycast na cena para debug
        Debug.DrawRay(playerTransform.position, Vector2.down * rayLength, Color.red);

        if (hit.collider != null)
        {
            // Verifica se o ch�o est� na layer de vidro
            if (((1 << hit.collider.gameObject.layer) & groundGlassLayer) != 0)
            {
                return glassFootstepClips[Random.Range(0, glassFootstepClips.Length)];
            }
            // Verifica se o ch�o est� na layer de concreto
            else if (((1 << hit.collider.gameObject.layer) & groundConcreteLayer) != 0)
            {
                return concreteFootstepClips[Random.Range(0, concreteFootstepClips.Length)];
            }
            // Verifica se o ch�o est� na layer de terra
            else if (((1 << hit.collider.gameObject.layer) & groundDirtLayer) != 0)
            {
                return dirtFootstepClips[Random.Range(0, dirtFootstepClips.Length)];
            }
            // Verifica se o ch�o est� na layer de madeira
            else if (((1 << hit.collider.gameObject.layer) & groundWoodLayer) != 0)
            {
                return woodFootstepClips[Random.Range(0, woodFootstepClips.Length)];
            }
        }
        return null; // Retorna null se nenhuma layer for detectada
    }
}
