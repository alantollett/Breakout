using UnityEngine;
using UnityEngine.UI;

 
public class HUDManager : MonoBehaviour {

    [SerializeField] private Player player;
    [SerializeField] private Text livesText;
    [SerializeField] private Text scoreText;

    public void Update() {
        livesText.text = "Lives: " + player.getLives();
        scoreText.text = "Score: " + player.getScore();
    }

}