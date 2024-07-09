using UnityEngine;

public enum ControlType
{
    Joystick,
    Keyboard
}

public class PlayerMovementation : MonoBehaviour
{
    [SerializeField] private ControlType controlType;
    [SerializeField] private Animator animatorController;
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
        Move();
    }

    void Move()
    {
        float X = Input.GetAxis("Horizontal");
        float Y = Input.GetAxis("Vertical");
        movement = new Vector2(X, Y).normalized * moveSpeed;
        rb.velocity = movement;

        if (X != 0 || Y != 0)
        {
            // Se houver movimento, atualiza movement e lastDirection
            lastDirection = movement;

            animatorController.SetFloat("X", X);
            animatorController.SetFloat("Y", Y);


            if (X < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (X > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
}
