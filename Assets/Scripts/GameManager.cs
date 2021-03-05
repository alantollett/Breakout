using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private Text livesText;
    [SerializeField] private Text scoreText;
    [SerializeField] private int lives = 5;
    [SerializeField] private Transform ball;
    [SerializeField] private GameObject gameOverPanel;
    private int score = 0;

    public void Start() {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
        gameOverPanel.SetActive(false);
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

        if(this.score % 10 == 0) {
            ball.gameObject.GetComponent<BallController>().speedUp();
        }
    }

    public void restartGame() {
        Debug.Log("Restarting");
        SceneManager.LoadScene("Level1");
    }
}
