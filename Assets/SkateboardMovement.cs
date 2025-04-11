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
    public float speedDecayRate = 0.99f;
    public float brakeForce = 3f;
    public float torqueForce = 100f;
    public float CastDistance;
    public Vector2 boxSize;
    public LayerMask groundLayer;
    private float move;
    private Rigidbody2D rb;
    private bool isBoosting = false;
    private float boostEndTime = 0f;
    private float currentMaxSpeed;
    private bool isGrounded;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = 0;
        rb.freezeRotation = false;
        currentMaxSpeed = maxSpeed;
    }
    
    public bool CheckIfGrounded()
    {
        Debug.Log("GroundedCalled: " + isGrounded);
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, CastDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position-transform.up * CastDistance, boxSize);
    }

    void Update()
    {
        Debug.Log("Grounded: " + isGrounded);
        isGrounded = CheckIfGrounded();

        if ( (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
        {
        isBoosting = true;
        boostEndTime = Time.time + boostDuration;
        currentMaxSpeed = boostMaxSpeed;
        }

        if (Time.time > boostEndTime && isBoosting)
        {
        isBoosting = false;
        }

        if (!isBoosting && currentMaxSpeed > maxSpeed)
        {
        currentMaxSpeed *= speedDecayRate;
        if (currentMaxSpeed < maxSpeed) currentMaxSpeed = maxSpeed;
        }

        if (Mathf.Abs(rb.linearVelocity.y) > 0.1f)
        {
        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddTorque(torqueForce * Time.deltaTime, ForceMode2D.Force);
        }
        if (Input.GetKey(KeyCode.E))
        {
            rb.AddTorque(-torqueForce * Time.deltaTime, ForceMode2D.Force);
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
            //rb.linearVelocity = new Vector2(Mathf.Clamp(rb.linearVelocity.x, -currentMaxSpeed, currentMaxSpeed), rb.linearVelocity.y);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x * (1f - brakeForce * Time.fixedDeltaTime), rb.linearVelocity.y);
        }
    }

}
