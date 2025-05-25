using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXController : MonoBehaviour
{
    public VisualEffect vfx; // VFX Graphをアタッチする
    public Vector2 vector2Value = new Vector2(1.0f, 1.0f); // Exposed名に合わせる
    // スケーリング係数（速度を調整）
    public float sensitivity = 10f;
    private Vector3? lastRightHandPos = null;
    private Vector2 defaultVector2Value = Vector2.one;
    private bool isReset = false;

    void Start()
    {
        defaultVector2Value = vector2Value; // 初期値を保存
        Debug.Log("VFXController started with default vector2Value: " + defaultVector2Value);
        HandEventManager.Instance.OnRightHandChanged += HandleHandMove;
        HandEventManager.Instance.OnNoHandsDetected += Reset;
    }

    private void HandleHandMove(string handKey, Vector3 currentHandPos)
    {
        Debug.Log($"HandleHandMove called with handKey: {handKey}, currentHandPos: {currentHandPos}");
        // 柔軟な条件で右手だけを対象に
        if (!handKey.ToLower().Contains("right")) return;

        if (lastRightHandPos.HasValue)
        {
            Vector3 delta = currentHandPos - lastRightHandPos.Value;
            vector2Value = new Vector2(-delta.x * sensitivity, delta.y * sensitivity);
            Debug.Log($"[DELTA] Velocity set to {vector2Value}");
        }
        vfx.SetVector2("VelocityXY", defaultVector2Value + vector2Value);
        lastRightHandPos = currentHandPos;
    }
    
    private void Reset()
    {
        Debug.Log("Resetting VFXController state.");
        lastRightHandPos = null;
        vector2Value = defaultVector2Value; // 初期化
        vfx.SetVector2("VelocityXY", vector2Value); // VFX Graph 側の Exposed プロパティも初期化
    }
}
