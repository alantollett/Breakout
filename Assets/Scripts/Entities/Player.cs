using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CommandProcessor))]
public class Player : MonoBehaviour, IEntity {

    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float paddleXBound = 7.2f;
    [SerializeField] private Sprite[] paddles = new Sprite[5];

    private CommandProcessor commandProcessor;
    private LevelManager levelManager;
    private MenuManager menuManager;
    private SpriteRenderer spriteRenderer;

    private string playerName;
    private int highestLevelCompleted;
    private int paddleId;
    private List<List<Command>> recordings;

    private Vector2 currentMove;
    private bool moving;
    private int lives = 3;
    private int score = 0;


    Rigidbody2D IEntity.rb => null;

    public void Awake() {
        commandProcessor = GetComponent<CommandProcessor>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        menuManager = GameObject.Find("GameManager").GetComponent<MenuManager>();
    }

    public void Start() {
        currentMove = Vector2.zero;
        moving = false;
    }


    public void Update() {
        // update paddle's position
        if (moving && !levelManager.isPaused()) {
            int frame = levelManager.getFrame();
            commandProcessor.Execute(new MoveCommand(this, frame, currentMove, movementSpeed * Time.deltaTime));

            // ensure that paddle is within bounds of the screen via code (not using RBs - read notes)
            if (transform.position.x >= paddleXBound) {
                transform.position = new Vector2(paddleXBound, transform.position.y);
            } else if (transform.position.x <= -paddleXBound) {
                transform.position = new Vector2(-paddleXBound, transform.position.y);
            }
        }
    }

    public void move(InputAction.CallbackContext context) {
        currentMove = context.ReadValue<Vector2>();
        currentMove.y = 0;

        if (context.started && !levelManager.isPaused()) {
            moving = true;
        } else if (context.canceled) {
            currentMove = Vector2.zero;
            moving = false;
        }
    }

    public void loadNew(string playerName, int paddleId) {
        this.playerName = playerName;
        this.paddleId = paddleId;
        highestLevelCompleted = 0;
        recordings = new List<List<Command>>();
    }

    public void loadOld(string playerName) {
        PlayerData data = SaveSystem.loadPlayerData(playerName);
        highestLevelCompleted = data.getHighestLevelCompleted();
        paddleId = data.getPaddleId();
        spriteRenderer.sprite = paddles[paddleId];

        recordings = data.getRecordings(this);
        if(recordings == null) recordings = new List<List<Command>>();
    }

    public void save() {
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

            if (levelManager.getLevel() > highestLevelCompleted) {
                highestLevelCompleted = levelManager.getLevel() + 1;
                recordings.Add(commandProcessor.getCommands());
                save();
                Debug.Log(recordings.Count);
            }
        }
    }

    public List<List<Command>> getRecordings() {
        return recordings;
    }


}
