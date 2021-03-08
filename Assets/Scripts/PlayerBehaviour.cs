using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CommandProcessor))]

public class PlayerBehaviour : MonoBehaviour, IEntity {

    [SerializeField] private float movementSpeed = 10f;

    private CommandProcessor commandProcessor;
    private Vector2 currentMove;
    private bool moving;

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
        }
    }

    public void Move(InputAction.CallbackContext context) {
        currentMove = context.ReadValue<Vector2>();

        if (context.started) {
            moving = true;
        } else if (context.canceled) {
            currentMove = Vector2.zero;
            moving = false;
        }
    }
}

