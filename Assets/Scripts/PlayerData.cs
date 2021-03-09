using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    private string name;
    private int highestLevelCompleted;
    private Dictionary<int, int> highScores;
    // private List<Command[]> recordings;


    public PlayerData(Player player) {
        this.name = player.getName();
        highestLevelCompleted = player.getHighestLevelCompleted();
        highScores = player.getHighScores();
    }

    public int getHighestLevelCompleted() { return highestLevelCompleted; }
    public Dictionary<int, int> getHighScores() { return highScores; }

}
