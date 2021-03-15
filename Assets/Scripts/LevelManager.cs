using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(MenuManager))]
public class LevelManager : MonoBehaviour {

    [SerializeField] private GameObject[] levels;
    [SerializeField] private GameObject wallsPrefab;
    [SerializeField] private GameObject ballPrefab;

    private MenuManager menuManager;
    private EntityManager entityManager;
    private Player player;
    private Ball ball;
    private GameObject bricks;
    private List<Command> currentRecording;

    private int currentLevel;
    private int frame;

    public void Awake() {
        entityManager = GetComponent<EntityManager>();
        menuManager = gameObject.GetComponent<MenuManager>();

        player = entityManager.getPlayer();
        ball = entityManager.getBall();
    }

    private void FixedUpdate() {
        frame++;
    }

    private void Update() {
        if (currentRecording != null && currentRecording.Count > 0) {
            while(currentRecording[0] != null && frame >= currentRecording[0].getFrame()) {
                currentRecording[0].Execute();

                if (currentRecording[0].GetType() == typeof(FireCommand)) {
                    ball.setMoving(true);
                }

                currentRecording.Remove(currentRecording[0]);
            }
        }
    }

    public void loadLevel(int level) {
        if (level > player.getHighestLevelCompleted() + 1) return;

        // turn on overlay
        menuManager.openMenu(4);

        // remove the existing levels bricks and reset ball and player position
        clearLevel();

        // reset initial values
        currentLevel = level;
        player.setLives(3);
        player.setScore(0);

        // load in the new bricks
        bricks = Instantiate(levels[level]);

        // reset the recording
        player.getCommandProcessor().clear();

        frame = 0;
    }

    public void loadRecording(List<Command> recording, int level) {
        loadLevel(level);

        currentRecording = recording;
        frame = 0;
    }

    public void clearLevel() {
        // remove the bricks
        if (bricks != null) {
            Destroy(bricks);
            bricks = null;
        }

        // reset the player and ball location
        player.gameObject.transform.position = new Vector2(0, player.gameObject.transform.position.y);
        ball.gameObject.transform.position = new Vector2(0, player.gameObject.transform.position.y + 0.4f);
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
        menuManager.openMenu(1);
    }

    public int getNumLevels() {
        return levels.Length;
    }

    public int getFrame() {
        return frame;
    }

    public Player getPlayer() {
        return player;
    }
}
