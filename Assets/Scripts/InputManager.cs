using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CommandProcessor))]
[RequireComponent(typeof(Player))]
public class InputManager : MonoBehaviour {

    [SerializeField] private LevelManager levelManager;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private float paddleMovementSpeed = 10f;
    [SerializeField] private float paddleXBound = 7.2f;

    private CommandProcessor commandProcessor;
    private Player player;

    private Vector2 currentMove;
    private bool moving;

    private void Awake() {
        commandProcessor = GetComponent<CommandProcessor>();
        player = GetComponent<Player>();
    }

    private void Start() {
        currentMove = Vector2.zero;
        moving = false;
    }

    public void Update() {
        UpdatePlayerPosition();
    }

    private void UpdatePlayerPosition() {
        if (moving && !levelManager.isPaused()) {
            int frame = levelManager.getFrame();
            commandProcessor.Execute(new MoveCommand(player, frame, currentMove, paddleMovementSpeed * Time.deltaTime));

            // ensure that paddle is within bounds of the screen via code (not using RBs - read notes)
            if (transform.position.x >= paddleXBound) {
                transform.position = new Vector2(paddleXBound, transform.position.y);
            } else if (transform.position.x <= -paddleXBound) {
                transform.position = new Vector2(-paddleXBound, transform.position.y);
            }
        }
    }

    public void Move(InputAction.CallbackContext context) {
        currentMove = context.ReadValue<Vector2>();
        currentMove.y = 0;

        if (context.started && !levelManager.isPaused()) {
            moving = true;
        } else if (context.canceled) {
            currentMove = Vector2.zero;
            moving = false;
        }

        if (tutorialManager != null) tutorialManager.setMessageIndex(2);
    }
    public void Fire(InputAction.CallbackContext context) {
        if (context.started && !levelManager.isPaused() && !player.GetBall().isMoving()) {
            // fire the ball at an angle between 20 and 160 degrees to ensure that it
            // does not start off going too horizontal or even below the paddle...
            int frame = levelManager.getFrame();
            Ball ball = player.GetBall();
            float movementSpeed = ball.getMovementSpeed();
            commandProcessor.Execute(new FireCommand(ball, frame, Random.Range(89, 91), movementSpeed));
        }

        if (tutorialManager != null) tutorialManager.setMessageIndex(3);
    }

    public void Pause(InputAction.CallbackContext context) {
        if (levelManager != null && context.started && !levelManager.isPaused()) {
            levelManager.pause();
            menuManager.setPauseMenuVisible(true);
        }
    }

}
