using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("Input keys binding")]
    [SerializeField] InputAction movement;


    [Tooltip("How fast the ship moves up and down based upon player input")]
    [SerializeField] float thrustMultiplier = 30f;
    [Tooltip("How fast player moves horizontally")]
    [SerializeField] float xRange = 23f;
    [Tooltip("How fast player moves vertically")]
    [SerializeField] float yRange = 15f;


    [Tooltip("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float positionYawFactor = 1.5f;

    [Tooltip("Player input base tuning")]
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float controlRollFactor = -30f;


    [Tooltip("Firing input key binding")]
    [SerializeField] InputAction fire;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers here")]
    [SerializeField] GameObject[] lasers;

    float xThrust;
    float yThrust;


    void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }

    void Update()
    {
        ProccessTranslation();
        ProccessRottation();
        ProccessFiring();
    }

    void ProccessFiring()
    {
        if (fire.ReadValue<float>() > 0f)
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    private void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emisionModule = laser.GetComponent<ParticleSystem>().emission;
            emisionModule.enabled = isActive;
        }
    }

    void ProccessRottation()
    {
        
        
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrust * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrust * controlRollFactor;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProccessTranslation()
    {
        xThrust = movement.ReadValue<Vector2>().x;
        yThrust = movement.ReadValue<Vector2>().y;

        float xOffset = xThrust * Time.deltaTime * thrustMultiplier;
        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        float vOffset = yThrust * Time.deltaTime * thrustMultiplier;
        float rawVPos = transform.localPosition.y + vOffset;
        float clampedYPos = Mathf.Clamp(rawVPos, -yRange, yRange);

        transform.localPosition = new Vector3(
        clampedXPos,
        clampedYPos,
        transform.localPosition.z);
    }
}
