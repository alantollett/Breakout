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

    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;

    private int lives;
    private int score;
    private GameObject bricks;
    private GameObject walls;
    private int currentLevel;

    private Player player;


    public void Start() {
        disableMenus();
        userCanvas.gameObject.SetActive(true);

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

        disableMenus();
        userCanvas.gameObject.SetActive(true);
    }

    public void openMain() {
        clearLevel();
        disableMenus();
        mainCanvas.gameObject.SetActive(true);

        usernameText.text = player.getPlayerName();
    }

    public void openLevels() {
        disableMenus();
        levelsCanvas.gameObject.SetActive(true);

        for (int i = 0; i < player.getHighestLevelCompleted() + 1; i++) {
            levelButtons[i].GetComponentInChildren<Text>().color = new Color(0, 255, 0);
        }
    }

    // 0 = tutorial, 1 = level 1, ...
    public void loadLevel(int level) {
        if (level > player.getHighestLevelCompleted() + 1) return;
        clearLevel();
        disableMenus();
        gameOverlayCanvas.gameObject.SetActive(true);


        // reset initial values
        currentLevel = level;
        lives = 3;
        score = 0;
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;

        // load in the game objects
        walls = Instantiate(wallsPrefab); 
        bricks = Instantiate(brickPrefabs[level]);

        Ball ball = Instantiate(ballPrefab).GetComponent<Ball>();
        player.setBall(ball);
        ball.setPlayerTransform(player.gameObject.transform);
        ball.setManager(this);
        
        
        // add the player object to the scene
        player.gameObject.SetActive(true);
    }

    public void removeLife() { 
        lives--;
        livesText.text = "Lives: " + lives;

        if(lives == 0) {
            gameOverlayCanvas.gameObject.SetActive(false);
            losePanel.SetActive(true);
        }
    }

    public void playAgain() {
        clearLevel();
        loadLevel(currentLevel);
    }

    private void clearLevel() {
        // remove the ball
        if(player.getBall() != null) {
            Destroy(player.getBall().gameObject);
            player.setBall(null);
        }

        // remove the bricks
        if(bricks != null) {
            Destroy(bricks);
            bricks = null;
        }

        // remove the walls
        if(walls != null) {
            Destroy(walls);
            walls = null;
        }

        // disable the player and reset location
        player.gameObject.SetActive(false);
        player.gameObject.transform.position = new Vector2(0, player.gameObject.transform.position.y);
    }

    private void disableMenus() {
        userCanvas.gameObject.SetActive(false);
        mainCanvas.gameObject.SetActive(false);
        levelsCanvas.gameObject.SetActive(false);
        gameOverlayCanvas.gameObject.SetActive(false);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
    }
}
