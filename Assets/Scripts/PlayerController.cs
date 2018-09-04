using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")][SerializeField] float controlSpeed = 12f;
    [Tooltip("In m")] [SerializeField] float clampXMax = 10f;
    [Tooltip("In m")] [SerializeField] float clampYMax = 5.5f;
    [SerializeField] GameObject[] guns;
    [Header("Screen position parameters")]
    [SerializeField] float positionPitchFactor = -5.5f;
    [SerializeField] float positionYawFactor = 5.5f;
    [Header("Control throw parameters")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -30f;

    float xThrow, yThrow;
    bool isControlsEnabled = true;

    void Update ()
    {
        if (isControlsEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float xOffset = controlSpeed * xThrow * Time.deltaTime;
        float yOffset = controlSpeed * yThrow * Time.deltaTime;
        float clampedXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -clampXMax, clampXMax);
        float clampedYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -clampYMax, clampYMax);
        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void OnPlayerDeath()
    {
        isControlsEnabled = false;
    }

    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in guns)
        {
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
