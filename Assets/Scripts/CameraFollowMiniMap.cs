using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMiniMap : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform target;
    public float fallOffset = 5f;
    public float smoothFallSpeed = 2f;
    public float minZoom = 5f;
    public float maxZoom = 7f; // Lowered max zoom
    public float zoomSpeed = 2f;
    public float cameraAccelerationTime = 2f;

    private Rigidbody2D rb;
    private float currentYOffset = 0f;
    private Camera cam;
    private float followSpeedMultiplier = 0f;
    private float accelerationTimer = 0f;

    void Start()
    {
        cam = GetComponent<Camera>();
        SetTarget(target);
    }

    void Update()
    {
        if (target == null || rb == null) return;

        if (followSpeedMultiplier < 1f)
        {
            accelerationTimer += Time.deltaTime;
            float t = Mathf.Clamp01(accelerationTimer / cameraAccelerationTime);
            followSpeedMultiplier = Mathf.SmoothStep(0f, 1f, t);
        }

        float targetYOffset = rb.linearVelocity.y < 0 ? -fallOffset : 0f;
        currentYOffset = Mathf.Lerp(currentYOffset, targetYOffset, smoothFallSpeed * Time.deltaTime);

        float speed = rb.linearVelocity.magnitude;
        float dynamicFollowSpeed = Mathf.Clamp(speed, 1f, 10f) * followSpeedMultiplier;

        Vector3 targetPos = new Vector3(target.position.x + 3, target.position.y + 4 + currentYOffset, -10f);
        transform.position = Vector3.Lerp(transform.position, targetPos, dynamicFollowSpeed * Time.deltaTime);

        // Smaller zoom effect
        float targetZoom = Mathf.Clamp(minZoom + speed * 0.1f, minZoom, maxZoom);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        if (target != null)
        {
            rb = target.GetComponent<Rigidbody2D>();
            accelerationTimer = 0f;
            followSpeedMultiplier = 0f;
        }
    }
}
