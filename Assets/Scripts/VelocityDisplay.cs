using UnityEngine;
using TMPro;
using UnityEngine.UI; 


public class VelocityDisplay : MonoBehaviour
{
    public Rigidbody2D rb; // Assign your Rigidbody2D (e.g. Player)
    public TextMeshProUGUI velocityText;
    public Slider velocitySlider;
    public Image fillImage;        // Reference to the Fill image
    public float maxSpeed = 150f; // Set this based on your gameplay

    public Color slowColor = Color.green;
    public Color fastColor = Color.red;

    void Update()
    {
        if (rb != null && velocityText != null)
        {
            Vector2 velocity = rb.linearVelocity;
            velocityText.text = $"Velocity: ({velocity.x:F2}, {velocity.y:F2})";
        }

        if (rb != null && velocitySlider != null)
        {
            float speed = rb.linearVelocity.magnitude; // Speed = length of velocity vector
            velocitySlider.value = Mathf.Clamp(speed, 0, maxSpeed);
        }
        if (rb != null && velocitySlider != null && fillImage != null)
        {
            float speed = rb.linearVelocity.magnitude;
            float normalizedSpeed = Mathf.Clamp01(speed / maxSpeed);

            // Update the slider
            velocitySlider.value = speed;

            // Interpolate color from slowColor to fastColor
            fillImage.color = Color.Lerp(slowColor, fastColor, normalizedSpeed);
            velocitySlider.value = Mathf.Lerp(velocitySlider.value, speed, Time.deltaTime * 10f);

        }
    }


}