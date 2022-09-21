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
        else
        {
            audioSource.Stop();
            mainBooster.Stop();
        }
        
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            AddRotation(rotationThrust);
            if (!leftBooster.isPlaying)
            {
                leftBooster.Play();
            }
            
            
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            AddRotation(-rotationThrust);
            if (!rightBooster.isPlaying)
            {
                rightBooster.Play();
            }
            

        }
        else
        {
            leftBooster.Stop();
            rightBooster.Stop();
        }
        
    }

    private void AddRotation(float rotationPerFrame)
    {
        rb.freezeRotation = true; //freeze the manual rotation
        transform.Rotate(Vector3.forward * rotationPerFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreeze the manual rotation

    }
}
