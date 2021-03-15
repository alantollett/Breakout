using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour, IEntity {

    [SerializeField] private float movementSpeed = 10f;

    private Player player;
    private Rigidbody2D rb;
    private bool moving;

    Rigidbody2D IEntity.rb => rb;

    // cache resources
    public void Awake() {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Game Manager").GetComponent<EntityManager>().getPlayer();
    }

    // deal with movement
    public void Update() {
        if (!moving) {
            // stick the ball to the player paddle
            transform.position = new Vector2(player.transform.position.x, player.transform.position.y + 0.4f);
        } else {
            // Enforce that the ball's speed is constant
            if (rb.velocity.magnitude != movementSpeed) rb.velocity = rb.velocity * movementSpeed;
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

    /*
     * GETTERS AND SETTERS
     */

    public bool isMoving() { return moving; }
    public float getMovementSpeed() { return movementSpeed; }
    public void setMoving(bool moving) { this.moving = moving;  }


}
