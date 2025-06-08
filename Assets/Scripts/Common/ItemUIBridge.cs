using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemUIBridge : MonoBehaviour
{
    [Tooltip("HandRangeSelector (以前の HandSushiSelector 等)")]
    [SerializeField] private HandRangeSelector selector;

    [Tooltip("RangeItem.itemName に対応するパネルを表示/非表示 するマネージャ")]
    [SerializeField] private UIPanelManager panelManager;

    [Tooltip("アイテム本体オブジェクト (名前を itemName と合わせておく)")]
    [SerializeField] private List<GameObject> itemObjects;
    protected virtual bool ShouldShowPanel => true;
    protected virtual bool ShouldShowEffect => true;
    protected virtual bool ShouldChangeMaterial => true;

    void Start()
    {
        selector.OnItemNameSelected += HandleSelection;
    }

    private void HandleSelection(string itemName)
    {
        if (!string.IsNullOrEmpty(itemName))
        {
            if (ShouldShowPanel)
                panelManager.ShowPanel(itemName);

            if (ShouldShowEffect)
                UpdateEffects(itemName);

            if (ShouldChangeMaterial)
                UpdateMaterial(itemName);
        }
        else
        {
            if (ShouldShowPanel)
                panelManager.HideAllPanels();

            if (ShouldShowEffect)
                ClearEffects();

            if (ShouldChangeMaterial)
                RevertMaterial();
        }
    }

    private void UpdateEffects(string itemName)
    {
        foreach (var obj in itemObjects)
        {
            var controller = obj.GetComponent<ItemEffectController>();
            if (controller != null)
                controller.SetSelected(obj.name == itemName);
        }
    }

    private void ClearEffects()
    {
        foreach (var obj in itemObjects)
        {
            var controller = obj.GetComponent<ItemEffectController>();
            if (controller != null)
                controller.SetSelected(false);
        }
    }

    private void UpdateMaterial(string itemName)
    {
        foreach (var obj in itemObjects)
        {
            var controller = obj.GetComponent<ItemMaterialController>();
            if (controller != null)
                controller.SetSelected(obj.name == itemName);
        }
    }

    private void RevertMaterial()
    {
        foreach (var obj in itemObjects)
        {
            var controller = obj.GetComponent<ItemMaterialController>();
            if (controller != null)
                controller.SetSelected(false);
        }
    }
}
