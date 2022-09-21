using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideThrustersSound : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip sideThrusters;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        RotationSound();
    }

    void RotationSound()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
           if (!audioSource.isPlaying)
           {
                audioSource.PlayOneShot(sideThrusters);
           }
            
            
            
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(sideThrusters);
            }
            
            

        }
        else
        {
            audioSource.Stop();
        }
        
    }
}
