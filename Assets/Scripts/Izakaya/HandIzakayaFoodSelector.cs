using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandIzakayaFoodSelector : MonoBehaviour
{
    [System.Serializable]
    public class MenuRange
    {
        public string menuName;
        public Transform rangeStart;
        public Transform rangeEnd;
        public float MinX => Mathf.Min(rangeStart.position.x, rangeEnd.position.x);
        public float MaxX => Mathf.Max(rangeStart.position.x, rangeEnd.position.x);
    }

    [SerializeField] private List<MenuRange> menuRanges;
    public event Action<string> OnMenuNameSelected;

    private string currentName = "";

    void Start()
    {
        HandEventManager.Instance.OnRightHandChanged += OnRightHandMoved;
        HandEventManager.Instance.OnNoHandsDetected += OnHandsLost;
    }

    void OnRightHandMoved(string key, Vector3 handWorldPos)
    {
        foreach (var range in menuRanges)
        {
            float minX = range.MinX;
            float maxX = range.MaxX;

            if (handWorldPos.x >= minX && handWorldPos.x <= maxX)
            {
                if (range.menuName != currentName)
                {
                    currentName = range.menuName;
                    OnMenuNameSelected?.Invoke(currentName);
                }
                return;
            }
        }
    }

    void OnHandsLost()
    {
        if (!string.IsNullOrEmpty(currentName))
        {
            currentName = "";
            OnMenuNameSelected?.Invoke(null);
        }
    }
}
