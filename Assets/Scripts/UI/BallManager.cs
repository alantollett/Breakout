using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour {

    [SerializeField] private GameObject balls;

    public void Awake() {

        foreach (Transform child in balls.transform) {
            Vector2 velocity = new Vector2(Random.Range(-10, 10), Random.Range(-10, 10));
            child.GetComponent<Rigidbody2D>().velocity = velocity;
        }

    }

}
