using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    [SerializeField] private Canvas userCanvas;
    [SerializeField] private Dropdown existingUser;
    [SerializeField] private InputField newUser;
    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Text usernameText;

    [SerializeField] private Canvas levelsCanvas;
    [SerializeField] private Button[] levelButtons;

    private Player player;


    public void Start() {
        userCanvas.gameObject.SetActive(true); 
        mainCanvas.gameObject.SetActive(false);
        levelsCanvas.gameObject.SetActive(false);

        // load all existing player names and populate the existing player dropdown menu with them
        string path = Application.persistentDataPath + "/";
        string[] files = System.IO.Directory.GetFiles(path, "*.player");

        existingUser.options.Clear(); // clear existing options from editor
        existingUser.options.Add(new Dropdown.OptionData("select user")); // add a default option

        for (int i = 0; i < files.Length; i++) {
            string[] parts = files[i].Split('/');
            string playerName = parts[parts.Length - 1].Split('.')[0];
            existingUser.options.Add(new Dropdown.OptionData(playerName));
        }
    }

    public void quitGame() {
        Application.Quit();
    }

    public void login(bool isNewUser) {
        if (!isNewUser) {
            if (existingUser.value != 0) {
                // existing user dropdown was edited, so create a user with that name...
                GameObject playerObject = Instantiate(playerPrefab);
                player = playerObject.GetComponent<Player>();
                player.initialisePlayerData(existingUser.options[existingUser.value].text.ToLower(), false);
                openMain();
            }
        } else if (newUser.text != null && newUser.text.Length > 0) {
            // new user text field was edited, so create a new user with that name...
            // need to check if the user exists before doing this!!!!! loop through dropdown options... check if equal
            GameObject playerObject = Instantiate(playerPrefab);
            player = playerObject.GetComponent<Player>();
            player.initialisePlayerData(newUser.text.ToLower(), true);
            openMain();
        }
    }

    public void openUser() {
        Destroy(player.gameObject);
        player = null;
        userCanvas.gameObject.SetActive(true);
        mainCanvas.gameObject.SetActive(false);
        levelsCanvas.gameObject.SetActive(false);
    }

    public void openMain() {
        userCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
        levelsCanvas.gameObject.SetActive(false);

        usernameText.text = player.getPlayerName();
    }

    public void openLevels() {
        userCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);
        levelsCanvas.gameObject.SetActive(true);

        for(int i = 0; i < player.getHighestLevelCompleted() + 1; i++) {
            levelButtons[i].GetComponentInChildren<Text>().color = new Color(0, 255, 0);
        }
    }

    public void loadLevel(int level) {
        // turn off all menus
        userCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);
        levelsCanvas.gameObject.SetActive(false);



    }
}
