using UnityEngine;
using UnityEngine.UI;

public class NewUserMenu : MonoBehaviour {

    [SerializeField] private InputField nameInput;
    [SerializeField] private Image paddleImage;
    [SerializeField] private Sprite[] paddles = new Sprite[5];
    [SerializeField] private GameObject playerPrefab;

    private int paddleId = 0;

    public void Awake() {
        StaticData.paddleId = -1;
    }

    public void nextPaddle() {
        paddleId++;
        if (paddleId > paddles.Length - 1) paddleId = 0;
        paddleImage.sprite = paddles[paddleId];
    }

    public void prevPaddle() {
        paddleId--;
        if (paddleId < 0) paddleId = paddles.Length - 1;
        paddleImage.sprite = paddles[paddleId];
    }

    public void createUser() {
        StaticData.playerName = nameInput.text;
        StaticData.paddleId = paddleId;

        // instantiate a player GO so that we can save it to disk
        Instantiate(playerPrefab);
    }
}
