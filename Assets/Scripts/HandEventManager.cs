using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)] 
public class HandEventManager : MonoBehaviour
{
    public static HandEventManager Instance { get; private set; }
    public event Action<string, Vector3> OnHandChanged;

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

    public void NotifyHandPos(string handkey, Vector3 pos)
    {
        OnHandChanged?.Invoke(handkey, pos);
    }
}
