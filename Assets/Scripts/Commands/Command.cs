using UnityEngine;
public abstract class Command {

    protected IEntity entity;
    protected float time;

    public Command(IEntity entity) {
        this.entity = entity;
        time = Time.timeSinceLevelLoad;
    }

    public abstract void Execute();
}
