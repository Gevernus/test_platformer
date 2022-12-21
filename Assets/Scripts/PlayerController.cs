using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 10.0f;
    public float jumpGravity = 3.0f;
    public float fallGravity = 10.0f;
    public Rigidbody2D rb;
    public PlayerState state = PlayerState.Idle;
    public LayerMask groundLayer;
    public LayerMask slideLayer;
    public float groundDetectionRadius = 0.05f;
    public float slideDetectionRadius = 0.5f;
    public FollowController companionFollow;

    // Update is called once per frame
    void Update()
    {
        // Check if the player is on the slide
        var isSlide = IsSlide();

        if (isSlide)
        {
            state = PlayerState.Slide;
            // Slide the player
            rb.velocity = new Vector2(speed, -speed);
        } else
        {
            // Check if the player is on the ground
            var isGrounded = IsGrounded();

            var left = Input.GetButton("Left");
            var right = Input.GetButton("Right");

            // Move the player
            rb.velocity = new Vector2(right ? speed : left ? -speed : 0, rb.velocity.y);

            if (isGrounded)
            {
                // Check for jump input
                if (Input.GetButtonDown("Jump"))
                {
                    // Jump
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    state = PlayerState.Jump;

                }
                else if (rb.velocity.x != 0 && rb.velocity.y == 0)
                {
                    state = PlayerState.Run;
                }
                else if (rb.velocity.y == 0)
                {
                    state = PlayerState.Idle;
                }
            }

            rb.gravityScale = rb.velocity.y > 0 ? jumpGravity : fallGravity;
        }

        //Check sprite direction
        var localScale = transform.localScale;
        if (rb.velocity.x < 0)
        {
            localScale.x = -1;
            companionFollow.Direction(false);
        } else if (rb.velocity.x > 0)
        {
            localScale.x = 1;
            companionFollow.Direction(true);
        }
        transform.localScale = localScale;
    }

    // Function to check if the player is on the ground
    bool IsGrounded()
    {
        // Create an overlap circle at the player's feet
        Vector2 feetPos = new Vector2(transform.position.x, transform.position.y - 0.5f);
        bool overlap = Physics2D.OverlapCircle(feetPos, groundDetectionRadius, groundLayer);
        return overlap;
    }

    bool IsSlide()
    {
        // Create an overlap circle at the player's slide position
        Vector2 slidePos = new Vector2(transform.position.x, transform.position.y);
        bool overlap = Physics2D.OverlapCircle(slidePos, slideDetectionRadius, slideLayer);
        return overlap;
    }
}

public enum PlayerState
{
    Idle,
    Run,
    Jump,
    Slide
}
