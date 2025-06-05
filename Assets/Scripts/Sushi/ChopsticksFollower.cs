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

    void Start()
    {
        HandEventManager.Instance.OnRightHandChanged += OnHandMoved;

        // Y/Z を現在のTransformから自動で取得してもOK
        if (fixedY == 0f) fixedY = transform.position.y;
        if (fixedZ == 0f) fixedZ = transform.position.z;
    }

    void OnHandMoved(string key, Vector3 handWorldPos)
    {
        if (!key.StartsWith("right")) return;

        // 箸が動ける範囲だけClampする（手のX座標は制限しない）
        float minX = Mathf.Min(minTransform.position.x, maxTransform.position.x);
        float maxX = Mathf.Max(minTransform.position.x, maxTransform.position.x);
        float clampedX = Mathf.Clamp(handWorldPos.x, minX, maxX);

        Vector3 targetPos = new Vector3(clampedX, fixedY, fixedZ);
        transform.position = targetPos;
    }
}