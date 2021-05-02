using UnityEngine;

public class MenuManager : MonoBehaviour {

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject loseMenu;

    private void OnEnable() {
        // register for events
        LevelManager.OnLevelLose += ShowLoseMenu;
        LevelManager.OnLevelWin += ShowWinMenu;
        LevelManager.OnLevelPause += ShowPauseMenu;
        LevelManager.OnLevelResume += HidePauseMenu;
    }

    private void OnDisable() {
        // unregister for events
        LevelManager.OnLevelLose -= ShowLoseMenu;
        LevelManager.OnLevelWin -= ShowWinMenu;
        LevelManager.OnLevelPause -= ShowPauseMenu;
        LevelManager.OnLevelResume -= HidePauseMenu;
    }

    private void ShowPauseMenu(bool showMenu) => pauseMenu.SetActive(showMenu);
    private void HidePauseMenu() => pauseMenu.SetActive(false);

    private void ShowWinMenu(int _) => winMenu.SetActive(true);
    private void ShowLoseMenu() => loseMenu.SetActive(true);
}
