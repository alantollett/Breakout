using UnityEngine;
using UnityEngine.UI;

 
public class HUDManager : MonoBehaviour {

    [SerializeField] private Text livesText;
    [SerializeField] private Text scoreText;

    private Player player;

    public void Start() {
        player = GetComponent<LevelManager>().getPlayer();
    }

    public void Update() {
        if (player != null) {
            livesText.text = "Lives: " + player.getLives();
            scoreText.text = "Score: " + player.getScore();
        }
    }

}