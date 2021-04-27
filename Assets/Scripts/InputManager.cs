using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(CommandProcessor))]
public class InputManager : MonoBehaviour {

    private LevelManager levelManager;
    private MenuManager menuManager;

    public void Awake() {
        levelManager = GameObject.Find("GameManager").GetComponent<LevelManager>();
        menuManager = GameObject.Find("GameManager").GetComponent<MenuManager>();
    }

    public void Move(InputAction.CallbackContext context) {
        levelManager.getPlayer().move(context);
    }
    public void Fire(InputAction.CallbackContext context) {
        levelManager.getBall().fire(context);
    }

    public void Pause(InputAction.CallbackContext context) {
        if (context.started && !levelManager.isPaused()) {
            levelManager.pause();
            menuManager.setPauseMenuVisible(true);
        }
    }

}
