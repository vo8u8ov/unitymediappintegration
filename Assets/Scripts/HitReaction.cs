using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitReaction : MonoBehaviour
{
    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand") && !hasTriggered)
        {
            hasTriggered = true;

            if (ParticlePool.Instance != null)
            {
                ParticlePool.Instance.PlayEffect(transform.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            hasTriggered = false;
        }
    }
}
