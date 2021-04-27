using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MenuManager))]
public class LevelManager : MonoBehaviour {

    [SerializeField] private int level;
    [SerializeField] private int numberOfBricks;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject ballPrefab;

    private Player player;
    private Ball ball;
    private bool paused;
    private int frame;
    
    private void Awake() {
        LoadPlayer();
        LoadBall();
    }

    private void Start() {
        frame = 0;
    }

    private void FixedUpdate() {
        if(!paused) frame++;
    }

    private void LoadPlayer() {
        // add the player to the scene
        GameObject go = Instantiate(playerPrefab);
        player = go.GetComponent<Player>();
        player.loadOld(StaticData.playerName);
    }

    private void LoadBall() {
        // add the ball to the scene
        GameObject go = Instantiate(ballPrefab);
        ball = go.GetComponent<Ball>();
    }

    public void pause() {
        paused = true;
        ball.pause();
    }

    public void resume() {
        paused = false;
    }

    public int getFrame() {
        return frame;
    }

    public int getNumberOfBricks() {
        return numberOfBricks;
    }

    public int getLevel() {
        return level;
    }
    
    public bool isPaused() {
        return paused;
    }

    public Ball getBall() {
        return ball;
    }
    public Player getPlayer() {
        return player;
    }
}
