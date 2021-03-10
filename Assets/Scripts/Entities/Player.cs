using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CommandProcessor))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour, IEntity {

    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float xBound = 6.9f;

    private CommandProcessor commandProcessor;
    //private GameObject ball;
    private Vector2 currentMove;
    private bool moving;

    // player data values
    private string playerName;
    private int highestLevelCompleted;
    private List<Command[]> recordings;
    private Dictionary<int, int> highScores;
    // private SpriteImage paddleImage...?

    // needed as implements IEntity
    Rigidbody2D IEntity.rb => null; 

    // cache resources that may be needed throughout runtime
    public void Awake() {
        commandProcessor = GetComponent<CommandProcessor>();
    }

    // initialise values (player data values loaded separately)
    public void Start() {
        currentMove = Vector2.zero;
        moving = false;
    }

    // initialises player data values (called by the menu manager)
    public void initialisePlayerData(string name, bool isNewPlayer) {
        playerName = name;

        if (isNewPlayer) {
            // if new player then set data values to defaults
            highestLevelCompleted = 0;
            recordings = new List<Command[]>();
            highScores = new Dictionary<int, int>();

            // and then save the new player to disk
            SaveSystem.savePlayerData(this);
        } else {
            // otherwise laod the player data from disk
            PlayerData data = SaveSystem.loadPlayerData(this.playerName);
            highestLevelCompleted = data.getHighestLevelCompleted();
            highScores = data.getHighScores();
        }
    }

    // deal with movement
    public void Update() {
        if (moving) {
            commandProcessor.Execute(new MoveCommand(this, currentMove, movementSpeed * Time.deltaTime));

            // ensure that paddle is within bounds of the screen via code (not using RBs - read notes)
            if (transform.position.x >= xBound) {
                transform.position = new Vector2(xBound, transform.position.y);
            } else if (transform.position.x <= -xBound) {
                transform.position = new Vector2(-xBound, transform.position.y);
            }
        }
    }

    /*
     * INPUT SYSTEM FUNCTIONS
     */
    public void Move(InputAction.CallbackContext context) {
        currentMove = context.ReadValue<Vector2>();
        currentMove.y = 0;

        if (context.started) {
            moving = true;
        } else if (context.canceled) {
            currentMove = Vector2.zero;
            moving = false;
        }
    }
    public void Fire(InputAction.CallbackContext context) {
        // fire ball, perhaps have a ball attribute and call fire method on that...?
        // just because I want all commands from each GO stored in one CommandProcessor,
        // so maybe even make a singleton command processor?
    }


    /*
     * GETTERS AND SETTERS 
     */

    public string getPlayerName() { return playerName; }
    public int getHighestLevelCompleted() { return highestLevelCompleted; }
    public Dictionary<int, int> getHighScores() { return highScores; }
}
