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
    [SerializeField] private GameObject[] brickPrefabs;
    [SerializeField] private GameObject wallsPrefab;
    [SerializeField] private GameObject ballPrefab;

    [SerializeField] private Canvas gameOverlayCanvas;
    [SerializeField] private Text livesText;
    [SerializeField] private Text scoreText;
    private int lives;
    private int score;

    private Player player;


    public void Start() {
        userCanvas.gameObject.SetActive(true); 
        mainCanvas.gameObject.SetActive(false);
        levelsCanvas.gameObject.SetActive(false);
        gameOverlayCanvas.gameObject.SetActive(false);

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
                playerObject.SetActive(false);
                player = playerObject.GetComponent<Player>();
                player.initialisePlayerData(existingUser.options[existingUser.value].text.ToLower(), false);
                openMain();
            }
        } else if (newUser.text != null && newUser.text.Length > 0) {
            // new user text field was edited, so create a new user with that name...
            // need to check if the user exists before doing this!!!!! loop through dropdown options... check if equal
            GameObject playerObject = Instantiate(playerPrefab);
            playerObject.SetActive(false);
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
        gameOverlayCanvas.gameObject.SetActive(false);
    }

    public void openMain() {
        userCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(true);
        levelsCanvas.gameObject.SetActive(false);
        gameOverlayCanvas.gameObject.SetActive(false);

        usernameText.text = player.getPlayerName();
    }

    public void openLevels() {
        userCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);
        levelsCanvas.gameObject.SetActive(true);
        gameOverlayCanvas.gameObject.SetActive(false);

        for (int i = 0; i < player.getHighestLevelCompleted() + 1; i++) {
            levelButtons[i].GetComponentInChildren<Text>().color = new Color(0, 255, 0);
        }
    }

    // 0 = tutorial, 1 = level 1, ...
    public void loadLevel(int level) {
        if (level > player.getHighestLevelCompleted() + 1) return;

        // turn off all menus
        userCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);
        levelsCanvas.gameObject.SetActive(false);

        // add the in-game overlay
        gameOverlayCanvas.gameObject.SetActive(true);

        // set lives and score to initial values
        lives = 3;
        score = 0;
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;

        // load in the walls, ball and bricks for the level
        Instantiate(wallsPrefab);

        Ball ball = Instantiate(ballPrefab).GetComponent<Ball>();
        player.setBall(ball);
        ball.setPlayerTransform(player.gameObject.transform);
        ball.setManager(this);
        
        Instantiate(brickPrefabs[level]);

        // add the player object to the scene
        player.gameObject.SetActive(true);
    }

    public void removeLife() { 
        lives--;
        livesText.text = "Lives: " + lives;
    }
}
