using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingManager : MonoBehaviour {

    private static List<Command> recording;
    private int frame;
    private bool paused;

    private void OnEnable() {
        LevelManager.OnLevelPause += Pause;
        LevelManager.OnLevelResume += Resume;
    }

    private void OnDisable() {
        LevelManager.OnLevelPause -= Pause;
        LevelManager.OnLevelResume -= Resume;
    }


    private void Pause(bool _) {
        paused = true;
    }
    private void Resume() {
        paused = false;
    }

    private void Start() {
        frame = 0;
    }

    private void FixedUpdate() {
        if(!paused) frame++;
    }

    private void Update() {
        if (!paused) { 
            if (recording != null && recording.Count > 0) {
                while (recording.Count > 0 && frame >= recording[0].getFrame()) {
                    recording[0].Execute();
                    recording.Remove(recording[0]);
                }

                recording = null;
            }
        }
    }

    public static void SetRecording(List<Command> recordingToPlay) {
        recording = recordingToPlay;
    }

    public int getFrame() {
        return frame;
    }

}
