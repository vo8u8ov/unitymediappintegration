using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandColliderController : MonoBehaviour
{
    public float forceStrength = 100f;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.attachedRigidbody;
        if (rb == null || rb.isKinematic) return;

        if (other.name == "LogoObj" || other.CompareTag("Container")) return;

        Vector3 dir = (other.transform.position - transform.position).normalized;
        dir.z = 0f;

        rb.AddForce(dir * forceStrength * Time.deltaTime, ForceMode.Impulse);
    }
}
