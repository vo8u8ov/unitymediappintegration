using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandColliderController : MonoBehaviour
{
    public float forceStrength = 100f;

    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        Debug.Log("Hit: " + other.name);
        if (other.name == "LogoObj") return;

        if (other.tag == "Container") return;

        if (rb.isKinematic)
        {
            rb.isKinematic = false; // 動くようにする
        }

        if (rb != null && !rb.isKinematic)
        {
            
            // 手の方向から外側に押す力を加える
            Vector3 dir = (other.transform.position - transform.position).normalized;
            dir.z = 0f;
            dir = dir.normalized;
            rb.AddForce(dir * forceStrength, ForceMode.Impulse);
        }
    }
}
