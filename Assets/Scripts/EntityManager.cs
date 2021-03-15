using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour {

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject ball;

    public Player getPlayer() {
        return player.GetComponent<Player>();
    }

    public Ball getBall() {
        return ball.GetComponent<Ball>(); ;
    }

}
