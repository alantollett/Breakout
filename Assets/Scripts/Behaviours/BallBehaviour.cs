using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(CommandProcessor))]
[RequireComponent(typeof(Rigidbody2D))]
public class BallBehaviour : MonoBehaviour, IEntity {

    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject player;

    private CommandProcessor commandProcessor; 
    private Rigidbody2D rb;

    private bool moving;

    Rigidbody2D IEntity.rb => rb;

    // cache resources
    public void Awake() {
        commandProcessor = GetComponent<CommandProcessor>();
        rb = GetComponent<Rigidbody2D>();
    }

    // initialise values
    public void Start() {
        moving = false;
    }

    public void Update() {
        if (!moving) {
            // stick the ball to the player paddle
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.5f);
        } else {
            // Enforce that the ball's speed is constant
            if (rb.velocity.magnitude != speed) rb.velocity = rb.velocity * speed;
        }
    }

    public void Fire(InputAction.CallbackContext context) {
        if (context.started && !moving) {
            // fire the ball at an angle between 20 and 160 degrees to ensure that it
            // does not start off going too horizontal or even below the paddle...
            commandProcessor.Execute(new FireCommand(this, Random.Range(89, 91), speed));
            moving = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bottom") {
            rb.velocity = Vector2.zero;
            moving = false;
            //gameManager.removeLife();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Brick") {
            //rb.velocity = Vector2.Reflect(rb.velocity, collision.contacts[0].normal);
            collision.gameObject.GetComponent<BrickBehaviour>().hit();
        }
    }
}
