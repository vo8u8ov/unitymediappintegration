using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagashiInteraction : MonoBehaviour
{
    private Vector3 originalScale;
    private float scaleTime = 0f;
    private float duration = 1f; // ← 長めにする（0.5〜1.0fくらいが自然）
    private bool scaling = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (scaling)
        {
            scaleTime += Time.deltaTime;
            float t = scaleTime / duration;
            float scale = Mathf.Lerp(2f, 1f, t);
            transform.localScale = originalScale * scale;

            if (t >= 1f)
            {
                scaling = false;
                transform.localScale = originalScale;
            }
        }
    }
    public void TriggerPulse()
    {
        scaleTime = 0f;
        scaling = true;
    }
}
