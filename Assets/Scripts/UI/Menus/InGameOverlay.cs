using UnityEngine;
using UnityEngine.UI;

public class InGameOverlay : IMenuManager {

    [SerializeField] private Text livesText;
    [SerializeField] private Text scoreText;

    private EntityManager entityManager;
    private Player player;

    public void Awake() {
        entityManager = GetComponent<EntityManager>();
        player = entityManager.getPlayer();
    }

    public override void enable() {
        canvas.gameObject.SetActive(true);
    }

    public override void disable() {
        canvas.gameObject.SetActive(false);
    }

    public void Update() {
        if (player != null) {
            livesText.text = "Lives: " + player.getLives();
            scoreText.text = "Score: " + player.getScore();
        }
    }

}
