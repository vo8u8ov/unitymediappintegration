using System;
using System.Collections.Generic;
using UnityEngine;

public class HandRangeSelector : MonoBehaviour
{
    [Serializable]
    public class RangeItem
    {
        [Tooltip("例：\"Sushi\", \"CreamPuff\" など")]
        public string itemName;
        public Transform rangeStart;
        public Transform rangeEnd;

        public float MinX => Mathf.Min(rangeStart.position.x, rangeEnd.position.x);
        public float MaxX => Mathf.Max(rangeStart.position.x, rangeEnd.position.x);
    }

    [SerializeField] private List<RangeItem> rangeItems;
    /// <summary>選択中のアイテム名を通知 (範囲外は null)</summary>
    public event Action<string> OnItemNameSelected;

    private string currentName = "";

    void Start()
    {
        HandEventManager.Instance.OnRightHandChanged += OnRightHandMoved;
        HandEventManager.Instance.OnNoHandsDetected += OnHandsLost;
    }

    private void OnRightHandMoved(string key, Vector3 handWorldPos)
    {
        foreach (var item in rangeItems)
        {
            if (handWorldPos.x >= item.MinX && handWorldPos.x <= item.MaxX)
            {
                if (item.itemName != currentName)
                {
                    currentName = item.itemName;
                    OnItemNameSelected?.Invoke(currentName);
                }
                return;
            }
        }
        // どの範囲にも入っていなければクリア
        ClearSelection();
    }

    private void OnHandsLost()
    {
        ClearSelection();
    }

    private void ClearSelection()
    {
        if (!string.IsNullOrEmpty(currentName))
        {
            currentName = "";
            OnItemNameSelected?.Invoke(null);
        }
    }
}
