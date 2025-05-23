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
        HandEventManager.Instance.OnRightHandChanged += HandleHandMove;
    }

    void OnDisable()
    {
        if (HandEventManager.Instance != null)
            HandEventManager.Instance.OnRightHandChanged -= HandleHandMove;
    }

    private void HandleHandMove(string handKey, Vector3 currentHandPos)
    {
        if (lastRightHandPos != null)
        {
            float deltaX = currentHandPos.x - lastRightHandPos.Value.x;
            float rotationY = deltaX * rotationSensitivity;

            targetObject.Rotate(0, rotationY, 0, Space.World);
        }

        lastRightHandPos = currentHandPos;
    }
}
