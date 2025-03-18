using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float acceleration = 10f;
    public float maxSpeed = 5f;
    public float friction = 0.98f;
    public float boostAcceleration = 15f;
    public float boostMaxSpeed = 8f;
    public float boostDuration = 0.5f;
    public float speedDecayRate = 0.99f; // How gradually speed returns to normal

    private float move;
    private Rigidbody2D rb;
    private bool isBoosting = false;
    private float boostEndTime = 0f;
    private float currentMaxSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 0;
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
    }

    void FixedUpdate()
    {
        move = Input.GetAxis("Horizontal");

        float currentAcceleration = isBoosting ? boostAcceleration : acceleration;

        if (move != 0)
        {
            rb.AddForce(new Vector2(move * currentAcceleration, 0), ForceMode2D.Force);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -currentMaxSpeed, currentMaxSpeed), rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x * friction, rb.velocity.y);
        }
    }
}