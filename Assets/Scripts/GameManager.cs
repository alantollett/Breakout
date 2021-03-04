using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private Text livesText;
    [SerializeField] private Text scoreText;
    [SerializeField] private int lives = 5;
    private int score = 0;

    public void Start() {
        livesText.text = "Lives: " + lives;
        scoreText.text = "Score: " + score;
    }

    public void removeLife() {
        lives -= 1;
        livesText.text = "Lives: " + lives;
    }
}
