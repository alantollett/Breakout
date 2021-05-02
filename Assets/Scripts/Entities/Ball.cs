using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour, IEntity {

    [SerializeField] private float movementSpeed = 10f;

    public static event System.Action OnBallDeath;

    private Vector2 ballVelocityBeforePause = Vector2.zero;
    private Rigidbody2D rb;

    Rigidbody2D IEntity.rb => rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
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
        ballVelocityBeforePause = new Vector2(rb.velocity.x, rb.velocity.y);
        rb.velocity = Vector2.zero;
    }

    private void Resume() {
        rb.velocity = ballVelocityBeforePause;
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bottom") {
            Destroy(this);
            OnBallDeath?.Invoke();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Brick") {
            collision.gameObject.GetComponent<Brick>().hit();
        }
    }
    public float getMovementSpeed() {
        return movementSpeed;
    }

    public bool isMoving() {
        return rb.velocity != Vector2.zero;
    }
}

