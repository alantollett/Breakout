using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TransitionManager))]
public class LevelsMenu : MonoBehaviour {

    [SerializeField] private int numMenus;
    [SerializeField] private int numLevels;
    [SerializeField] private GameObject scrollViewContent;
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private GameObject playerPrefab;

    private TransitionManager transitionManager;

    public void Awake() {
        transitionManager = GetComponent<TransitionManager>();
        loadLevelButtons();
    }

    private void loadLevelButtons() {
        for (int i = 1; i < numLevels + 1; i++) {
            // get the player name from the static data
            string playerName = StaticData.playerName;

            // create a new instance of the player button prefab in the scroll view
            GameObject button = Instantiate(levelButtonPrefab);
            button.transform.SetParent(scrollViewContent.transform);

            // load the player from disk to retrieve highest level
            GameObject go = Instantiate(playerPrefab);
            Player player = go.GetComponent<Player>();
            player.loadOld(playerName);

            // change the text of the button to the playername
            Text buttonText = button.GetComponentsInChildren<Text>()[0];
            buttonText.text = i == 1 ? "Tutorial" : "Level " + (i - 1);

            // change the color and listener of the button based upon users level
            if (i < player.getHighestLevelCompleted() + 3) {
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
        transitionManager.LoadScene(numMenus - 1 + levelNumber);
    }
}
