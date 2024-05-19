using UnityEngine;
enum controlType
{
    joysctick,
    keyboard
}
public class PlayerMovimentation : MonoBehaviour
{
    [SerializeField]controlType controlType;

    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    public Joystick joystick;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        if(controlType == controlType.keyboard)
        {
            movement = new Vector2(moveX, moveY).normalized;
        }
        else if(controlType == controlType.joysctick)
        {
            movement = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;
        }
       
    }

    void FixedUpdate()
    {
        // Aplica o movimento ao Rigidbody2D
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
