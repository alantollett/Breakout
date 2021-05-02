using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPlayerManager : MonoBehaviour {

    private void Awake() {
        Ball ball = FindObjectOfType<Ball>();
        int angle = Random.Range(1, 359);

        ball.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * ball.getMovementSpeed();
    }

}
