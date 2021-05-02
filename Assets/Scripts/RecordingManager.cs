using System.Collections.Generic;
using UnityEngine;

public class RecordingManager : MonoBehaviour {

    public static string recordingName;
    private CommandProcessor commandProcessor;
    private InputManager inputManager;
    private List<Command> recording;
    private int frame;
    private bool paused;
    private Player player;

    private void Awake() {
        commandProcessor = FindObjectOfType<CommandProcessor>();
        inputManager = FindObjectOfType<InputManager>();
    }

    private void Start() {
        player = FindObjectOfType<Player>();
        if(recordingName != null) {
            int recordingIndex = player.getRecordingNames().IndexOf(recordingName);
            recording = player.getRecordings()[recordingIndex];
            recordingName = null;
        }
        frame = 0;
    }

    private void OnEnable() {
        LevelManager.OnLevelPause += Pause;
        LevelManager.OnLevelResume += Resume;
        LevelManager.OnLevelWin += SaveRecording;
    }

    private void OnDisable() {
        LevelManager.OnLevelPause -= Pause;
        LevelManager.OnLevelResume -= Resume;
        LevelManager.OnLevelWin -= SaveRecording;
    }

    private void Pause(bool _) {
        paused = true;
    }
    private void Resume() {
        paused = false;
    }

    private void SaveRecording(int level) {
        if(recording == null && level > 0) {
            player.AddRecordingName("Level " + level + " " + System.DateTime.Now.ToString("d MMM hh:mm"));
            player.AddRecording(commandProcessor.getCommands());
            player.save();
        }
    }


    private void FixedUpdate() {
        if(!paused) frame++;
    }

    private void Update() {
        if (!paused) {
            if (recording != null && recording.Count > 0) {
                while(recording.Count > 0 && frame >= recording[0].getFrame()) {
                    recording[0].Execute();
                    recording.Remove(recording[0]);

                    EnforceBounds();
                }
            }
        }
    }

    private void EnforceBounds() {
        // ensure that paddle is within bounds of the screen via code (not using RBs - read notes)
        if (player.transform.position.x >= inputManager.paddleXBound) {
            player.transform.position = new Vector2(inputManager.paddleXBound, player.transform.position.y);
        } else if (player.transform.position.x <= -inputManager.paddleXBound) {
            player.transform.position = new Vector2(-inputManager.paddleXBound, player.transform.position.y);
        }
    }


    public int getFrame() {
        return frame;
    }

}
