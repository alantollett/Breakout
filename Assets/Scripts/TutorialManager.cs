using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    [SerializeField] private GameObject[] messages;
    private int currentMessage = 0;
    private LevelManager levelManager;

    private void Awake() {
        levelManager = GetComponent<LevelManager>();
        StartCoroutine(displayMessage(1, 4));
    }

    private void Update() {
        for (int i = 0; i < messages.Length; i++) {
            if(i == currentMessage) {
                messages[i].SetActive(true);
            }else {
                messages[i].SetActive(false);
            }
        }
    }

    public void setMessageIndex(int index) {
        // check if tutorial is finished and return
        if (currentMessage == -1) return;

        // otherwise, update the message if the message requested
        // is furhter in the tutorial than the current message...
        if (index > currentMessage) {
            // display the message with a 3s delay
            StartCoroutine(displayMessage(index, 3));

            // if the final input message is requested,
            // display the "continue playing" message after a delay
            // and then remove all messages after a further delay
            if (index == 3) {
                StartCoroutine(displayMessage(4, 6));
                StartCoroutine(displayMessage(-1, 11));
            }
        }
    }

    private IEnumerator displayMessage(int index, int seconds) {
        yield return new WaitForSeconds(seconds);
        currentMessage = index;
    }

}
