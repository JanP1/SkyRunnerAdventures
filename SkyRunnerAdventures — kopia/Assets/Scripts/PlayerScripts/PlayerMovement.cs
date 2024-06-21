/*using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float maxSpeed = 15f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private float jumpCooldown = 0.5f; // Cooldown time in seconds
    [SerializeField] private float accelerationTime = 0.5f; // Time taken to reach max speed from rest

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float speed = 0f;
    private bool isFacingRight = true;
    private bool canJump = true;
    private float jumpTimer = 0f; // Timer to track jump cooldown
    private float horizontal;

    private float currentAccelerationTime = 0f; // Current time for acceleration

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Update the jump cooldown timer
        if (!canJump)
        {
            jumpTimer -= Time.deltaTime;
            if (jumpTimer <= 0f)
            {
                canJump = true; // Reset the flag when the cooldown ends
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            // If the player is grounded or has enough velocity to allow for bunny hopping and the jump is off cooldown
            if ((IsGrounded() || Mathf.Abs(rb.velocity.y) < 0.1f) && canJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                canJump = false; // Set the flag to prevent jumping until the cooldown ends
                jumpTimer = jumpCooldown; // Set the cooldown timer
            }
        }

        // If the player releases the "W" key mid-air, reduce the upward velocity to allow for variable jump height
        if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0f)
        {
            // If the player is trying to move, accelerate until max speed
            currentAccelerationTime = Mathf.Min(currentAccelerationTime + Time.fixedDeltaTime, accelerationTime);
            speed = Mathf.Lerp(0f, maxSpeed, currentAccelerationTime / accelerationTime);
        }
        
        // Update horizontal velocity if there is input

        // ----------TESTING----------------
        if (Mathf.Abs(horizontal) > 0.1f )
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        // --------------------------------

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
*/


/*using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float maxSpeed = 15f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private float jumpCooldown = 0.5f; // Cooldown time in seconds
    [SerializeField] private float accelerationTime = 0.5f; // Time taken to reach max speed from rest
    [SerializeField] public float decelerationFactor = 0.95f; // Deceleration factor
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private float speed = 0f;
    private bool isFacingRight = true;
    private bool canJump = true;
    private float jumpTimer = 0f; // Timer to track jump cooldown
    private float horizontal;

    private float currentAccelerationTime = 0f; // Current time for acceleration

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        // Update the jump cooldown timer
        if (!canJump)
        {
            jumpTimer -= Time.deltaTime;
            if (jumpTimer <= 0f)
            {
                canJump = true; // Reset the flag when the cooldown ends
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            // If the player is grounded or has enough velocity to allow for bunny hopping and the jump is off cooldown
            if ((IsGrounded() || Mathf.Abs(rb.velocity.y) < 0.1f) && canJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                canJump = false; // Set the flag to prevent jumping until the cooldown ends
                jumpTimer = jumpCooldown; // Set the cooldown timer
            }
        }

        // If the player releases the "W" key mid-air, reduce the upward velocity to allow for variable jump height
        if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0f)
        {
            // If the player is trying to move, accelerate until max speed
            currentAccelerationTime = Mathf.Min(currentAccelerationTime + Time.fixedDeltaTime, accelerationTime);
            speed = Mathf.Lerp(0f, maxSpeed, currentAccelerationTime / accelerationTime);
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        else
        {
            // If no horizontal input is detected, gradually decelerate
            rb.velocity = new Vector2(rb.velocity.x * decelerationFactor, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}


*/




using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float maxSpeed = 15f;
    [SerializeField] private float jumpingPower = 16f;
    [SerializeField] private float jumpCooldown = 0.5f; // Cooldown time in seconds
    [SerializeField] private float accelerationTime = 0.5f; // Time taken to reach max speed from rest
    [SerializeField] public float decelerationFactor = 0.95f; // Deceleration factor
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public Animator animator;

    private float speed = 0f;
    private bool isFacingRight = true;
    private bool canJump = true;
    private float jumpTimer = 0f; // Timer to track jump cooldown
    private float horizontal;

    private float currentAccelerationTime = 0f; // Current time for acceleration

    private Transform originalParent;
    private Transform platformTransform;

    void Start()
    {
        originalParent = transform.parent;
    }

    void Update()
    {


        if (!IsGrounded())
        {
            animator.SetBool("isJumping", true);
        }
        else
        {
            animator.SetBool("isJumping", false);
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        // Update the jump cooldown timer
        if (!canJump)
        {
            jumpTimer -= Time.deltaTime;
            if (jumpTimer <= 0f)
            {
                canJump = true; // Reset the flag when the cooldown ends
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            // If the player is grounded or has enough velocity to allow for bunny hopping and the jump is off cooldown
            if ((IsGrounded() || Mathf.Abs(rb.velocity.y) < 0.1f) && canJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                canJump = false; // Set the flag to prevent jumping until the cooldown ends
                jumpTimer = jumpCooldown; // Set the cooldown timer
            }
        }

        // If the player releases the "W" key mid-air, reduce the upward velocity to allow for variable jump height
        if (Input.GetKeyUp(KeyCode.W) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate()
    {

        

        horizontal = Input.GetAxisRaw("Horizontal");

        if (horizontal != 0f)
        {
            // If the player is trying to move, accelerate until max speed
            currentAccelerationTime = Mathf.Min(currentAccelerationTime + Time.fixedDeltaTime, accelerationTime);
            speed = Mathf.Lerp(0f, maxSpeed, currentAccelerationTime / accelerationTime);
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        }
        else
        {
            // If no horizontal input is detected, gradually decelerate
            rb.velocity = new Vector2(rb.velocity.x * decelerationFactor, rb.velocity.y);
        }

        // Adjust velocity to match platform if the player is on the platform
        if (platformTransform != null)
        {
            Vector3 newPosition = transform.position;
            newPosition.x += horizontal * speed * Time.fixedDeltaTime;
            transform.position = newPosition;
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            platformTransform = collision.transform;
            transform.SetParent(platformTransform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MovingPlatform"))
        {
            transform.SetParent(originalParent);
            platformTransform = null;
        }
    }
}



