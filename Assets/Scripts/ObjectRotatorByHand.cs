using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotatorByHand : MonoBehaviour
{
    public Transform targetObject;
    public float rotationSensitivity = 300f;

    private Vector3? lastRightHandPos = null;

    void OnEnable()
    {
        HandEventManager.Instance.OnHandChanged += HandleHandMove;
    }

    void OnDisable()
    {
        if (HandEventManager.Instance != null)
            HandEventManager.Instance.OnHandChanged -= HandleHandMove;
    }

    private void HandleHandMove(string handKey, Vector3 currentHandPos)
    {
        // "right" という文字列を含むハンドキーのみ受け付ける（例："right_0"）
        if (!handKey.ToLower().Contains("right")) return;

        if (lastRightHandPos != null)
        {
            float deltaX = currentHandPos.x - lastRightHandPos.Value.x;
            float rotationY = deltaX * rotationSensitivity;

            targetObject.Rotate(0, rotationY, 0, Space.World);
        }

        lastRightHandPos = currentHandPos;
    }
}
