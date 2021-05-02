using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    [SerializeField] private AudioSource bounceSound;

    private void OnEnable() {
        Ball.OnBallBounce += PlaySound;
    }

    private void OnDisable() {
        Ball.OnBallBounce -= PlaySound;
    }

    private void PlaySound() {
        bounceSound.Play();
    }
}
