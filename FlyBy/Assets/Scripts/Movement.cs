using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField] float mainThrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBooster;
    [SerializeField] ParticleSystem rightBooster;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }


    void ProcessThrust()
    {
        
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrusting();

        }
        else
        {
            StopThrusting();
        }

    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation();

        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            ApplyNegativeRotation();

        }
        else
        {
            StopRotation();
        }

    }

    private void ApplyThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);

        }
        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    private void StopThrusting()
    {
        audioSource.Stop();
        mainBooster.Stop();
    }


private void ApplyRotation()
    {
        AddRotation(rotationThrust);
        if (!leftBooster.isPlaying)
        {
            leftBooster.Play();
        }
    }

private void ApplyNegativeRotation()
    {
        AddRotation(-rotationThrust);
        if (!rightBooster.isPlaying)
        {
            rightBooster.Play();
        }
    }

    private void StopRotation()
    {
        leftBooster.Stop();
        rightBooster.Stop();
    }

    private void AddRotation(float rotationPerFrame)
    {
        rb.freezeRotation = true; //freeze the manual rotation
        transform.Rotate(Vector3.forward * rotationPerFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreeze the manual rotation

    }
}
