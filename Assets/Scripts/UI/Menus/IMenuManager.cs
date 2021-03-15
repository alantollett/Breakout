using UnityEngine;

public abstract class IMenuManager : MonoBehaviour {

    [SerializeField] protected Canvas canvas;

    public abstract void enable();
    public abstract void disable();

}
