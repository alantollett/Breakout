using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {

    [SerializeField] private GameManager gameManager;
    [SerializeField] private int maxHealth = 1;
    private int health;

    public void Awake() {
        health = maxHealth;
    }

    public void hit() {
        health -= 1;
        gameManager.addScore(1);
        if(health <= 0) {
            Destroy(this.gameObject);
        }        
    }
}
