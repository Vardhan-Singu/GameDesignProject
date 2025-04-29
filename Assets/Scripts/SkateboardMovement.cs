using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public AudioClip rollingSound;
    public AudioClip ollieSound;
    private AudioSource audioSource;
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
    public bool isGrounded;
    public bool isOnSkateboard = true;

    public float ollieForce = 8f;
    public float maxOllieForce = 12f;
    public float maxOllieChargeTime = 0.5f;
    private float ollieChargeStartTime;
    private bool isChargingOllie;
    public float ollieTiltForce = 5f;
    public float ollieTiltDuration = 0.1f;
    private float ollieTiltEndTime = 0f;

    private float targetVolume = 0f;
    private float move;
    private Rigidbody2D rb;
    private bool isBoosting = false;
    private float boostEndTime = 0f;
    private float currentMaxSpeed;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = 0;
        rb.freezeRotation = false;
        currentMaxSpeed = maxSpeed;
    }

    public bool CheckIfGrounded()
    {
        return Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, CastDistance, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position - transform.up * CastDistance, boxSize);
    }

    public float GetSpeed()
    {
        return rb.linearVelocity.magnitude;
    }

    void Update()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.R))
        {
            float zRotation = transform.rotation.eulerAngles.z;
            if (zRotation > 90f && zRotation < 270f)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rb.angularVelocity = 0f;
            }
        }

        isGrounded = CheckIfGrounded();

        if (!isOnSkateboard) return;

        if (Input.GetKeyDown(KeyCode.LeftAlt) || Input.GetKeyDown(KeyCode.RightAlt))
        {
            if (Time.timeScale == 1f)
                Time.timeScale = 0.3f;
            else
                Time.timeScale = 1f;
        }

        if (isGrounded && (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)))
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

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isChargingOllie = true;
            ollieChargeStartTime = Time.time;
        }

        if (isGrounded && isChargingOllie && Input.GetKeyUp(KeyCode.Space))
        {
            float chargeTime = Mathf.Clamp(Time.time - ollieChargeStartTime, 0, maxOllieChargeTime);
            float chargePercent = chargeTime / maxOllieChargeTime;
            float finalOllieForce = Mathf.Lerp(ollieForce, maxOllieForce, chargePercent);

            rb.AddForce(Vector2.up * finalOllieForce, ForceMode2D.Impulse);
            rb.AddTorque(-ollieTiltForce, ForceMode2D.Impulse);
            audioSource.PlayOneShot(ollieSound);
            ollieTiltEndTime = Time.time + ollieTiltDuration;

            isChargingOllie = false;
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
        /*
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOnSkateboard = !isOnSkateboard;
            Debug.Log("Skateboard active: " + isOnSkateboard);
        }
        */

        if (!isOnSkateboard)
            return;
    }
// MAKE NOISE LOUDER
    void FixedUpdate()
    {
    if (!isOnSkateboard) return;

    move = Input.GetAxis("Horizontal");
    float currentAcceleration = isBoosting ? boostAcceleration : acceleration;

    if (isGrounded && move != 0)
    {
        rb.AddForce(new Vector2(move * currentAcceleration, 0), ForceMode2D.Force);
    }

    float horizontalSpeed = Mathf.Abs(rb.linearVelocity.x);

    if (isGrounded && horizontalSpeed > 0.1f) // <-- ADDED isGrounded check here
    {
        if (!audioSource.isPlaying || audioSource.clip != rollingSound)
        {
            audioSource.clip = rollingSound;
            audioSource.loop = true;
            audioSource.pitch = Random.Range(0.95f, 1.05f);
            audioSource.Play();
        }

        float maxVolumeSpeed = boostMaxSpeed;
        targetVolume = Mathf.Clamp01(horizontalSpeed / maxVolumeSpeed)*2.5F;

        audioSource.pitch = 0.95f + (horizontalSpeed / maxVolumeSpeed) * 0.1f;
    }
    else
    {
        targetVolume = 0f;
    }

    audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, Time.deltaTime * 2f);

    if (audioSource.volume == 0f && audioSource.isPlaying)
    {
        audioSource.Stop();
    }

    if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x * (1f - brakeForce * Time.fixedDeltaTime), rb.linearVelocity.y);
    }
    }

}
