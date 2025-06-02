using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerInteraction : MonoBehaviour
{
    private Vector3 originalScale;
    private float scaleTime = 0f;
    private float duration = 1f; // ← 長めにする（0.5〜1.0fくらいが自然）
    private bool scaling = false;

    private Color originalColor;
    private Color pulseColor = new Color(1.0f, 0.0f, 0.7f); // RGB(255, 0, 180)// 発光時の色
    private Renderer rend;
    private MaterialPropertyBlock block;

    void Start()
    {
        originalScale = transform.localScale;

        rend = GetComponent<Renderer>();
        block = new MaterialPropertyBlock();
        rend.GetPropertyBlock(block);
        originalColor = block.GetColor("_Color"); // ← Shader Graphで_Colorを使っている前提
       
    }

    void Update()
    {
        if (scaling)
        {
            scaleTime += Time.deltaTime;
            float t = scaleTime / duration;
            float scale = Mathf.Lerp(2f, 1f, t);
            transform.localScale = originalScale * scale;

            Color currentColor = Color.Lerp(pulseColor, originalColor, t);
            block.SetColor("_Color", currentColor);
            rend.SetPropertyBlock(block);

            if (t >= 1f)
            {
                scaling = false;
                transform.localScale = originalScale;

                block.SetColor("_Color", originalColor);
                rend.SetPropertyBlock(block);
            }
        }
    }

    public void TriggerPulse()
    {
        scaleTime = 0f;
        scaling = true;
    }
}
