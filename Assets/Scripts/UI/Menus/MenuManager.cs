using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject loseMenu;

    public void setPauseMenuVisible(bool visible) {
        pauseMenu.SetActive(visible);
    }
    public void setWinMenuVisible(bool visible) {
        winMenu.SetActive(visible);
    }
    public void setLoseMenuVisible(bool visible) {
        loseMenu.SetActive(visible);
    }
}
