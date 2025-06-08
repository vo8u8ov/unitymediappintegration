using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HandRangeSelector で選択された itemName に応じて
/// パネル表示とオブジェクトのエフェクト切り替えを行う汎用ブリッジ
/// </summary>
public class ItemUIBridge : MonoBehaviour
{
    [Tooltip("HandRangeSelector (以前の HandSushiSelector 等)")]
    [SerializeField] private HandRangeSelector selector;

    [Tooltip("RangeItem.itemName に対応するパネルを表示/非表示 するマネージャ")]
    [SerializeField] private UIPanelManager panelManager;

    [Tooltip("アイテム本体オブジェクト (名前を itemName と合わせておく)")]
    [SerializeField] private List<GameObject> itemObjects;

    void Start()
    {
        selector.OnItemNameSelected += HandleSelection;
    }

    private void HandleSelection(string itemName)
    {
        if (!string.IsNullOrEmpty(itemName))
        {
            // パネルを表示
            panelManager.ShowPanel(itemName);

            // オブジェクトのエフェクト切替
            foreach (var obj in itemObjects)
            {
                var controller = obj.GetComponent<ItemEffectController>();
                if (controller != null)
                    controller.SetSelected(obj.name == itemName);
            }
        }
        else
        {
            // 選択解除時はすべてクリア
            panelManager.HideAllPanels();
            foreach (var obj in itemObjects)
            {
                var controller = obj.GetComponent<ItemEffectController>();
                if (controller != null)
                    controller.SetSelected(false);
            }
        }
    }
}
