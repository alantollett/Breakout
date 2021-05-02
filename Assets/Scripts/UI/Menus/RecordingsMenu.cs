using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TransitionManager))]
public class RecordingsMenu : MonoBehaviour {

    [SerializeField] private int numMenus;
    [SerializeField] private GameObject scrollViewContent;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject playerPrefab;

    private TransitionManager transitionManager;

    public void Awake() {
        transitionManager = GetComponent<TransitionManager>();
        loadRecordingButtons();
    }

    private void loadRecordingButtons() {

        // get the player name from the static data
        string playerName = StaticData.playerName;
        GameObject go = Instantiate(playerPrefab);
        Player player = go.GetComponent<Player>();
        //player.loadOld(playerName);

        for (int i = 0; i < player.getRecordingNames().Count; i++) {

            // create a new instance of the button prefab in the scroll view
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(scrollViewContent.transform);
            button.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 600);

            // change text to Recording index
            Text buttonText = button.GetComponentsInChildren<Text>()[0];
            buttonText.text = player.getRecordingNames()[i];

            // add a listener to the button
            string recording = player.getRecordingNames()[i];
            button.GetComponent<Button>().onClick.AddListener(() => loadRecording(recording));

            // destroy the player game object as no longer needed
            Destroy(go);
        }
    }

    public void loadRecording(string recordingName) {
        int levelNum = int.Parse(recordingName.Split(' ')[1]);
        //StaticData.replayName = recordingName;
        transitionManager.LoadScene(numMenus + levelNum);
    }
}
