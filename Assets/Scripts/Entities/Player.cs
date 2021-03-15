using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CommandProcessor))]
public class Player : MonoBehaviour, IEntity {
    //private UIManager uiManager;
    private LevelManager levelManager;
    private CommandProcessor commandProcessor;

    private string playerName;
    private int highestLevelCompleted;
    private List<List<Command>> recordings;
    private Dictionary<int, int> highScores;
    private int lives;
    private int score;

    Rigidbody2D IEntity.rb => null;

    public void Awake() {
        //uiManager = GameObject.Find("Game Manager").GetComponent<UIManager>();
        levelManager = GameObject.Find("Game Manager").GetComponent<LevelManager>();
        commandProcessor = GetComponent<CommandProcessor>();
    }

    public void load(string name, bool isNewPlayer) {
        playerName = name;

        if (isNewPlayer) {
            // if new player then set data values to defaults
            highestLevelCompleted = 0;
            recordings = new List<List<Command>>();
            highScores = new Dictionary<int, int>();

            // and then save the new player to disk
            SaveSystem.savePlayerData(this);
        } else {
            // otherwise laod the player data from disk
            PlayerData data = SaveSystem.loadPlayerData(playerName);
            highestLevelCompleted = data.getHighestLevelCompleted();
            //highScores = data.getHighScores();
            recordings = data.getRecordings(this);

            if(recordings == null) {
                recordings = new List<List<Command>>();
            }
        }
    }

    public void unLoad() {
        playerName = "";
        highestLevelCompleted = 0;
        recordings = null;
        highScores = null;
    }

    public void save() {
        recordings.Add(commandProcessor.clear());
        SaveSystem.savePlayerData(this);
    }

    public string getPlayerName() { 
        return playerName; 
    }

    public int getHighestLevelCompleted() { 
        return highestLevelCompleted;
    }

    public void setHighestLevelCompleted(int level) {
        highestLevelCompleted = level;
    }

    public Dictionary<int, int> getHighScores() { 
        return highScores; 
    }

    public int getLives() { 
        return lives; 
    }

    public void setLives(int lives) {
        this.lives = lives;

        if (lives == 0) {
            //uiManager.openMenu("lose");
        }
    }

    public int getScore() { 
        return score; 
    }

    public void setScore(int score) { 
        this.score = score;

        if(levelManager.getRemainingBricks() == 0) {
            //uiManager.openMenu("win");
            
            if(levelManager.getCurrentLevel() > highestLevelCompleted) {
                highestLevelCompleted = levelManager.getCurrentLevel();
            }
        }
    }

    public List<List<Command>> getRecordings() {
        return recordings;
    }
}
