using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    [SerializeField] private Canvas userCanvas;
    [SerializeField] private Canvas levelsCanvas;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas recordingsCanvas;
    [SerializeField] private Canvas gameOverlayCanvas;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Dropdown existingUser;
    [SerializeField] private InputField newUser;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private Text livesText;
    [SerializeField] private Text scoreText;

    private Player player;

    public void Start() {
        disableAllMenus();
        userCanvas.gameObject.SetActive(true);
        loadDropdown();
    }

    public void Update() {
        if(player != null) {
            livesText.text = "Lives: " + player.getLives();
            scoreText.text = "Score: " + player.getScore();
        }
    }

    public void login(bool isNewUser) {
        if (existingUser.value != 0 || newUser.text != null) {
            GameObject playerObject = Instantiate(playerPrefab);
            playerObject.name = "player";
            player = playerObject.GetComponent<Player>();

            if (isNewUser) {
                player.load(newUser.text.ToLower(), true);
            } else {
                player.load(existingUser.options[existingUser.value].text.ToLower(), false);
            }

            openMenu("main");
        }
    }

    public void logout() {
        Destroy(player.gameObject);
        player = null;
        openMenu("user");
    }

    public void openMenu(string menu) {
        menu = menu.ToLower(); 
        disableAllMenus();
        
        if (menu.Equals("user")) {
            userCanvas.gameObject.SetActive(true);
        } else if (menu.Equals("main")) {
            mainCanvas.gameObject.SetActive(true);
        } else if (menu.Equals("levels")) {
            levelsCanvas.gameObject.SetActive(true);
            for (int i = 0; i < player.getHighestLevelCompleted() + 2; i++) {
                levelButtons[i].GetComponentInChildren<Text>().color = new Color(0, 255, 0);
            }
        } else if (menu.Equals("game overlay")) {
            gameOverlayCanvas.gameObject.SetActive(true);
        } else if (menu.Equals("win")) {
            winPanel.SetActive(true);
            player.save();
        } else if (menu.Equals("lose")) {
            losePanel.SetActive(true);
            player.save();
        } else if (menu.Equals("recordings")) {
            recordingsCanvas.gameObject.SetActive(true);

            if (player.getRecordings().Count > 0) {
                foreach(Command c in player.getRecordings()[0]) {
                    //Instantiate(recordingPrefab);
                }
            }
        }
    }

    private void disableAllMenus() {
        userCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);
        levelsCanvas.gameObject.SetActive(false);
        gameOverlayCanvas.gameObject.SetActive(false);
        recordingsCanvas.gameObject.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    private void loadDropdown() {
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


}
