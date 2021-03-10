using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CommandProcessor))]
public class PlayerBehaviour : MonoBehaviour, IEntity {

    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float xBound = 6.9f;

    private CommandProcessor commandProcessor;
    private Vector2 currentMove;
    private bool moving;


    Rigidbody2D IEntity.rb => null;

    // cache resources
    public void Awake() {
        commandProcessor = GetComponent<CommandProcessor>();
    }

    // initialise values
    public void Start() {
        currentMove = Vector2.zero;
        moving = false;
    }

    public void Update() {
        if (moving) {
            commandProcessor.Execute(new MoveCommand(this, currentMove, movementSpeed * Time.deltaTime));

            // ensure that paddle is within bounds of the screen via code (not using RBs - read notes)
            if(transform.position.x >= xBound) {
                transform.position = new Vector2(xBound, transform.position.y);
            }else if(transform.position.x <= -xBound) {
                transform.position = new Vector2(-xBound, transform.position.y);
            }
        }
    }

    public void Move(InputAction.CallbackContext context) {
        currentMove = context.ReadValue<Vector2>();
        currentMove.y = 0;

        if (context.started) {
            moving = true;
        } else if (context.canceled) {
            currentMove = Vector2.zero;
            moving = false;
        }
    }



}