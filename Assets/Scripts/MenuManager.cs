using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [SerializeField] private GameObject levelsPanel;
    [SerializeField] private GameObject mainPanel;

    public void Start() {
        mainPanel.SetActive(true);
        levelsPanel.SetActive(false);
    }

    public void openLevels() {
        levelsPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void openMain() {
        mainPanel.SetActive(true);
        levelsPanel.SetActive(false);
    }

    public void quitGame() {
        Application.Quit();
    }

    public void loadLevel(int level) {
        SceneManager.LoadScene("Level" + level);
    }
}
