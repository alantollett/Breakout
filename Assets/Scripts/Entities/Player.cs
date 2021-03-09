using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private string name;
    private int highestLevelCompleted;
    private List<Command[]> recordings;
    private Dictionary<int, int> highScores;

    // initialises player data values
    public void initialise(string name, bool isNewPlayer) {
        this.name = name;
     
        if (isNewPlayer) {
            // if new player then set data values to defaults
            highestLevelCompleted = 0;
            recordings = new List<Command[]>();
            highScores = new Dictionary<int, int>();

            // and then save the new player to disk
            SaveSystem.savePlayerData(this);
        } else {
            // otherwise laod the player data from disk
            PlayerData data = SaveSystem.loadPlayerData(this.name);
            highestLevelCompleted = data.getHighestLevelCompleted();
            highScores = data.getHighScores();
        }
    }

    public string getName() { return name; }
    public int getHighestLevelCompleted() { return highestLevelCompleted; }
    public Dictionary<int, int> getHighScores() { return highScores; }
}
