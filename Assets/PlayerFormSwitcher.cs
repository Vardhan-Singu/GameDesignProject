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
            if (walkForm != null && skateForm != null)
                skateForm.transform.position = walkForm.transform.position;

            walkForm.SetActive(false);
            skateForm.SetActive(true);

            EnableMovement(skateForm, true);
            EnableMovement(walkForm, false);
        }
        else
        {
            if (skateForm != null)
                walkForm.transform.position = skateForm.transform.position;

            if (skateForm != null) skateForm.SetActive(false);
            walkForm.SetActive(true);

            EnableMovement(walkForm, true);
            if (skateForm != null) EnableMovement(skateForm, false);

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
        yield return new WaitForSeconds(0.5f);

        if (skateForm != null)
        {
            Destroy(skateForm);
            skateForm = null;
        }
    }
}