using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseMenu : IMenuManager {

    public override void enable() {
        canvas.gameObject.SetActive(true);
    }

    public override void disable() {
        canvas.gameObject.SetActive(false);
    }

}
