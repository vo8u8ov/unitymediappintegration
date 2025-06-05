using System.Collections.Generic;
using UnityEngine;
using System;

public class HandSushiSelector : MonoBehaviour
{
    [System.Serializable]
    public class SushiRange
    {
        public string sushiName;
        public Transform rangeStart;
        public Transform rangeEnd;
        public float MinX => Mathf.Min(rangeStart.position.x, rangeEnd.position.x);
        public float MaxX => Mathf.Max(rangeStart.position.x, rangeEnd.position.x);
    }

    [SerializeField] private List<SushiRange> sushiRanges;
    public event Action<string> OnSushiNameSelected;

    private string currentName = "";

    void Start()
    {
        HandEventManager.Instance.OnRightHandChanged += OnRightHandMoved;
    }

    void OnRightHandMoved(string key, Vector3 handWorldPos)
    {
        foreach (var range in sushiRanges)
        {
            float minX = range.MinX;
            float maxX = range.MaxX;

            if (handWorldPos.x >= minX && handWorldPos.x <= maxX)
            {
                if (range.sushiName != currentName)
                {
                    currentName = range.sushiName;
                    Debug.Log($"Sushi selected: {currentName}");
                    OnSushiNameSelected?.Invoke(currentName);
                }
                return;
            }
        }

        if (!string.IsNullOrEmpty(currentName))
        {
            currentName = "";
            OnSushiNameSelected?.Invoke(null);
        }
    }
}