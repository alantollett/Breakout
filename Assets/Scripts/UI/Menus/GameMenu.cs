using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameMenu : MonoBehaviour {

    [SerializeField] private Text menuSubtitle;

    public void Awake() {
        menuSubtitle.text = "Logged in as: " + StaticData.playerName;
    }

}
