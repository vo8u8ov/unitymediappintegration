using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseItemUIBridge : MonoBehaviour
{
    [Tooltip("HandRangeSelector (以前の HandSushiSelector 等)")]
    [SerializeField] private HandRangeSelector selector;

    [Tooltip("RangeItem.itemName に対応するパネルを表示/非表示 するマネージャ")]
    [SerializeField] private SenseManager senseManager;

    // Start is called before the first frame update
    void Start()
    {
        selector.OnItemNameSelected += HandleSelection;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    private void HandleSelection(string itemName)
    {
        if (!string.IsNullOrEmpty(itemName))
        {
            senseManager.ShowItem(itemName);
        }
        else
        {
            senseManager.HideAllItems();
        }
    }
}
