using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float timeTillDestruction = 3f;

    void Start()
    {
        Destroy(this.gameObject, timeTillDestruction);
    }
}
