using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiEffectController : MonoBehaviour
{
    private ParticleSystem magicParticle;

    void Start()
    {
        magicParticle = GetComponentInChildren<ParticleSystem>(true);

        if (magicParticle == null)
        {
            Debug.LogWarning($"{name} にパーティクルが見つかりませんでした！");
        }
    }

    public void PlayEffect()
    {
        if (magicParticle == null) return;

        if (magicParticle.isPlaying)
        {
            magicParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        magicParticle.Play();
    }
}
