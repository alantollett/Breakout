using UnityEngine;

public class MoveCommand : Command {

    private Vector2 direction;
    private float distance;

    public MoveCommand(IEntity entity, Vector2 direction, float distance) : base(entity){
        this.direction = direction;
        this.distance = distance;
    }

    public override void Execute() {
        entity.transform.Translate(distance * direction);
    }


    public override string ToString() {
        return "MoveCommand:" + direction.x + "," + direction.y + "," + distance;
    }
}
