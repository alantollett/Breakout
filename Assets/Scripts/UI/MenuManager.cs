using UnityEngine;

public class MenuManager : MonoBehaviour {

    private IMenuManager[] menus;

    public void Awake() {
        menus = gameObject.GetComponentsInChildren<IMenuManager>();
    }

    public void Start() {
        menus[0].enable();
    }

    public void openMenu(int index) {
        for (int i = 0; i < menus.Length; i++) menus[i].disable();
        menus[index].enable();
    }
}
