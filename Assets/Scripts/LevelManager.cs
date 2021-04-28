using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {

    [SerializeField] private Player player;
    [SerializeField] private Ball ball;
    [SerializeField] private int level;
    [SerializeField] private int numberOfBricks;

    private bool paused;
    private int frame;
    private List<Command> recording;
    
    private void Awake() {
        player.loadOld(StaticData.playerName);

        if (StaticData.replayName != null) {
            player.loadRecordings();
            int recordingIndex = player.getRecordingNames().IndexOf(StaticData.replayName);
            recording = player.getRecordings()[recordingIndex];
        }
    }

    private void Start() {
        frame = 0;
    }

    private void FixedUpdate() {
        if(!paused) frame++;
    }

    private void Update() {
        if (StaticData.replayName != null && recording.Count > 0) {
            while (recording.Count > 0 && frame >= recording[0].getFrame()) {
                recording[0].Execute();
                recording.Remove(recording[0]);
            }
            StaticData.replayName = null;
        }
    }

    public void pause() {
        paused = true;
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
