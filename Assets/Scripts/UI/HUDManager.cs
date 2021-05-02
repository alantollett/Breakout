using UnityEngine;
using UnityEngine.UI;

 
public class HUDManager : MonoBehaviour {

    [SerializeField] private Text livesText;
    [SerializeField] private Text scoreText;

    private void OnEnable() {
        LevelManager.OnLifeLost += SetLivesText;
        LevelManager.OnScoreChange += SetScoreText;
    }

    private void OnDisable() {
        LevelManager.OnLifeLost -= SetLivesText;
        LevelManager.OnScoreChange -= SetScoreText;
    }

    private void SetLivesText(int lives) {
        livesText.text = "Lives: " + lives;
    }

    private void SetScoreText(int score) {
        scoreText.text = "Score: " + score;
    }
}