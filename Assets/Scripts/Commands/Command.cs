using UnityEngine;

public abstract class Command {

    protected IEntity entity;
    protected float time;

    public Command(IEntity entity, float time) {
        this.entity = entity;
        this.time = time;
    }

    public float getTime() { return time; }

    public abstract void Execute();

    public override abstract string ToString();
}
