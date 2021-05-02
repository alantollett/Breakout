using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Brick : MonoBehaviour {

    [SerializeField] private int health = 1;
    [SerializeField] private int points = 1;
    [SerializeField] private Sprite[] sprites = new Sprite[3];

    public static event System.Action<int> OnBrickBreak; // int = points brick is worth
    private SpriteRenderer spriteRenderer;
    private int maxHealth;


    public void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

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
            // change sprite depending on health
            int step = maxHealth / 3;
            if (health == maxHealth - step) {
                spriteRenderer.sprite = sprites[1];
            } else if (health == maxHealth - (2 * step)) {
                spriteRenderer.sprite = sprites[2];
            }
        }
    }
}
