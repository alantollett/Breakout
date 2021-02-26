using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BallController : MonoBehaviour
{
    [SerializeField] private float initialSpeed = 2f;
    private Rigidbody2D rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnEnable()
    {
        Vector2 velocity = new Vector2(Random.Range(-initialSpeed, initialSpeed), Random.Range(-initialSpeed, initialSpeed));
        rb.velocity = velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Destroyable")
        {
            Destroy(collision.gameObject);
        }
    }

}
