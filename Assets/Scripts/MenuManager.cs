using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public void startGame() {
        SceneManager.LoadScene("Level1");
    }
    public void quitGame() {
        Application.Quit();
    }

}
