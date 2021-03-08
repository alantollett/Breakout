using UnityEngine;

public interface IEntity {
    Transform transform { get; }
    Rigidbody2D rb { get; }
}

