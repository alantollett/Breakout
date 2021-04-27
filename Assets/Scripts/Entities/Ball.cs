using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour, IEntity {

    [SerializeField] private float movementSpeed = 10f;
    private CommandProcessor commandProcessor;

    private LevelManager levelManager;
    private Vector2 ballVelocityBeforePause = Vector2.zero;
    private Player player;
    private Rigidbody2D rb;
    private bool moving;

    Rigidbody2D IEntity.rb => rb;

    // cache resources
    public void Awake() {
        rb = GetComponent<Rigidbody2D>();
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();

        // check for null as in menus level manager doesnt exist
        if(levelManager != null) {
            player = levelManager.getPlayer();
            commandProcessor = levelManager.GetComponent<CommandProcessor>();
        }
    }

    public void Update() {
        // check if level manager exists (if a menu ignore the balls)
        if(levelManager != null) {
            // update ball's position
            if (!moving) {
                // stick the ball to the player paddle
                transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.4f);
            } else if (!levelManager.isPaused()) {
                // unpause if was paused
                if (ballVelocityBeforePause != Vector2.zero) {
                    rb.velocity = ballVelocityBeforePause;
                    ballVelocityBeforePause = Vector2.zero;
                }
            }
        }
    }

    public void fire(InputAction.CallbackContext context) {
        if (context.started && !levelManager.isPaused() && !moving) {
            // fire the ball at an angle between 20 and 160 degrees to ensure that it
            // does not start off going too horizontal or even below the paddle...
            int frame = levelManager.getFrame();
            commandProcessor.Execute(new FireCommand(this, frame, Random.Range(89, 91), movementSpeed));
            moving = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bottom") {
            rb.velocity = Vector2.zero;
            moving = false;
            player.setLives(player.getLives() - 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Brick") {
            collision.gameObject.GetComponent<Brick>().hit();
        }
    }

    public bool isMoving() { 
        return moving; 
    }
    public float getMovementSpeed() { 
        return movementSpeed; 
    }
    public void setMoving(bool moving) { 
        this.moving = moving;  
    }
    public void pause() {
        if(ballVelocityBeforePause == Vector2.zero) {
            ballVelocityBeforePause = new Vector2(rb.velocity.x, rb.velocity.y);
            rb.velocity = Vector2.zero;
        }
    }
}

