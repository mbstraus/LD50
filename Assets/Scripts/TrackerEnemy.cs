using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerEnemy : MonoBehaviour
{
    private PlayerLight Player;
    private Rigidbody2D rb;
    private float HitCooldownRemaining = 0f;
    private AudioSource audioSource;

    [SerializeField]
    public float DistanceToPlayerToBeginTracking = 20f;
    [SerializeField]
    public float MovementSpeed = 10f;
    [SerializeField]
    public float HitCooldownTime = 1f;
    [SerializeField]
    public int LightStrength = 1;
    [SerializeField]
    public float PushBackForce = 2f;
    [SerializeField]
    public PowerChargePickup PowerChargePickupPrefab;
    [SerializeField]
    public AudioClip SlimeDied;

    void Start() {
        Player = FindObjectOfType<PlayerLight>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update() {
        if (HitCooldownRemaining > 0f) {
            HitCooldownRemaining -= Time.deltaTime;
        }
    }

    void FixedUpdate() {
        if (HitCooldownRemaining <= 0f) {
            Vector2 heading = Player.transform.position - transform.position;
            float distance = heading.magnitude;
            if (distance <= DistanceToPlayerToBeginTracking) {
                Vector2 direction = heading / distance;
                rb.velocity = new Vector2(direction.x * MovementSpeed, rb.velocity.y);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
            Debug.Log("Collided with " + collision.gameObject.name);
            Player.ChangeCurrentLightPips(-LightStrength, true);
            Vector2 heading = Player.transform.position - transform.position;
            float distance = heading.magnitude;
            Vector2 direction = heading / distance;
            rb.velocity = new Vector2(-direction.x * PushBackForce, rb.velocity.y);
            HitCooldownRemaining = HitCooldownTime;
        }
    }

    public void Die() {
        PowerChargePickup instance = Instantiate(PowerChargePickupPrefab, transform.position, Quaternion.identity);
        instance.ChargeAmount = LightStrength;
        audioSource.PlayOneShot(SlimeDied);
        Destroy(gameObject);
    }
}
