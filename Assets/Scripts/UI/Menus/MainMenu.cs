using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntityManager))]
[RequireComponent(typeof(MenuManager))]
public class MainMenu : IMenuManager {

    private EntityManager entityManager;
    private MenuManager menuManager;

    public void Awake() {
        entityManager = GetComponent<EntityManager>();
        menuManager = GetComponent<MenuManager>();
    }

    public override void enable() {
        canvas.gameObject.SetActive(true);
    }

    public override void disable() {
        canvas.gameObject.SetActive(false);
    }

    public void logout() {
        entityManager.getPlayer().unLoad();
        menuManager.openMenu(0);
    }

}
