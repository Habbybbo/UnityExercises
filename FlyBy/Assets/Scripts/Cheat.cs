using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cheat : MonoBehaviour
{
    BoxCollider boxColider;
    CollisionHandler collisionHandler;
    bool collisionDisabled = false;

    void Start()
    {
        boxColider = GetComponent<BoxCollider>();
        collisionHandler = GetComponent<CollisionHandler>();
        
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextScene();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
            
        }
        
        if (collisionDisabled == true)
        {
            boxColider.enabled = false;
        }
        else
        {
            boxColider.enabled = true;
        }
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

}
