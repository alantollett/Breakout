using UnityEngine;

[System.Serializable]
public class MoveCommand : Command {

    private Vector2 direction;
    private float distance;

    public MoveCommand(IEntity entity, int frame, Vector2 direction, float distance) : base(entity, frame){
        this.direction = direction;
        this.distance = distance;
    }

    public override void Execute() {
        entity.transform.Translate(distance * direction);
    }


    public override string ToString() {
        return "MoveCommand:" + entity + "," + frame + "," + direction.x + "," + direction.y + "," + distance;
    }
}
