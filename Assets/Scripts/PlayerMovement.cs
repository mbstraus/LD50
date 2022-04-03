using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Animator Animator;
    private Vector2 MovementVector = Vector2.zero;
    private bool IsJumpHeld = false;
    private float JumpCheckDelay = 0f;
    private PlayerSounds soundPlayer;

    [SerializeField]
    public bool IsJumping = false;
    [SerializeField]
    public Rigidbody2D rb;
    [SerializeField]
    public float MovementSpeed = 10;
    [SerializeField]
    public float JumpForce = 10;
    [SerializeField]
    public float GroundDistanceCheck = 0.7f;
    [SerializeField]
    public float JumpCheckDelayTime = 0.1f;

    // Start is called before the first frame update
    void Start() {
        Animator = GetComponent<Animator>();
        soundPlayer = GetComponent<PlayerSounds>();
    }

    private void Update() {
        if (JumpCheckDelay > 0f) {
            JumpCheckDelay -= Time.deltaTime;
        } else {
            if (IsJumping && IsGrounded()) {
                Animator.SetBool("Is Jumping", false);
                IsJumping = false;
            }
        }
    }

    private void FixedUpdate() {
        rb.velocity = new Vector2(MovementVector.x * MovementSpeed, rb.velocity.y);
        if (IsJumpHeld && !IsJumping && IsGrounded()) {
            JumpCheckDelay = JumpCheckDelayTime;
            IsJumping = true;
            rb.velocity = new Vector2(0, JumpForce);
        }
    }

    public void Move(InputAction.CallbackContext callbackContext) {
        MovementVector = callbackContext.ReadValue<Vector2>();
        if (MovementVector == Vector2.zero) {
            Animator.SetBool("Is Moving", false);
        } else {
            Animator.SetBool("Is Moving", true);
            Animator.SetFloat("Horizontal", MovementVector.x);
        }
    }

    public void Jump(InputAction.CallbackContext callbackContext) {
        if (callbackContext.started && !IsJumpHeld && IsGrounded()) {
            soundPlayer.PlayJump();
            Animator.SetBool("Is Jumping", true);
            IsJumpHeld = true;
        }
        if (callbackContext.canceled) {
            IsJumpHeld = false;
        }
    }

    public bool IsGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, GroundDistanceCheck, LayerMask.GetMask("Ground"));
        if (hit.collider != null) {
            return true;
        }
        return false;
    }
}
