using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    [SerializeField] private GameObject[] messages;
    private int currentMessage = 0;

    private void Awake() {
        // show aim message for 5s then move to movement message
        StartCoroutine(displayMessage(1, 5));
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
            // display the message after a 2s delay
            StartCoroutine(displayMessage(index, 2));

            if (index == 3) {
                // if final message, after 10s remove all messages
                StartCoroutine(displayMessage(-1, 10));
            }
        }
    }

    private IEnumerator displayMessage(int index, int seconds) {
        yield return new WaitForSeconds(seconds);
        currentMessage = index;
    }

}
