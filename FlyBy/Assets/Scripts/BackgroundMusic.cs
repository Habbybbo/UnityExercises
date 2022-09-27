using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip backgroundMusic;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(backgroundMusic);
        }
    }
}
