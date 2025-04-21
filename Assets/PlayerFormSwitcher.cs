using UnityEngine;
using System.Collections;

public class PlayerFormSwitcher : MonoBehaviour
{
    public GameObject walkForm;
    public GameObject skateForm;

    private bool isSkating = false;

    void Start()
    {
        SetForm(false); // Start in walking form
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isSkating = !isSkating;
            SetForm(isSkating);
        }
    }

    void SetForm(bool skate)
    {
        if (skate)
        {
            // Match skateForm position to walkForm before switching
            skateForm.transform.position = walkForm.transform.position;

            walkForm.SetActive(false);
            skateForm.SetActive(true);

            EnableMovement(skateForm, true);
            EnableMovement(walkForm, false);
        }
        else
        {
            // Match walkForm position to skateForm before switching
            walkForm.transform.position = skateForm.transform.position;

            skateForm.SetActive(false);
            walkForm.SetActive(true);

            EnableMovement(walkForm, true);
            EnableMovement(skateForm, false);

            // Start coroutine to remove the skateboard
            StartCoroutine(RemoveSkateboard());
        }
    }

    void EnableMovement(GameObject obj, bool enable)
    {
        var movement = obj.GetComponent<PlayerMovement>();
        if (movement != null)
        {
            movement.enabled = enable;
        }
    }

    IEnumerator RemoveSkateboard()
    {
        // Optional: Play VFX or animation before removal
        yield return new WaitForSeconds(0.5f);

        if (skateForm != null)
        {
            Destroy(skateForm);
        }
    }
}
