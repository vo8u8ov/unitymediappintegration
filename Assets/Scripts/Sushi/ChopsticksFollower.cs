using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopsticksFollower : MonoBehaviour
{

    [Header("手の範囲を制限するTransform")]
    public Transform minTransform;  // 左端（最小X）
    public Transform maxTransform;  // 右端（最大X）

    [Header("固定座標")]
    public float fixedY = 0.5f;
    public float fixedZ = 0f;
    public bool handActive = true;

    private Vector3 lastHandPos = Vector3.positiveInfinity;
    
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (meshRenderer != null) meshRenderer.enabled = false;

        HandEventManager.Instance.OnRightHandChanged += OnHandMoved;
        HandEventManager.Instance.OnNoHandsDetected += OnNoHands;

        // Y/Z を現在のTransformから自動で取得してもOK
        if (fixedY == 0f) fixedY = transform.position.y;
        if (fixedZ == 0f) fixedZ = transform.position.z;

    }

   void OnHandMoved(string key, Vector3 handWorldPos)
    {
        if (!key.StartsWith("right")) return;
        if (handWorldPos == lastHandPos) return;
        lastHandPos = handWorldPos;

        // X の範囲を取得
        float minX = Mathf.Min(minTransform.position.x, maxTransform.position.x);
        float maxX = Mathf.Max(minTransform.position.x, maxTransform.position.x);

        // 範囲外なら非表示にして終了
        if (handWorldPos.x < minX || handWorldPos.x > maxX)
        {
            handActive = false;
            if (meshRenderer != null) meshRenderer.enabled = false;
            return;
        }

        // 範囲内なら表示＆移動
        handActive = true;
        if (meshRenderer != null) meshRenderer.enabled = true;

        float clampedX = Mathf.Clamp(handWorldPos.x, minX, maxX);
        Vector3 targetPos = new Vector3(clampedX, fixedY, fixedZ);
        transform.position = targetPos;
    }

    void OnNoHands()
    {   
        handActive = false;
        if (meshRenderer != null) meshRenderer.enabled = false;
    }
}