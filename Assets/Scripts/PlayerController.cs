using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    private Vector2 currentMove;

    public void Move(InputAction.CallbackContext context)
    {
        currentMove = context.ReadValue<Vector2>();
    }

    public void Update()
    {
        Vector3 moveVelocity = movementSpeed * (currentMove.x * Vector3.right + currentMove.y * Vector3.forward);
        Vector3 moveThisFrame = Time.deltaTime * moveVelocity;
        transform.position += moveThisFrame;
    }
}
