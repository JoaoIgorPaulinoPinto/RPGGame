using UnityEngine;

public enum ControlType
{
    Joystick,
    Keyboard
}

public class PlayerMovementation : MonoBehaviour
{
    public bool canMove;

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

        if (canMove)
        {
            movement = new Vector2(X, Y).normalized;
            rb.velocity = movement * moveSpeed;


            if (AimSystem.Instance.VerifyRotation() != Vector2.zero)    
            {
                lastDirection = AimSystem.Instance.VerifyRotation();

                /*  
                    animatorController.SetFloat("X", X);
                    animatorController.SetFloat("Y", Y);
                */
            }
        }
        else
        {
           //rb.velocity = Vector2.zero;
        }
    }
}
