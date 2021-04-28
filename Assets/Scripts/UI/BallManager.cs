using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private int numBalls;

    public void Awake() {

        // instantiate a number of balls and give them a random velocity
        for(int i = 0; i < numBalls; i++) {
            GameObject ball = Instantiate(ballPrefab);

            Vector2 velocity = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
            ball.GetComponent<Rigidbody2D>().velocity = velocity;
        }

    }

}
