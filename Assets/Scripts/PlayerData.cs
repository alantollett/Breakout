using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    [SerializeField] private string name;
    [SerializeField] private int highestLevelCompleted;
    //[SerializeField] private Dictionary<int, int> highScores;
    [SerializeField] private string recordings;


    public PlayerData(Player player) {
        name = player.getPlayerName();
        highestLevelCompleted = player.getHighestLevelCompleted();
        //highScores = player.getHighScores();
        recordings = recordingsToString(player.getRecordings());
    }

    private string recordingsToString(List<List<Command>> recordings) {
        string recordingsStr = "";

        int i = 0;
        foreach(List<Command> recording in recordings) {
            recordingsStr += "START " + i + "\n";

            foreach(Command c in recording) {
                recordingsStr += c.ToString() + "\n";
            }

            recordingsStr += "END " + i + "\n";
            i++;
        }

        return recordingsStr;
    }

    public int getHighestLevelCompleted() { return highestLevelCompleted; }
    //public Dictionary<int, int> getHighScores() { return highScores; }

    public List<List<Command>> getRecordings(IEntity player) {
        List<List<Command>> recordingsArr = new List<List<Command>>();

        if (recordings == null || recordings.Length == 0) return recordingsArr;

        string[] lines = recordings.Split('\n');
        List<Command> currentRecording = null;
        foreach(string line in lines) {
            if (line.StartsWith("START")) {
                currentRecording = new List<Command>();
            } else if (line.StartsWith("END")) {
                recordingsArr.Add(currentRecording);
            } else if(line.Length > 0){
                string[] parts = line.Split(':');
                string[] components = parts[1].Split(',');

                if (parts[0].Equals("MoveCommand")) {
                    float x = float.Parse(components[0]);
                    float y = float.Parse(components[1]);
                    float speed = float.Parse(components[2]);
                    currentRecording.Add(new MoveCommand(player, new Vector2(x, y), speed));
                }else if (parts[0].Equals("FireCommand")) {
                    float angle = float.Parse(components[0]);
                    float speed = float.Parse(components[1]);
                    currentRecording.Add(new FireCommand(player, angle, speed));
                }
            }
        }

        return recordingsArr; 
    }

}
