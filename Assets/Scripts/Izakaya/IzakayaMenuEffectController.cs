using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IzakayaMenuEffectController : MonoBehaviour
{
    private ParticleSystem washiParticle;
    private bool isSelected = false;
    private float playInterval = 3.0f; // パーティクルの寿命と同じくらい
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        washiParticle = GetComponentInChildren<ParticleSystem>(true);

        if (washiParticle == null)
        {
            Debug.LogWarning($"{name} にパーティクルが見つかりませんでした！");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected && washiParticle != null)
        {
            timer += Time.deltaTime;

            if (timer >= playInterval)
            {
                washiParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                washiParticle.Play();
                timer = 0f;
            }
        }
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;

        if (selected)
        {
            timer = playInterval; // すぐ再生
        }
        else if (washiParticle != null)
        {
            washiParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}
