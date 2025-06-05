using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiEffectController : MonoBehaviour
{
    private ParticleSystem magicParticle;
    private bool isSelected = false;
    private float playInterval = 3.0f; // パーティクルの寿命と同じくらい
    private float timer = 0f;

    void Start()
    {
        magicParticle = GetComponentInChildren<ParticleSystem>(true);

        if (magicParticle == null)
        {
            Debug.LogWarning($"{name} にパーティクルが見つかりませんでした！");
        }
    }

    void Update()
    {
        if (isSelected && magicParticle != null)
        {
            timer += Time.deltaTime;

            if (timer >= playInterval)
            {
                magicParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                magicParticle.Play();
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
        else if (magicParticle != null)
        {
            magicParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    public void PlayEffect()
    {
        // 明示的な1回再生（外部呼び出し用）
        if (magicParticle != null)
        {
            magicParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            magicParticle.Play();
        }
    }     
}
