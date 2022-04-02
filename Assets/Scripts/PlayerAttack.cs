using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    private Vector2 ShotDirection = Vector2.zero;
    private bool IsShooting = false;
    private bool HasShot = false;
    private float ShotCooldownTime = 0.5f;
    private float ShotCooldownRemaining = 0f;

    private PlayerLight PlayerLightController;

    [SerializeField]
    public LightBullet LightBulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PlayerLightController = GetComponent<PlayerLight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!HasShot && IsShooting) {
            // Shoot
            ShotCooldownRemaining = ShotCooldownTime;
            HasShot = true;
            PlayerLightController.ChangeCurrentLightPips(-1, false);
            LightBullet bullet = Instantiate(LightBulletPrefab, transform.position, Quaternion.identity);
            bullet.MovementDirection = ShotDirection;
        }

        if (ShotCooldownRemaining > 0f) {
            ShotCooldownRemaining -= Time.deltaTime;
        }

        if (ShotCooldownRemaining <= 0f && !IsShooting) {
            // Only allow for another shot if the button is released and the cooldown is over.
            HasShot = false;
        }
    }

    public void Shoot(InputAction.CallbackContext callbackContext) {
        if (callbackContext.started && !HasShot) {
            IsShooting = true;
            ShotDirection = callbackContext.ReadValue<Vector2>();
        }
        if (callbackContext.canceled) {
            IsShooting = false;
            ShotDirection = Vector2.zero;
        }
    }
}
