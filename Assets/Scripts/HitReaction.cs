using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReaction : MonoBehaviour
{

    void Start()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            Debug.Log("Hit by hand");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {

        }
    }
}
