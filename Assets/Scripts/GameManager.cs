using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameManager : MonoBehaviour {

    [SerializeField] private Text livesText;
    [SerializeField] private Text scoreText;
    [SerializeField] private int lives = 5;
    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject gameCompletedPanel;
    [SerializeField] private GameObject brickHolder;
    private int score = 0;
    private int level = 1;

    public void Start() {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        gameOverPanel.SetActive(false);
        gameCompletedPanel.SetActive(false);
    }

    public void removeLife() {
        lives -= 1;
        livesText.text = "Lives: " + lives;

        if(lives == 0) {
            gameOverPanel.SetActive(true);
        }
    }
    public void addScore(int score) {
        this.score += score;
        scoreText.text = "Score: " + this.score;

        if (this.score % 10 == 0) {
            //ball.gameObject.GetComponent<BrickBehaviour>().speedUp();
        }

        if (brickHolder.transform.childCount == 1) {
            gameCompletedPanel.SetActive(true);
        }
    }

    public void nextLevel() {
        SceneManager.LoadScene("Level" + (level + 1));
        level += 1;
    }



    public void restartGame() {
        SceneManager.LoadScene("Level1");
    }
    public void exitGame() {
        SceneManager.LoadScene("Main Menu");
    }
}
