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

    private Rigidbody2D rb;
    private float currentYOffset = 0f;
    private Camera cam;

    void Start()
    {
        rb = target.GetComponent<Rigidbody2D>();
        cam = GetComponent<Camera>();
    }

    void Update()
{
    // Vertical offset for falling
    float targetYOffset = rb.linearVelocity.y < 0 ? -fallOffset : 0f;
    currentYOffset = Mathf.Lerp(currentYOffset, targetYOffset, smoothFallSpeed * Time.deltaTime);

    // Dynamically adjust follow speed based on velocity
    float speed = rb.linearVelocity.magnitude;
    float dynamicFollowSpeed = Mathf.Clamp(speed, 1f, 10f); // clamp for stability

    // Follow position
    Vector3 targetPos = new Vector3(target.position.x + 3, target.position.y + 4 + currentYOffset, -10f);
    transform.position = Vector3.Lerp(transform.position, targetPos, dynamicFollowSpeed * Time.deltaTime);

    // Zoom out based on speed
    float targetZoom = Mathf.Clamp(minZoom + speed, minZoom, maxZoom);
    cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
}

}
