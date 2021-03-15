using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MenuManager))]
[RequireComponent(typeof(EntityManager))]
public class UserMenu : IMenuManager {

    [SerializeField] private Dropdown existingUser;
    [SerializeField] private InputField newUser;

    private EntityManager entityManager;
    private MenuManager menuManager;

    public void Awake() {
        entityManager = GetComponent<EntityManager>();
        menuManager = GetComponent<MenuManager>();
    }

    public override void enable() {
        canvas.gameObject.SetActive(true);
        loadDropdown();
        newUser.text = null;
    }

    public override void disable() {
        canvas.gameObject.SetActive(false);
    }

    public void login(bool isNewUser) {
        if (isNewUser && newUser.text != null) {
            entityManager.getPlayer().load(newUser.text.ToLower(), true);
            menuManager.openMenu(1); // should be main menu
        } else if (!isNewUser && existingUser.value != 0) {
            entityManager.getPlayer().load(existingUser.options[existingUser.value].text.ToLower(), false);
            menuManager.openMenu(1); // should be main menu
        }
    }

    private void loadDropdown() {
        if (existingUser == null) {
            Debug.Log("Existing user dropdown not set...");
        } else {
            string path = Application.persistentDataPath + "/";
            string[] files = System.IO.Directory.GetFiles(path, "*.player");

            existingUser.options.Clear(); // clear existing options from editor
            existingUser.options.Add(new Dropdown.OptionData("select user")); // add a default option

            for (int i = 0; i < files.Length; i++) {
                string[] parts = files[i].Split('/');
                string playerName = parts[parts.Length - 1].Split('.')[0];
                existingUser.options.Add(new Dropdown.OptionData(playerName));
            }
        }
    }

}