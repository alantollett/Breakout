using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(LevelManager))]
[RequireComponent(typeof(MenuManager))]
public class InputManager : MonoBehaviour {

    private LevelManager levelManager;
    private MenuManager menuManager;
    private TutorialManager tutorialManager;

    public void Start() {
        levelManager = GetComponent<LevelManager>();
        menuManager = GetComponent<MenuManager>();
        tutorialManager = GetComponent<TutorialManager>();
    }

    public void Move(InputAction.CallbackContext context) {
        if (levelManager != null) levelManager.getPlayer().move(context);
        if (tutorialManager != null) tutorialManager.setMessageIndex(2);
    }
    public void Fire(InputAction.CallbackContext context) {
        if (levelManager != null) levelManager.getBall().fire(context);
        if (tutorialManager != null) tutorialManager.setMessageIndex(3);
    }

    public void Pause(InputAction.CallbackContext context) {
        if (levelManager != null && context.started && !levelManager.isPaused()) {
            levelManager.pause();
            menuManager.setPauseMenuVisible(true);
        }
    }

}
