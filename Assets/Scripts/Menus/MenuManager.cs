using UnityEngine;

public class MenuManager : MonoBehaviour {

    private IMenuManager[] menus;

    public void Awake() {
        menus = gameObject.GetComponentsInChildren<IMenuManager>();
    }

    public void Start() {
        menus[0].enable(); // should be user/login menu
    }

    public void openMenu(int index) {
        for (int i = 0; i < menus.Length; i++) menus[i].disable();
        menus[index].enable();
    }
}









    /*
    [SerializeField] private Canvas gameOverlayCanvas;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private Text livesText;
    [SerializeField] private Text scoreText;


    private Player player;
    private LevelManager levelManager;

    public void Awake() {
        levelManager = GetComponent<LevelManager>();
        player = levelManager.getPlayer();
    }

    public void Start() {
        disableAllMenus();
        userCanvas.gameObject.SetActive(true);
        loadDropdown();
    }

    public void Update() {
        if(player != null) {
            livesText.text = "Lives: " + player.getLives();
            scoreText.text = "Score: " + player.getScore();
        }
    }

    public void openMenu(string menu) {
        menu = menu.ToLower(); 
        disableAllMenus();
        
        if (menu.Equals("user")) {
            userCanvas.gameObject.SetActive(true);
        } else if (menu.Equals("main")) {
            mainCanvas.gameObject.SetActive(true);
        } else if (menu.Equals("levels")) {

        } else if (menu.Equals("game overlay")) {
            gameOverlayCanvas.gameObject.SetActive(true);
        } else if (menu.Equals("win")) {
            winPanel.SetActive(true);
            player.save();
        } else if (menu.Equals("lose")) {
            losePanel.SetActive(true);
            player.save();
        } else if (menu.Equals("recordings")) {
            recordingsCanvas.gameObject.SetActive(true);
            
            for(int i = 0; i < recordingScrollBarContent.transform.childCount; i++) {
                Destroy(recordingScrollBarContent.transform.GetChild(i).gameObject);
            }

            for (int i = 0; i < player.getRecordings().Count; i++) {
                GameObject button = Instantiate(recordingButtonPrefab);
                button.transform.SetParent(recordingScrollBarContent.transform);
                button.GetComponentInChildren<Text>().text = "RECORDING " + (i + 1);
                int level = i;
                button.GetComponent<Button>().onClick.AddListener(() => levelManager.loadRecording(player.getRecordings()[level], 0));
            }

        }
    }
    */
