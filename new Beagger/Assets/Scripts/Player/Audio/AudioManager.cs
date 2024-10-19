using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Footstep Sounds")]
    public AudioClip[] glassFootstepClips; // Sons de passos no vidro
    public AudioClip[] concreteFootstepClips; // Sons de passos no concreto
    public AudioClip[] dirtFootstepClips; // Sons de passos na terra

    private AudioSource audioSource;

    [Header("Ground Layer Masks")]
    public LayerMask groundGlassLayer;
    public LayerMask groundConcreteLayer;
    public LayerMask groundDirtLayer;

    private Transform playerTransform; // Transform do jogador para verificar o chão

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Atribui o transform do player (certifique-se que o player tem a tag "Player")
    }

    // Método para tocar sons de passos, chamado pelo evento de animação
    public void PlayFootstepSound()
    {
        AudioClip clip = GetFootstepClipByGround();
            
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Método para verificar a layer do chão e pegar o som correspondente
    private AudioClip GetFootstepClipByGround()
    {
        // Raycast 2D abaixo do jogador para detectar a layer do chão
        RaycastHit2D hit = Physics2D.Raycast(playerTransform.position, Vector2.zero);
       
        if (hit.collider != null)
        {
            // Verifica se o chão está na layer de vidro
            if (((1 << hit.collider.gameObject.layer) & groundGlassLayer) != 0)
            {
                return glassFootstepClips[Random.Range(0, glassFootstepClips.Length)];
            }
            // Verifica se o chão está na layer de concreto
            else if (((1 << hit.collider.gameObject.layer) & groundConcreteLayer) != 0)
            {
                return concreteFootstepClips[Random.Range(0, concreteFootstepClips.Length)];
            }
            // Verifica se o chão está na layer de terra
            else if (((1 << hit.collider.gameObject.layer) & groundDirtLayer) != 0)
            {
                return dirtFootstepClips[Random.Range(0, dirtFootstepClips.Length)];
            }
        }
        return null; // Retorna null se nenhuma layer for detectada
    }
}
