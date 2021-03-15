using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Brick : MonoBehaviour {

    [SerializeField] private int maxHealth = 1;
    [SerializeField] private Sprite[] sprites = new Sprite[3];

    private int health;
    private SpriteRenderer spriteRenderer;
    private Player player;

    // cache resources
    public void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Game Manager").GetComponent<EntityManager>().getPlayer();
    }

    // initialise values
    public void Start() {
        health = maxHealth;
        spriteRenderer.sprite = sprites[0];
    }

    public void hit() {
        health -= 1;

        if (health <= 0) {
            player.setScore(player.getScore() + 1);
            Destroy(this.gameObject);
        } else {
            // change to third broken / two thirds broken / ... sprites.
            int step = maxHealth / 3;
            if (health == maxHealth - step) {
                spriteRenderer.sprite = sprites[1];
            } else if (health == maxHealth - (2 * step)) {
                spriteRenderer.sprite = sprites[2];
            }
        }
    }
}
