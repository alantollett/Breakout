using UnityEngine;
using UnityEngine.InputSystem;

public class LevelManager : MonoBehaviour {

    [SerializeField] private int level;
    [SerializeField] private int lives = 3;
    private int score = 0;

    public static event System.Action<int> OnLevelWin; // level number
    public static event System.Action<bool> OnLevelPause; // if pause menu should show
    public static event System.Action<int> OnLifeLost; // remaining lives
    public static event System.Action<int> OnScoreChange; // current score
    public static event System.Action OnLevelLose;
    public static event System.Action OnLevelResume;

    private void OnEnable() {
        Brick.OnBrickBreak += AddScore;
        Ball.OnBallDeath += RemoveLife;
    }

    private void OnDisable() {
        Brick.OnBrickBreak -= AddScore;
        Ball.OnBallDeath -= RemoveLife;
    }

    private void Start() {
        // broadcast initial values for lives and score for hud
        OnLifeLost?.Invoke(lives);
        OnScoreChange?.Invoke(score);
    }

    private void AddScore(int score) {
        this.score += score;
        OnScoreChange?.Invoke(this.score);

        if (FindObjectsOfType<Brick>().Length == 1) {
            OnLevelPause?.Invoke(false);
            OnLevelWin?.Invoke(level);
        }
    }

    public void RemoveLife() {
        lives -= 1;
        OnLifeLost?.Invoke(lives);

        if (lives == 0) {
            OnLevelPause?.Invoke(false);
            OnLevelLose?.Invoke();
        }
    }

    public void Pause(InputAction.CallbackContext context) {
        if (context.started) {
            OnLevelPause?.Invoke(true);
        }
    }

    public void resume() {
        OnLevelResume?.Invoke();
    }

    public int getLevel() {
        return level;
    }
}
