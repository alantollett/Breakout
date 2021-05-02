using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour, IEntity {
    Rigidbody2D IEntity.rb => null; // required as implements IEntity

    [SerializeField] private Sprite[] paddles;
    [SerializeField] private GameObject ballPrefab;

    private SpriteRenderer spriteRenderer;

    private string playerName;
    private int paddleId = 0;
    private int highestLevelCompleted = 0;
    private List<List<Command>> recordings = new List<List<Command>>();
    private List<string> recordingNames = new List<string>();


    public void Awake() {
        LoadPlayer();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = paddles[paddleId];
    }

    private void OnEnable() {
        // register for events
        LevelManager.OnLevelWin += SetHighestLevelCompleted;
        LevelManager.OnLifeLost += AddBall;
    }

    private void OnDisable() {
        // unregister for events
        LevelManager.OnLevelWin -= SetHighestLevelCompleted;
        LevelManager.OnLifeLost -= AddBall;
    }

    private void LoadPlayer() {
        // get the playername from static class as needs to persist between scenes.
        playerName = StaticData.playerName;

        if (StaticData.paddleId != -1) {
            // if the static paddle id has been edited (by the new user menu)
            // then just update the paddleId of the script and save to disk.
            paddleId = StaticData.paddleId;
            StaticData.paddleId = -1;
            save();
        } else {
            // the player alreadt exists (came from existing user menu) so
            // load the player's data from the disk and populate into this script.
            PlayerData data = SaveSystem.loadPlayerData(playerName);
            highestLevelCompleted = data.getHighestLevelCompleted();
            recordings = data.getRecordings();
            recordingNames = data.getRecordingNames();
            paddleId = data.getPaddleId();
        }
    }

    private void AddBall(int remainingLives) {
        if(remainingLives > 0) {
            // add a ball to the scene
            Ball ball = Instantiate(ballPrefab).GetComponent<Ball>();
            ball.transform.position = new Vector3(transform.position.x, -3.55f, 0);
            ball.transform.parent = this.transform;
        }
    }

    public void save() {
        SaveSystem.savePlayerData(this);
    }

    public int getHighestLevelCompleted() { 
        return highestLevelCompleted;
    }

    public void SetHighestLevelCompleted(int level) {
        if (level > highestLevelCompleted) {
            highestLevelCompleted = level;
            save();
        }
    }

    public List<string> getRecordingNames() {
        return recordingNames;
    }

    public List<List<Command>> getRecordings() {
        return recordings;
    }

    public string getPlayerName() {
        return playerName;
    }

    public int getPaddleId() {
        return paddleId;
    }
}
