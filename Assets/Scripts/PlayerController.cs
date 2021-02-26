using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 4000f;
    private Rigidbody2D rb;
    private Vector2 currentMove;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        currentMove = context.ReadValue<Vector2>();
    }

    public void Update()
    {
        Vector2 velocity = rb.velocity;
        velocity.x = currentMove.x * movementSpeed * Time.deltaTime;
        rb.velocity = velocity;
    }
}
