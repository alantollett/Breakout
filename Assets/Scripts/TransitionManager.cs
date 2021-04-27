using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour {

    [SerializeField] private Animator transition;
    [SerializeField] private float transitionTime;

    public void LoadScene(int buildIndex) {
        StartCoroutine(TransitionTo(buildIndex));
    }

    IEnumerator TransitionTo(int buildIndex) {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(buildIndex);
    }
}
