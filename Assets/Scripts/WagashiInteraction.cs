using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagashiInteraction : MonoBehaviour
{
    public float maxScaleFactor = 1.5f;
    private Vector3 originalScale;
    private float scaleTime = 0f;
    private float duration = 1f; // ← 拡大から元に戻るまでの時間（例：1秒）
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
            float scale = Mathf.Lerp(maxScaleFactor, 1f, t);
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
        // すでに拡大中なら何もしない
        if (scaling) return;

        scaleTime = 0f;
        scaling = true;
    }
}
