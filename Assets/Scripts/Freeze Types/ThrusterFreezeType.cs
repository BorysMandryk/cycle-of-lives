using UnityEngine;

public class ThrusterFreezeType : FreezeType
{
    [SerializeField] private float _thrustForce;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        rigidbody.velocity = Vector2.zero;
        rigidbody.AddForce(Vector2.up * _thrustForce, ForceMode2D.Impulse);
    }
}
