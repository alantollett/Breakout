﻿using UnityEngine;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour {

    [SerializeField] private TransitionManager transitionManager;
    [SerializeField] private int numLevels;
    [SerializeField] private GameObject scrollViewContent;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject playerPrefab;


    public void Awake() {
        loadLevelButtons();
    }

    private void loadLevelButtons() {
        for (int i = 0; i < numLevels; i++) {
            // get the player name from the static data
            string playerName = StaticData.playerName;

            // create a new instance of the player button prefab in the scroll view
            GameObject button = Instantiate(buttonPrefab);
            button.transform.SetParent(scrollViewContent.transform);

            // load the player from disk to retrieve highest level
            GameObject go = Instantiate(playerPrefab);
            Player player = go.GetComponent<Player>();

            // change the text of the button to the playername
            Text buttonText = button.GetComponentsInChildren<Text>()[0];
            buttonText.text = i == 0 ? "Tutorial" : "Level " + i;

            // change the color and listener of the button based upon users level
            if (i < player.getHighestLevelCompleted() + 2) {
                buttonText.color = new Color(255, 255, 255);
                int level = i;
                button.GetComponent<Button>().onClick.AddListener(() => loadLevel(level));
            } else {
                buttonText.color = new Color(255, 0, 0);
            }

            // destroy the player game object as no longer needed
            Destroy(go);
        }
    }

    public void loadLevel(int levelNumber) {
        transitionManager.LoadScene("Level " + levelNumber);
    }
}
