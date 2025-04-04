using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform target;
    public float fallOffset = 5f; // Extra downward offset when falling
    public float smoothFallSpeed = 2f; // Speed for smoothing the fall offset
    private Rigidbody2D rb;
    private float currentYOffset = 0f;

    void Start()
    {
        rb = target.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float targetYOffset = rb.linearVelocity.y < 0 ? -fallOffset : 0f;
        currentYOffset = Mathf.Lerp(currentYOffset, targetYOffset, smoothFallSpeed * Time.deltaTime);
        
        Vector3 newPos = new Vector3(target.position.x + 5, target.position.y + 10 + currentYOffset, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, FollowSpeed * Time.deltaTime);
    }
}
