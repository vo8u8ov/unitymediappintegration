using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 汎用アイテム用のエフェクトコントローラ
/// ItemUIBridge などから SetSelected() を呼んで選択状態を切り替えられます
/// </summary>
public class ItemEffectController : MonoBehaviour
{
    // 子オブジェクトにアタッチされた ParticleSystem（Inspector で設定してもOK）
    private ParticleSystem effectParticle;
    private bool isSelected = false;

    // 再生間隔（デフォルト 3秒）
    [SerializeField] private float playInterval = 3.0f;
    private float timer = 0f;

    void Start()
    {
        // 非アクティブ子オブジェクトも探せるよう true を渡す
        effectParticle = GetComponentInChildren<ParticleSystem>(true);
        if (effectParticle == null)
            Debug.LogWarning($"[{name}] 子に ParticleSystem が見つかりませんでした");
    }

    void Update()
    {
        if (!isSelected || effectParticle == null) return;

        timer += Time.deltaTime;
        if (timer >= playInterval)
        {
            // いったん停止してリスタート
            effectParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            effectParticle.Play();
            timer = 0f;
        }
    }

    /// <summary>
    /// 選択状態の切り替え
    /// </summary>
    public void SetSelected(bool selected)
    {
        isSelected = selected;

        if (selected)
        {
            // すぐにエフェクトを再生
            timer = playInterval;
        }
        else if (effectParticle != null)
        {
            // 非選択時は止める
            effectParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    /// <summary>
    /// 外部から明示的に一度だけ再生したいときに呼ぶ
    /// </summary>
    public void PlayEffect()
    {
        if (effectParticle == null) return;
        effectParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        effectParticle.Play();
    }
}
