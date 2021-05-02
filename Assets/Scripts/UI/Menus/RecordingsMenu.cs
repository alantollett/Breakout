using UnityEngine;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.UI;

public class RecordingsMenu : MonoBehaviour {

    [SerializeField] private TransitionManager transitionManager;
    [SerializeField] private GameObject scrollViewContent;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject playerPrefab;


    public void Awake() {
        loadReplayButtons();
    }

    private void loadReplayButtons() {
        // get the player name from the static data
        string playerName = StaticData.playerName;

        // load the player from disk to retrieve the recordings
        GameObject go = Instantiate(playerPrefab);
        Player player = go.GetComponent<Player>();

        for (int i = 0; i < player.getRecordingNames().Count; i++) {

            // create a new instance of the button prefab in the scroll view
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(scrollViewContent.transform);

            // change text to Recording index
            Text buttonText = button.GetComponentsInChildren<Text>()[0];
            buttonText.text = player.getRecordingNames()[i];

            // add a listener to the button
            string recording = player.getRecordingNames()[i];
            button.GetComponent<Button>().onClick.AddListener(() => loadRecording(recording));
            button.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 600);


            // destroy the player game object as no longer needed
            Destroy(go);
        }
    }

    public void loadRecording(string recordingName) {
        int levelNum = int.Parse(recordingName.Split(' ')[1]);
        RecordingManager.recordingName = recordingName;
        transitionManager.LoadScene("Level " + levelNum);
    }
}



