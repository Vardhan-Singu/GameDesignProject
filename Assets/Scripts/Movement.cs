using UnityEngine;

public class Movement : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    public float CastDistance = 0.1f;
    public Vector2 boxSize = new Vector2(0.5f, 0.1f);
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private float targetSpeed = 0f;
    private float currentSpeed = 0f;
    public bool isGrounded { get; private set; }

    void Start()
    {
        // Try to find the Rigidbody2D on the child (Player)
        rb = GetComponentInChildren<Rigidbody2D>();
        
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on " + gameObject.name + " or its children");
        }
    }

    void Update()
    {
        // Check if grounded
        isGrounded = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, CastDistance, groundLayer);

        if (rb == null) return;

        float inputX = Input.GetAxisRaw("Horizontal");
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? runSpeed : walkSpeed;

        targetSpeed = inputX * speed;
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
    }

    void FixedUpdate()
    {
        if (rb == null || !isGrounded) return;

        rb.linearVelocity = new Vector2(currentSpeed, rb.linearVelocity.y);  // Updated to use velocity directly
    }

    public float GetSpeed()
    {
        return rb != null ? Mathf.Abs(rb.linearVelocity.x) : 0f;  // Updated to use velocity directly
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - transform.up * CastDistance, boxSize);
    }
}
