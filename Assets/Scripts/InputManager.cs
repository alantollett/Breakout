using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CommandProcessor))]
[RequireComponent(typeof(Player))]
public class InputManager : MonoBehaviour {

    [SerializeField] private float paddleMovementSpeed = 10f;
    [SerializeField] private float paddleXBound = 6.85f;

    public static event System.Action OnMove;
    public static event System.Action OnFire;

    private CommandProcessor commandProcessor;
    private RecordingManager recordingManager;
    private Player player;

    private Vector2 currentMove;
    private bool moving;
    private bool paused;

    private void Awake() {
        recordingManager = FindObjectOfType<RecordingManager>();
        commandProcessor = GetComponent<CommandProcessor>();
        player = GetComponent<Player>();
    }

    private void OnEnable() {
        LevelManager.OnLevelPause += Pause;
        LevelManager.OnLevelResume += Resume;
    }

    private void OnDisable() {
        LevelManager.OnLevelPause -= Pause;
        LevelManager.OnLevelResume -= Resume;
    }

    private void Pause(bool _) {
        paused = true;
    }
    private void Resume() {
        paused = false;
    }

    private void Update() {
        UpdatePlayerPosition();
    }

    private void UpdatePlayerPosition() {
        if (!paused) {
            if (moving && recordingManager != null) {
                int frame = recordingManager.getFrame();
                commandProcessor.Execute(new MoveCommand(player, frame, currentMove, paddleMovementSpeed * Time.deltaTime));

                
                // ensure that paddle is within bounds of the screen via code (not using RBs - read notes)
                if (transform.position.x >= paddleXBound) {
                    transform.position = new Vector2(paddleXBound, transform.position.y);
                } else if (transform.position.x <= -paddleXBound) {
                    transform.position = new Vector2(-paddleXBound, transform.position.y);
                }

                OnMove?.Invoke();
            }
        }
    }

    public void Move(InputAction.CallbackContext context) {
        currentMove = context.ReadValue<Vector2>();
        currentMove.y = 0;

        if (context.started && !paused) {
            moving = true;
        } else if (context.canceled) {
            currentMove = Vector2.zero;
            moving = false;
        }
    }

    public void Fire(InputAction.CallbackContext context) {
        Ball ball = FindObjectOfType<Ball>();

        if (context.started && !paused && recordingManager != null && ball != null && !ball.isMoving()) {
            // fire the ball at an angle between 20 and 160 degrees to ensure that it
            // does not start off going too horizontal or even below the paddle...
            int frame = recordingManager.getFrame();
            float movementSpeed = ball.getMovementSpeed();
            commandProcessor.Execute(new FireCommand(ball, frame, Random.Range(89, 91), movementSpeed));
            OnFire?.Invoke();
        }
    }

}
