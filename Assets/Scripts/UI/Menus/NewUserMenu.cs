using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TransitionManager))]
public class NewUserMenu : MonoBehaviour {

    [SerializeField] private InputField nameInput;
    [SerializeField] private Image paddleImage;
    [SerializeField] private Sprite[] paddles = new Sprite[5];
    [SerializeField] private GameObject playerPrefab;

    private TransitionManager transitionManager;
    private int paddleId = 0;

    public void Awake() {
        transitionManager = GetComponent<TransitionManager>();
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

    public void login() {
        // provide a static point of access to the player name and paddle
        // as we need a way for the new scene to access the data
        StaticData.playerName = nameInput.text;

        // save the player to disk
        GameObject go = Instantiate(playerPrefab);
        go.SetActive(false);
        Player player = go.GetComponent<Player>();
        player.loadNew(nameInput.text, paddleId);
        player.save();

        // finally load the game menu scene
        transitionManager.GetComponent<TransitionManager>().LoadScene(3);
    }
}
