using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInfoPanel
{
    [Tooltip("例：\"Sushi\"、\"CreamPuff\" など RangeItem.itemName と合わせる")]
    public string itemName;
    [Tooltip("Inspector で割り当てるパネル GameObject")]
    public GameObject infoPanel;
}

public class UIPanelManager : MonoBehaviour
{
    [Header("登録するアイテムごとのパネル")]
    [SerializeField] private List<ItemInfoPanel> itemPanelList;

    // itemName → パネル本体 のマップ
    private Dictionary<string, GameObject> panels = new Dictionary<string, GameObject>();

    // 現在表示中の itemName
    private string currentVisibleName = "";

    void Awake()
    {
        // 起動時にすべて非表示＆辞書へ登録
        foreach (var entry in itemPanelList)
        {
            if (entry.infoPanel == null) continue;
            entry.infoPanel.SetActive(false);
            panels[entry.itemName] = entry.infoPanel;
        }
    }

    /// <summary>
    /// itemName に対応するパネルだけを表示
    /// </summary>
    public void ShowPanel(string itemName)
    {
        if (currentVisibleName == itemName) return;

        foreach (var kv in panels)
        {
            kv.Value.SetActive(kv.Key == itemName);
        }

        currentVisibleName = itemName;
    }

    /// <summary>
    /// すべてのパネルを非表示
    /// </summary>
    public void HideAllPanels()
    {
        foreach (var panel in panels.Values)
            panel.SetActive(false);

        currentVisibleName = "";
    }
}
