using UnityEngine;

public class LightBullet : MonoBehaviour
{
    private Rigidbody2D rb;

    public Vector2 MovementDirection { get; set; }

    [SerializeField]
    public float MovementSpeed = 5f;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + (MovementDirection * MovementSpeed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Destroy(gameObject);

        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            TrackerEnemy enemy = collision.collider.gameObject.GetComponent<TrackerEnemy>();
            enemy.Die();
        }
    }
}
