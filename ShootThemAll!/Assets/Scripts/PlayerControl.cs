using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] InputAction movement;

    [SerializeField] float thrustMultiplier = 30f;
    [SerializeField] float xRange = 23f;
    [SerializeField] float yRange = 15f;
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] float controlPitchFactor = -10f;
    [SerializeField] float positionYawFactor = 1.5f;
    [SerializeField] float controlRollFactor = -30f;

    float xThrust;
    float yThrust;


    void Start()
    {
        
    }

    void OnEnable()
    {
        movement.Enable();
    }

    void OnDisable()
    {
        movement.Disable();
    }

    void Update()
    {
        ProccessTranslation();
        ProccessRottation();
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
