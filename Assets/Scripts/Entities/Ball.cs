using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour, IEntity {

    [SerializeField] private float movementSpeed = 10f;

    private LevelManager levelManager;
    private Vector2 ballVelocityBeforePause = Vector2.zero;
    private Player player;
    private Rigidbody2D rb;

    Rigidbody2D IEntity.rb => rb;

    // cache resources
    public void Awake() {
        rb = GetComponent<Rigidbody2D>();
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    public void Update() {
        // check if level manager exists (if a menu ignore the balls)
        if(levelManager != null) {
            if (levelManager.isPaused() && ballVelocityBeforePause == Vector2.zero) {
                pause();
            } else if(!levelManager.isPaused()){
                if (ballVelocityBeforePause != Vector2.zero) {
                    rb.velocity = ballVelocityBeforePause;
                    ballVelocityBeforePause = Vector2.zero;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bottom") {
            player.setLives(player.getLives() - 1);
            Destroy(this);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Brick") {
            collision.gameObject.GetComponent<Brick>().hit();
        }
    }

    public bool isMoving() { 
        return rb.velocity != Vector2.zero; 
    }

    public float getMovementSpeed() { 
        return movementSpeed; 
    }
    public void pause() {
        if(ballVelocityBeforePause == Vector2.zero) {
            ballVelocityBeforePause = new Vector2(rb.velocity.x, rb.velocity.y);
            rb.velocity = Vector2.zero;
        }
    }

    public void setPlayer(Player player) {
        this.player = player;
    }
}

