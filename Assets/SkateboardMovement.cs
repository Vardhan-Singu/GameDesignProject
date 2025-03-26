using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 5f;
    public float friction = 0.999f;
    public float boostAcceleration = 15f;
    public float boostMaxSpeed = 8f;
    public float boostDuration = 0.5f;
    public float naturalDrag = 0.02f;
    public float speedDecayRate = 0.99f; // How gradually speed returns to normal
    public float brakeForce = 3f;
    public float torqueForce = 5f; // ✅ New: torque amount for rotation

    private float move;
    private Rigidbody2D rb;
    private bool isBoosting = false;
    private float boostEndTime = 0f;
    private float currentMaxSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = 0;
        rb.freezeRotation = false; // ✅ Let physics rotate the board
        currentMaxSpeed = maxSpeed;
    }

    void Update()
    {
        // Detect Shift key tap
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            isBoosting = true;
            boostEndTime = Time.time + boostDuration;
            currentMaxSpeed = boostMaxSpeed; // Temporarily increase max speed
        }

        // Gradually let the max speed decay instead of resetting immediately
        if (Time.time > boostEndTime && isBoosting)
        {
            isBoosting = false;
        }

        if (!isBoosting && currentMaxSpeed > maxSpeed)
        {
            currentMaxSpeed *= speedDecayRate; // Slowly return to normal speed
            if (currentMaxSpeed < maxSpeed) currentMaxSpeed = maxSpeed; // Prevent undershooting
        }

        // ✅ Mid-air tilt using torque
        if (Mathf.Abs(rb.linearVelocity.y) > 0.1f)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                rb.AddTorque(torqueForce * Time.deltaTime, ForceMode2D.Force); // Tilt counter-clockwise
            }
            if (Input.GetKey(KeyCode.E))
            {
                rb.AddTorque(-torqueForce * Time.deltaTime, ForceMode2D.Force); // Tilt clockwise
            }
        }
    }

    void FixedUpdate()
    {
        move = Input.GetAxis("Horizontal");
        float currentAcceleration = isBoosting ? boostAcceleration : acceleration;

        if (move != 0)
        {
            rb.AddForce(new Vector2(move * currentAcceleration, 0), ForceMode2D.Force);
            rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocity.x, -currentMaxSpeed, currentMaxSpeed), rb.linearVelocity.y);
        }

        // Brake when holding a key (e.g., Down Arrow or S)
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * (1f - brakeForce * Time.fixedDeltaTime), rb.linearVelocity.y);
        }
    }
}
