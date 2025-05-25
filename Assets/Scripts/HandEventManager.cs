using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class HandEventManager : MonoBehaviour
{
    public static HandEventManager Instance { get; private set; }
    public event Action<string, Vector3> OnRightHandChanged;
    public event Action<string, Vector3> OnLeftHandChanged;
    public event Action OnNoHandsDetected;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void NotifyRightHandPos(string handkey, Vector3 pos)
    {   
        OnRightHandChanged?.Invoke(handkey, pos);
    }

    public void NotifyLeftHandPos(string handkey, Vector3 pos)
    {
        OnLeftHandChanged?.Invoke(handkey, pos);
    }
    
    public void NotifyHandsInactive()
    {
        OnNoHandsDetected?.Invoke();
    }
}
