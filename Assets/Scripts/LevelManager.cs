using UnityEngine;

[RequireComponent(typeof(UIManager))]
public class LevelManager : MonoBehaviour {

    [SerializeField] private GameObject[] brickPrefabs;
    [SerializeField] private GameObject wallsPrefab;
    [SerializeField] private GameObject ballPrefab;

    private UIManager uiManager;

    private Player player;
    private Ball ball;
    private GameObject bricks;
    private GameObject walls;

    private int currentLevel;

    public void Awake() {
        uiManager = gameObject.GetComponent<UIManager>();
    }

    // 0 = tutorial, 1 = level 1, ...
    public void loadLevel(int level) {
        player = GameObject.Find("player").GetComponent<Player>();

        if (level > player.getHighestLevelCompleted() + 1) return;
        uiManager.openMenu("game overlay");
        clearLevel();

        // reset initial values
        currentLevel = level;
        player.setLives(3);
        player.setScore(0);

        // load in the game objects
        walls = Instantiate(wallsPrefab);
        bricks = Instantiate(brickPrefabs[level]);
        ball = Instantiate(ballPrefab).GetComponent<Ball>();
        ball.gameObject.name = "ball";
    }

    public void clearLevel() {
        // remove the ball
        if (ball != null) {
            Destroy(ball.gameObject);
            ball = null;
        }

        // remove the bricks
        if (bricks != null) {
            Destroy(bricks);
            bricks = null;
        }

        // remove the walls
        if (walls != null) {
            Destroy(walls);
            walls = null;
        }

        // reset the player location
        player.gameObject.transform.position = new Vector2(0, player.gameObject.transform.position.y);
    }

    public int getRemainingBricks() {
        if (bricks == null) return 99999;
        return bricks.transform.childCount - 1;
    }

    public int getCurrentLevel() {
        return currentLevel;
    }

    public void loadNextLevel() {
        clearLevel();
        loadLevel(currentLevel + 1);
    }

    public void loadSameLevel() {
        clearLevel();
        loadLevel(currentLevel);
    }

    public void quitLevel() {
        clearLevel();
        uiManager.openMenu("main");
    }
}
