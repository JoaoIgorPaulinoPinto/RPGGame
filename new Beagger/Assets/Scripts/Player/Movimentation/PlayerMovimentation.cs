using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public enum ControlType
{
    Joystick,
    Keyboard
}

public class PlayerMovementation : MonoBehaviour
{


    [SerializeField] ParticleSystem walkParticle;
    public bool canMove;

    [SerializeField] private ControlType controlType;
    [SerializeField] public Animator animatorController;
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    [SerializeField] private Vector2 movement;
    [SerializeField] private Vector2 lastDirection; // Armazena a última direção válida

    [SerializeField] public Vector2 Direction => lastDirection.normalized; // Propriedade para acessar a direção normalizada

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void FixedUpdate()
    {
        //  Particle();
        Move();
        if (EquipedItemsManager.Instance.prefab)
        {
            EquipedItemsManager.Instance.prefab.TryGetComponent<SpriteRenderer>(out SpriteRenderer spRender);

            if (Direction.y > 0)
            {

                spRender.sortingOrder = 2;
            }
            else
            {
                spRender.sortingOrder = 3;
            }
        }

    }

    void Move()
    {

        float X = Input.GetAxis("Horizontal");

        float Y = Input.GetAxis("Vertical");

        if (canMove)
        {
            movement = new Vector2(X, Y).normalized;
            rb.velocity = movement * moveSpeed;


            if (AimSystem.Instance.VerifyRotation() != Vector2.zero)
            {
                lastDirection = AimSystem.Instance.VerifyRotation();


                animatorController.SetFloat("X", X);
                animatorController.SetFloat("Y", Y);

            }
        }
        else
        {
            //rb.velocity = Vector2.zero;
        }
    }/*
    void Particle()
    {
        // Verifica se há movimento nos eixos X ou Y
        if (Mathf.Abs(movement.x) > 0.1f || Mathf.Abs(movement.y) > 0.1f)
        {
            if (!walkParticle.isPlaying)
            {
                walkParticle.Play();  // Inicia a fumaça
            }
        }
        else
        {
            if (walkParticle.isPlaying)
            {
                walkParticle.Stop();  // Para a fumaça quando o jogador para
            }
        }
    }*/

}
