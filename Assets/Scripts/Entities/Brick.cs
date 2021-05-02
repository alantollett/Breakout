using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Brick : MonoBehaviour {

    [SerializeField] private int health = 1;
    [SerializeField] private int points = 1;
    private int maxHealth;

    // remove sprites to another component
    [SerializeField] private Sprite[] sprites = new Sprite[3];
    [SerializeField] private LevelManager levelManager;
    private SpriteRenderer spriteRenderer;

     
    public static event System.Action<int> OnBrickBreak;


    // cache resources
    public void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // initialise values
    public void Start() {
        spriteRenderer.sprite = sprites[0];
        maxHealth = health;
    }

    public void hit() {
        health -= 1;

        if (health <= 0) {
            Destroy(this.gameObject);
            OnBrickBreak?.Invoke(points);
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
