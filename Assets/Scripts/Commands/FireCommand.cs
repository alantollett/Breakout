using UnityEngine;

public class FireCommand : Command {

    private float angle;
    private float speed;

    public FireCommand(IEntity entity, float angle, float speed) : base(entity) {
        this.angle = angle;
        this.speed = speed;
    }

    public override void Execute() {
        entity.rb.velocity = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)) * speed;
    }

    public override string ToString() {
        return "FireCommand:" + angle + "," + speed;
    }
}
