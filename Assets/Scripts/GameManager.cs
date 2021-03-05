using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private Text livesText;
    [SerializeField] private Text scoreText;
    [SerializeField] private int lives = 5;
    [SerializeField] private Transform ball;
    private int score = 0;

    public void Start() {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }

    public void removeLife() {
        lives -= 1;
        livesText.text = "Lives: " + lives;
    }
    public void addScore(int score) {
        this.score += score;
        scoreText.text = "Score: " + this.score;

        if(this.score % 10 == 0) {
            ball.gameObject.GetComponent<BallController>().speedUp();
        }
    }
}
