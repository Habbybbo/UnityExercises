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
    [SerializeField] AudioClip sideThrusters;

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
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            
        }
        else
        {
            audioSource.Stop();
        }
        
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            AddRotation(rotationThrust);

            
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            AddRotation(-rotationThrust);

            
        }
        
    }

    private void AddRotation(float rotationPerFrame)
    {
        rb.freezeRotation = true; //freeze the manual rotation
        transform.Rotate(Vector3.forward * rotationPerFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreeze the manual rotation

    }
}
