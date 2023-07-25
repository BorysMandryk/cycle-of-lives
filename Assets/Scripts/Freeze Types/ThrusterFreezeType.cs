using UnityEngine;

public class ThrusterFreezeType : FreezeType
{
    [SerializeField] private float _height = 4.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rigidbody.velocity.y < -0.01f)
        {
            Debug.Log(rigidbody.velocity.ToString("F8"));
            return;
        }
        rigidbody.velocity = Vector2.zero;

        // Якщо позиція об'єкта не збігається з центром колайдера
        //Vector2 thrusterCenter = GetComponent<Collider2D>().bounds.center;
        //Vector2 colliderCenter = collision.GetComponent<Collider2D>().bounds.center;
        //float distanceToObjectCenterY = thrusterCenter.y - colliderCenter.y;

        float distanceToObjectCenterY = transform.position.y - collision.transform.position.y;
        float force = Utils.HeightToForce(_height + distanceToObjectCenterY, rigidbody);
        rigidbody.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
}
