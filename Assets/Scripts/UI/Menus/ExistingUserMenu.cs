using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExistingUserMenu : MonoBehaviour {

    [SerializeField] private TransitionManager transitionManager;
    [SerializeField] private GameObject scrollViewContent;
    [SerializeField] private GameObject playerButtonPrefab;
    [SerializeField] private Sprite[] paddles = new Sprite[5];
    [SerializeField] private GameObject playerPrefab;

    public void Awake() {
        loadUserButtons();
    }

    private void loadUserButtons() {
        string path = Application.persistentDataPath + "/";
        string[] files = System.IO.Directory.GetFiles(path, "*.player");

        for (int i = 0; i < files.Length; i++) {
            // get the player name and paddle id from the player file
            string[] parts = files[i].Split('/');
            string playerName = parts[parts.Length - 1].Split('.')[0];

            // create a new instance of the player button prefab in the scroll view
            GameObject button = Instantiate(playerButtonPrefab);
            button.transform.SetParent(scrollViewContent.transform);

            // change the text of the button to the playername
            Text nameText = button.GetComponentsInChildren<Text>()[0];
            nameText.text = playerName;

            // load the player from disk to retrieve paddle and level
            GameObject go = Instantiate(playerPrefab);
            Player player = go.GetComponent<Player>();
            player.loadOld(playerName);

            // change the image of the button to the paddle
            Image img = button.GetComponentsInChildren<Image>()[1];
            img.sprite = paddles[player.getPaddleId()];

            // change the level text
            Text levelText = button.GetComponentsInChildren<Text>()[1];
            levelText.text = "Level " + player.getHighestLevelCompleted();

            // destroy the player game object as no longer needed
            Destroy(go);

            // add click listeners
            button.GetComponent<Button>().onClick.AddListener(() => login(playerName));
            button.GetComponent<Button>().onClick.AddListener(() => transitionManager.LoadScene("Game Menu"));
        }
    }

    public void login(string playerName) {
        // provide a static point of access to the selected player
        // as we need a way for the new scene to access the data
        StaticData.playerName = playerName;
    }
}
