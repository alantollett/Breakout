using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CommandProcessor))]
public class InputManager : MonoBehaviour {

    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float paddleXBound = 7.2f;

    private Player player;
    private CommandProcessor commandProcessor;
    private LevelManager levelManager;

    private Vector2 currentMove;
    private bool moving;
    private Ball ball;

    public void Awake() {
        player = GetComponent<Player>();
        commandProcessor = GetComponent<CommandProcessor>();
        levelManager = GameObject.Find("Game Manager").GetComponent<LevelManager>();
        ball = GameObject.Find("Game Manager").GetComponent<EntityManager>().getBall();
    }

    public void Start() {
        currentMove = Vector2.zero;
        moving = false;
    }

    public void Update() {
        if (moving) {
            int frame = levelManager.getFrame();
            commandProcessor.Execute(new MoveCommand(player, frame, currentMove, movementSpeed * Time.deltaTime));

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

        if (context.started) {
            moving = true;
        } else if (context.canceled) {
            currentMove = Vector2.zero;
            moving = false;
        }
    }
    public void Fire(InputAction.CallbackContext context) {
        if (context.started && !ball.isMoving()) {
            // fire the ball at an angle between 20 and 160 degrees to ensure that it
            // does not start off going too horizontal or even below the paddle...
            int frame = levelManager.getFrame();
            commandProcessor.Execute(new FireCommand(ball, frame, Random.Range(89, 91), ball.getMovementSpeed()));
            ball.setMoving(true);
        }
    }
}
