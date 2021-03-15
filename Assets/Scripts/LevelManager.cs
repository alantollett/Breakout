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

    private int currentLevel;
    private float startTime;

    public void Awake() {
        entityManager = GetComponent<EntityManager>();
        menuManager = gameObject.GetComponent<MenuManager>();

        player = entityManager.getPlayer();
        ball = entityManager.getBall();
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

        startTime = Time.timeSinceLevelLoad;
    }

    public void loadRecording(List<Command> recording, int level) {
        Debug.Log("LOADING RECORDING");
        loadLevel(level);

        startTime = Time.timeSinceLevelLoad;
        while(recording.Count > 0) {
            float currentTime = Time.timeSinceLevelLoad - startTime;

            if (recording[0].getTime() >= currentTime) {
                recording[0].Execute();
                recording.Remove(recording[0]);
            }
        }
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

    public float getStartTime() {
        return startTime;
    }

    public Player getPlayer() {
        return player;
    }
}
