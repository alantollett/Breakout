using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    [SerializeField] private Text message;
    private Queue<int> messagesQ = new Queue<int>();
    private int messageIndex;

    private string[] messages = { 
        "The aim of the game is to destroy all of the bricks.",
        "To do so you will need to move your paddle.\nPress <- or -> to Move.",
        "You will also need to fire the ball at the bricks.\nPress Space to Fire.",
        "Keep playing until you destroy all the bricks or run out of lives!",
        "Pause at any point by pressing Esc!",
        ""
    };

    private void Awake() {
        messagesQ.Enqueue(0);
        messagesQ.Enqueue(1);
        messageIndex = 2;
        StartCoroutine(queueCheck());
    }

    private void OnEnable() {
        InputManager.OnMove += Move;
        InputManager.OnFire += Fire;
    }

    private void OnDisable() {
        InputManager.OnMove -= Move;
        InputManager.OnFire -= Fire;
    }

    private IEnumerator queueCheck() {
        for( ; ; ) {
            Debug.Log(messagesQ.Count);
            if(messagesQ.Count > 0) message.text = messages[messagesQ.Dequeue()];
            yield return new WaitForSeconds(4);
        }
    }

    private void Move() {
        if (messageIndex == 2) {
            messagesQ.Enqueue(messageIndex);
            messageIndex++;
        }
    }

    private void Fire() {
        if (messageIndex == 3) {
            messagesQ.Enqueue(3);
            messagesQ.Enqueue(4);
            messagesQ.Enqueue(5);
            messageIndex = 5;
        }
    }

}
