using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BrickBehaviour : MonoBehaviour {

    [SerializeField] private MenuManager manager;
    [SerializeField] private int maxHealth = 1;
    [SerializeField] private Sprite[] sprites = new Sprite[3];
    private int health;
    private SpriteRenderer spriteRenderer;

    // cache resources
    public void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // initialise values
    public void Start() {
        health = maxHealth;
        spriteRenderer.sprite = sprites[0];
    }

    public void hit() {
        health -= 1;

        if(health <= 0) {
            manager.addScore(1);
            Destroy(this.gameObject);
        } else {
            int step = maxHealth / 3;

            if(health == maxHealth - step) {
                spriteRenderer.sprite = sprites[1];
            }else if(health == maxHealth - (2 * step)) {
                spriteRenderer.sprite = sprites[2];
            }
        }

    }

    public void setManager(MenuManager manager) { this.manager = manager; }
}
