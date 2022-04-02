using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 MovementVector = Vector2.zero;
    private bool IsJumpHeld = false;
    private bool IsJumping = false;

    [SerializeField]
    public Rigidbody2D rb;
    [SerializeField]
    public float MovementSpeed = 10;
    [SerializeField]
    public float JumpForce = 10;
    [SerializeField]
    public float GroundDistanceCheck = 0.7f;

    // Start is called before the first frame update
    void Start() {
    }

    private void FixedUpdate() {
        //rb.MovePosition(rb.position + (MovementVector * MovementSpeed * Time.fixedDeltaTime));
        rb.velocity = new Vector2(MovementVector.x * MovementSpeed, rb.velocity.y);
        if (IsJumpHeld && !IsJumping && IsGrounded()) {
            IsJumping = true;
            rb.velocity = new Vector2(0, JumpForce);
        }
        if (IsJumping && IsGrounded()) {
            IsJumping = false;
        }
    }

    public void Move(InputAction.CallbackContext callbackContext) {
        MovementVector = callbackContext.ReadValue<Vector2>();
        //animator.SetBool("Is Moving", IsCharacterMoving);
        //animator.SetFloat("Horizontal", MovementVector.x);
        //animator.SetFloat("Vertical", MovementVector.y);
    }

    public void Jump(InputAction.CallbackContext callbackContext) {
        if (callbackContext.started && !IsJumpHeld && IsGrounded()) {
            IsJumpHeld = true;
        }
        if (callbackContext.canceled) {
            IsJumpHeld = false;
        }
    }

    public bool IsGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, GroundDistanceCheck, LayerMask.GetMask("Ground"));
        if (hit.collider != null) {
            Debug.Log("Player is grounded!");
            return true;
        }
        Debug.Log("Player is not grounded!");
        return false;
    }
}
