using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    [SerializeField] private Canvas userCanvas;
    [SerializeField] private Dropdown existingUser;
    [SerializeField] private InputField newUser;
    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private Canvas levelsCanvas;


    public void Start() {
        userCanvas.gameObject.SetActive(true);
        
        // load all existing player names and populate the existing player dropdown menu with them
        string path = Application.persistentDataPath + "/";
        string[] files = System.IO.Directory.GetFiles(path, "*.player");

        existingUser.options.Clear(); // clear existing options from editor
        existingUser.options.Add(new Dropdown.OptionData("select user")); // add a default option

        for (int i = 0; i < files.Length; i++) {
            string[] parts = files[i].Split('/');
            string playerName = parts[parts.Length - 1].Split('.')[0];
            existingUser.options.Add(new Dropdown.OptionData(playerName));
        }
        
        

        // populate existingUser dropdown

        mainCanvas.gameObject.SetActive(false);
        levelsCanvas.gameObject.SetActive(false);
    }

    public void quitGame() {
        Application.Quit();
    }

    public void login() {
        if(existingUser.value != 0) {
            // existing user dropdown was edited, so create a user with that name...
            GameObject playerObject = Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);
            Player playerComponent = playerObject.GetComponent<Player>();
            playerComponent.initialise(existingUser.options[existingUser.value].text.ToLower(), false);

        } else if(newUser.text != null && newUser.text.Length > 0) {
            // new user text field was edited, so create a new user with that name...
            GameObject playerObject = Instantiate(playerPrefab, new Vector2(0, 0), Quaternion.identity);
            Player playerComponent = playerObject.GetComponent<Player>();
            playerComponent.initialise(newUser.text.ToLower(), true);

            // need to check if the user exists before doing this!!!!! loop through dropdown options... check if equal

        }
    }
}
