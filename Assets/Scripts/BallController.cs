using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform player;
    [SerializeField] private GameManager gameManager;

    private bool inMotion;
    private Rigidbody2D rb;

    public void Awake() {
        rb = GetComponent<Rigidbody2D>();

        // ensure that the user doesn't change these settings in the editor...
        // this is needed because we deal with our own ball physics via code!
        rb.isKinematic = true;
        rb.useFullKinematicContacts = true;

        inMotion = false;
    }

    void Update() {
        if (!inMotion) {
            // stick the ball to the player paddle
            transform.position = new Vector2(player.position.x, player.position.y + 0.5f);
        } else {
            // Enforce that the ball's speed is constant
            if (rb.velocity.magnitude != speed) rb.velocity = rb.velocity * speed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        // if hits a wall then reflect normally
        if (collision.gameObject.tag == "Wall") {
            rb.velocity = Vector2.Reflect(rb.velocity, collision.contacts[0].normal);
        }

        // if hits the player paddle then reflect differently based upon left/right/middle
        else if (collision.gameObject.tag == "Player") {
            Vector3 playerPos = collision.gameObject.transform.position;
            Vector3 ballPos = this.transform.position;

            // middle
            if (ballPos.x >= playerPos.x - 0.25 && ballPos.x <= playerPos.x + 0.25) {
                rb.velocity = Vector2.Reflect(rb.velocity, collision.contacts[0].normal);

            // left/right
            } else {
                // replace this...
                rb.velocity = Vector2.Reflect(rb.velocity, collision.contacts[0].normal);
            }
        }

        // if hits a destroyable object, deflect and then destroy it!
        else if(collision.gameObject.tag == "Destroyable") {
            rb.velocity = Vector2.Reflect(rb.velocity, collision.contacts[0].normal);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Bottom") {
            rb.velocity = Vector2.zero;
            inMotion = false;
            gameManager.removeLife();
        }
    }

    public void Fire(InputAction.CallbackContext context) {
        // fire the ball in a random direction
        if (!inMotion) {
            // limit the angle to between 20 and 160 degrees from the paddle so that it doesn't
            // start off going too horizontal or even below the paddle...
            float angle = Random.Range(20, 160); 
            rb.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * speed;
            inMotion = true;
        }
    }

}