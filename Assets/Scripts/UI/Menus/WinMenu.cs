using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinMenu : IMenuManager {

    public override void enable() {
        canvas.gameObject.SetActive(true);

        // add next level button and re-arrange via code, i.e. if there is a next level
    }

    public override void disable() {
        canvas.gameObject.SetActive(false);
    }

}
