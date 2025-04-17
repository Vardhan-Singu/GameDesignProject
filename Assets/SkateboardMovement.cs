using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float acceleration = 10f;
    public float maxSpeed = 5f;
    public float friction = 0.999f;
    public float brakeForce = 3f;
    public float torqueForce = 100f;

    [Header("Boost Settings")]
    public float boostAcceleration = 15f;
    public float boostMaxSpeed = 8f;
    public float boostDuration = 0.5f;
    public float speedDecayRate = 0.99f;

    [Header("Ground Check")]
    public float CastDistance = 0.5f;
    public Vector2 boxSize = new Vector2(1f, 0.2f);
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isBoosting = false;
    private float boostEndTime = 0f;
    private float currentMaxSpeed;
    private float moveInput;

    // Public speed getter
    public float GetSpeed()
    {
        return rb != null ? rb.linearVelocity.magnitude : 0f;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = 0;
        rb.freezeRotation = false;
        currentMaxSpeed = maxSpeed;
    }

    void Update()
    {
        // Handle Boost Activation
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isBoosting = true;
            boostEndTime = Time.time + boostDuration;
            currentMaxSpeed = boostMaxSpeed;
        }

        // Handle Boost End
        if (Time.time > boostEndTime && isBoosting)
        {
            isBoosting = false;
        }

        // Gradually return to normal max speed after boost
        if (!isBoosting && currentMaxSpeed > maxSpeed)
        {
            currentMaxSpeed *= speedDecayRate;
            if (currentMaxSpeed < maxSpeed)
                currentMaxSpeed = maxSpeed;
        }

        // Handle Torque While Airborne
        if (Mathf.Abs(rb.linearVelocity.y) > 0.1f)
        {
            if (Input.GetKey(KeyCode.Q))
                rb.AddTorque(torqueForce * Time.deltaTime, ForceMode2D.Force);
            if (Input.GetKey(KeyCode.E))
                rb.AddTorque(-torqueForce * Time.deltaTime, ForceMode2D.Force);
        }
    }

    void FixedUpdate()
    {
        moveInput = Input.GetAxisRaw("Horizontal"); // Raw for snappy input
        float currentAcceleration = isBoosting ? boostAcceleration : acceleration;

        if (moveInput != 0)
        {
            rb.AddForce(new Vector2(moveInput * currentAcceleration, 0f), ForceMode2D.Force);
        }

        // Clamp speed manually
        rb.linearVelocity = new Vector2(
            Mathf.Clamp(rb.linearVelocity.x, -currentMaxSpeed, currentMaxSpeed),
            rb.linearVelocity.y
        );

        // Apply Brakes
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x * (1f - brakeForce * Time.fixedDeltaTime),
                rb.linearVelocity.y
            );
        }
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, CastDistance, groundLayer);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - transform.up * CastDistance, boxSize);
    }
}