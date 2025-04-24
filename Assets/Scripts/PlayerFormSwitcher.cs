using UnityEngine;

public class PlayerFormSwitcher : MonoBehaviour
{
    public GameObject walkForm;
    public GameObject skateForm;
    public CameraFollow cameraFollow;

    public Animator walkAnimHolderAnimator;  // Animator for the walking animation
    public Animator skateAnimHolderAnimator; // Animator for the skating animation

    public string walkSwitchAnim = "SwitchToWalk";  // Name of the walk switch animation
    public string skateSwitchAnim = "SwitchToSkate"; // Name of the skate switch animation

    private bool isSkating = false;

    void Start()
    {
        SetForm(false); // Start in walking form
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isSkating = !isSkating;
            SetForm(isSkating);
        }
    }

    void SetForm(bool skate)
    {
        if (walkForm == null || skateForm == null || cameraFollow == null) return;

        // Play the switch animation for the appropriate form
        if (skate && skateAnimHolderAnimator != null)
        {
            skateAnimHolderAnimator.SetTrigger("SwitchAnim");
        }
        else if (!skate && walkAnimHolderAnimator != null)
        {
            walkAnimHolderAnimator.SetTrigger("SwitchAnim");
        }

        // Sync positions
        if (skate)
        {
            skateForm.transform.position = walkForm.transform.position;
            cameraFollow.SetTarget(skateForm.transform); // Switch camera target
        }
        else
        {
            Vector3 offset = Vector3.up * 5f; // slight upward nudge
            walkForm.transform.position = skateForm.transform.position + offset;
            cameraFollow.SetTarget(walkForm.transform); // Switch camera target
        }

        walkForm.SetActive(!skate);
        skateForm.SetActive(skate);

        EnableMovement(walkForm, !skate);
        EnableMovement(skateForm, skate);
    }

    void EnableMovement(GameObject obj, bool enable)
    {
        if (obj == null) return;

        var skateMovement = obj.GetComponent<PlayerMovement>();
        if (skateMovement != null)
            skateMovement.enabled = enable;

        var walkMovement = obj.GetComponent<PlayerController>();
        if (walkMovement != null)
            walkMovement.enabled = enable;

        var rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.simulated = enable;
    }
}