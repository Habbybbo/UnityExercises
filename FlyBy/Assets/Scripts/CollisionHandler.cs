
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float timeToDelay = 2f;
    [SerializeField] AudioClip finish;
    [SerializeField] AudioClip crash;

    [SerializeField] ParticleSystem finishParticles;
    [SerializeField] ParticleSystem crashParticles;
    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning == false)
        {
                switch (other.gameObject.tag)
            {
                case "Friendly":
                Debug.Log("You'v bumped into friendly");
                break;
                case "Finish":
                StartSuccessSequence(timeToDelay);
                break;
                default:
                StartCrashSequence(timeToDelay);
                break;
            }
        }
        
    }

    private void StartSuccessSequence(float delayTime)
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.volume = 0.2f;
        audioSource.PlayOneShot(finish);
        GetComponent<Movement>().enabled = false;
        finishParticles.Play();
        Invoke("LoadNextScene", delayTime);
        
        
    }

    void StartCrashSequence(float delayTime)
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.volume = 0.2f;
        audioSource.PlayOneShot(crash);
        GetComponent<Movement>().enabled = false;
        crashParticles.Play();
        Invoke("ReloadScene", delayTime);
        
        
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
