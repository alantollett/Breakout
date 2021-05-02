using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour, IEntity {
    Rigidbody2D IEntity.rb => null;

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
    }

    private void OnEnable() {
        LevelManager.OnLevelWin += SetHighestLevelCompleted;
    }

    private void OnDisable() {
        LevelManager.OnLevelWin -= SetHighestLevelCompleted;
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
            // the player already exists (came from existing user menu) so
            // load the player's data from the disk and populate into this script.
            PlayerData data = SaveSystem.loadPlayerData(playerName);
            highestLevelCompleted = data.getHighestLevelCompleted();
            recordings = data.getRecordings();
            recordingNames = data.getRecordingNames();
            paddleId = data.getPaddleId();
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = paddles[paddleId];
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

    public void AddRecordingName(string name) {
        recordingNames.Add(name);
    }

    public void AddRecording(List<Command> recording) {
        recordings.Add(recording);
    }
}
