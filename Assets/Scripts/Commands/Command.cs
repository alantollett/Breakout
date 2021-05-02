using UnityEngine;

[System.Serializable]
public abstract class Command {

    protected IEntity entity;
    protected int frame;

    public Command(IEntity entity, int frame) {
        this.entity = entity;
        this.frame = frame;
    }

    public float getFrame() { return frame; }

    public abstract void Execute();

    public override abstract string ToString();
}
