using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CommandProcessor))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour, IEntity {

    [SerializeField] private Sprite[] paddles = new Sprite[5];
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private MenuManager menuManager;
    [SerializeField] private GameObject ballPrefab;

    private SpriteRenderer spriteRenderer;
    private CommandProcessor commandProcessor;
    private Ball ball;

    private string playerName;
    private int highestLevelCompleted;
    private int paddleId;
    private List<List<Command>> recordings;
    private List<string> recordingNames;

    private int lives = 3;
    private int score = 0;


    Rigidbody2D IEntity.rb => null;

    public void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        commandProcessor = GetComponent<CommandProcessor>();
        addBall();
    }

    public void loadNew(string playerName, int paddleId) {
        this.playerName = playerName;
        this.paddleId = paddleId;
        highestLevelCompleted = 0;
        recordings = new List<List<Command>>();
        recordingNames = new List<string>();
    }

    public void loadOld(string playerName) {
        this.playerName = playerName;

        PlayerData data = SaveSystem.loadPlayerData(playerName);
        highestLevelCompleted = data.getHighestLevelCompleted();
        paddleId = data.getPaddleId();
        spriteRenderer.sprite = paddles[paddleId];

        recordingNames = data.getRecordingNames();
        if (recordingNames == null) {
            recordingNames = new List<string>();
        }
    }

    private void addBall() {
        // add a ball to the scene
        ball = Instantiate(ballPrefab).GetComponent<Ball>();
        ball.transform.position = new Vector3(transform.position.x, -3.55f, 0);
        ball.transform.parent = this.transform;
        ball.setPlayer(this);
    }

    public void save() {
        Debug.Log(playerName);
        SaveSystem.savePlayerData(this);
    }

    public string getPlayerName() {
        return playerName;
    }
    public void setPlayerName(string playerName) {
        this.playerName = playerName;
    }

    public int getHighestLevelCompleted() { 
        return highestLevelCompleted;
    }

    public List<string> getRecordingNames() {
        return recordingNames;
    }

    public void setHighestLevelCompleted(int level) {
        highestLevelCompleted = level;
        save();
    }

    public void setPaddleId(int paddleId) {
        this.paddleId = paddleId;
    }

    public int getPaddleId() {
        return paddleId;
    }

    public int getLives() { 
        return lives; 
    }

    public void setLives(int lives) {
        this.lives = lives;

        if (lives == 0) {
            levelManager.pause();
            menuManager.setLoseMenuVisible(true);
        } else {
            addBall();
        }
    }

    public int getScore() { 
        return score; 
    }

    public void setScore(int score) { 
        this.score = score;

        if (levelManager.getNumberOfBricks() == score) {
            levelManager.pause();
            menuManager.setWinMenuVisible(true);

            if(StaticData.replayName == null) {
                if (recordings == null) loadRecordings();
                recordings.Add(commandProcessor.getCommands());
                recordingNames.Add("Level " + levelManager.getLevel() + " " + System.DateTime.Now.ToString("d/MMM hh:mm"));
                save();
            }

            if (levelManager.getLevel() > highestLevelCompleted) {
                highestLevelCompleted = levelManager.getLevel();
            }
        }
    }

    public List<List<Command>> getRecordings() {
        return recordings;
    }

    public void loadRecordings() {
        PlayerData data = SaveSystem.loadPlayerData(playerName);
        recordings = data.getRecordings();
    }

    public Ball GetBall() {
        return ball;
    }
}
