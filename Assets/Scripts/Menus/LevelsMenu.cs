using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LevelManager))]
[RequireComponent(typeof(EntityManager))]
public class LevelsMenu : IMenuManager {

    [SerializeField] private GameObject levelScrollBarContent;
    [SerializeField] private GameObject levelButtonPrefab;

    private EntityManager entityManager;
    private LevelManager levelManager;

    public void Awake() {
        levelManager = GetComponent<LevelManager>();
        entityManager = GetComponent<EntityManager>();
    }

    public override void enable() {
        canvas.gameObject.SetActive(true);
        loadLevelButtons();
    }

    public override void disable() {
        canvas.gameObject.SetActive(false);
        unloadLevelButtons();
    }

    private void loadLevelButtons() {
        for (int i = 0; i < levelManager.getNumLevels(); i++) {
            GameObject button = Instantiate(levelButtonPrefab);
            button.transform.SetParent(levelScrollBarContent.transform);

            Text text = button.GetComponentInChildren<Text>();
            if (i == 0) {
                text.text = "TUTORIAL";
            } else {
                text.text = "LEVEL " + i;
            }

            if (i < entityManager.getPlayer().getHighestLevelCompleted() + 2) {
                text.color = new Color(0, 255, 0);
                int level = i;
                button.GetComponent<Button>().onClick.AddListener(() => levelManager.loadLevel(level));
            } else {
                text.color = new Color(255, 0, 0);
            }
        }
    }

    private void unloadLevelButtons() {
        for (int i = 0; i < levelScrollBarContent.transform.childCount; i++) {
            Destroy(levelScrollBarContent.transform.GetChild(i).gameObject);
        }
    }

}
