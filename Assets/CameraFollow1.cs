using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform target;
    public float fallOffset = 5f;
    public float smoothFallSpeed = 2f;
    public float minZoom = 5f;
    public float maxZoom = 15f;
    public float zoomSpeed = 2f;

    public float cameraAccelerationTime = 2f; // Time to reach full follow speed

    private Rigidbody2D rb;
    private float currentYOffset = 0f;
    private Camera cam;
    private float followSpeedMultiplier = 0f;
    private float accelerationTimer = 0f;

    void Start()
    {
        rb = target.GetComponent<Rigidbody2D>();
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        if (target == null || rb == null) return;

        // Smooth start using easing
        if (followSpeedMultiplier < 1f)
        {
            accelerationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(accelerationTimer / cameraAccelerationTime);
            followSpeedMultiplier = Mathf.SmoothStep(0f, 1f, t); // Eased transition from 0 to 1
        }

        // Vertical offset for falling
        float targetYOffset = rb.linearVelocity.y < 0 ? -fallOffset : 0f;
        currentYOffset = Mathf.Lerp(currentYOffset, targetYOffset, smoothFallSpeed * Time.deltaTime);

        // Dynamic follow speed based on velocity and easing
        float speed = rb.linearVelocity.magnitude;
        float dynamicFollowSpeed = Mathf.Clamp(speed, 1f, 10f) * followSpeedMultiplier;

        // Follow position
        Vector3 targetPos = new Vector3(target.position.x + 3, target.position.y + 4 + currentYOffset, -10f);
        transform.position = Vector3.Lerp(transform.position, targetPos, dynamicFollowSpeed * Time.deltaTime);

        // Zoom out based on speed
        float targetZoom = Mathf.Clamp(minZoom + speed, minZoom, maxZoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
    }
}