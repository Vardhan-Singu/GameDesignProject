using UnityEngine;

public class PlayerFormSwitcher : MonoBehaviour
{
    public GameObject walkForm;
    public GameObject skateForm;
    public CameraFollow cameraFollow;
    public CameraFollow minimapCameraFollow; // New minimap camera follow

    public Animator walkAnimHolderAnimator;
    public Animator skateAnimHolderAnimator;

    public string walkSwitchAnim = "SwitchToWalk";
    public string skateSwitchAnim = "SwitchToSkate";

    private bool isSkating = false;

    void Start()
    {
        SetForm(false);
    }

    void Update()
    {
    if (animator == null)
        return;

    // Only check the component that exists
    if (playerMovement != null && playerMovement.enabled)
    {
        HandleSkateboardingAnimation();
    }
    else if (playerController != null && playerController.enabled)
    {
        HandleWalkingAnimation();
    }
    }

    void SetForm(bool skate)
    {
        if (walkForm == null || skateForm == null || cameraFollow == null || minimapCameraFollow == null) return;

        if (skate && skateAnimHolderAnimator != null)
            skateAnimHolderAnimator.SetTrigger("SwitchAnim");
        else if (!skate && walkAnimHolderAnimator != null)
            walkAnimHolderAnimator.SetTrigger("SwitchAnim");

        if (skate)
        {
            skateForm.transform.position = walkForm.transform.position;
            cameraFollow.SetTarget(skateForm.transform);
            minimapCameraFollow.SetTarget(skateForm.transform);
        }
        else
        {
            Vector3 offset = Vector3.up * 5f;
            walkForm.transform.position = skateForm.transform.position + offset;
            cameraFollow.SetTarget(walkForm.transform);
            minimapCameraFollow.SetTarget(walkForm.transform);
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
        if (skateMovement != null) skateMovement.enabled = enable;

        var walkMovement = obj.GetComponent<PlayerController>();
        if (walkMovement != null) walkMovement.enabled = enable;

        var rb = obj.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = enable;
    }
}
