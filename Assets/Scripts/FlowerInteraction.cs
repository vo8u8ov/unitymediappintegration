using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerInteraction : MonoBehaviour
{
    private Vector3 originalScale;
    private float scaleTime = 0f;
    private float duration = 0.2f;
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
            float scale = Mathf.Lerp(1.6f, 1f, t); // 一瞬大きく → 戻る

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
