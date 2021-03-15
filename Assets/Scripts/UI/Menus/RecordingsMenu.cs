using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LevelManager))]
[RequireComponent(typeof(EntityManager))]
public class RecordingsMenu : IMenuManager {

    [SerializeField] private GameObject recordingScrollBarContent;
    [SerializeField] private GameObject recordingButtonPrefab;

    private EntityManager entityManager;
    private LevelManager levelManager;

    public void Awake() {
        levelManager = GetComponent<LevelManager>();
        entityManager = GetComponent<EntityManager>();
    }

    public override void enable() {
        canvas.gameObject.SetActive(true);
        loadRecordingButtons();
    }

    public override void disable() {
        canvas.gameObject.SetActive(false);
        unloadRecordingButtons();
    }

    private void loadRecordingButtons() {
        for (int i = 0; i < entityManager.getPlayer().getRecordings().Count; i++) {
            GameObject button = Instantiate(recordingButtonPrefab);
            button.transform.SetParent(recordingScrollBarContent.transform);
            button.GetComponentInChildren<Text>().text = "RECORDING " + (i + 1);
            int level = i;
            button.GetComponent<Button>().onClick.AddListener(() => levelManager.loadRecording(entityManager.getPlayer().getRecordings()[level], 0));
        }
    }

    private void unloadRecordingButtons() {
        for (int i = 0; i < recordingScrollBarContent.transform.childCount; i++) {
            Destroy(recordingScrollBarContent.transform.GetChild(i).gameObject);
        }
    }

}
