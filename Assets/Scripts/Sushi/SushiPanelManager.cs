using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class SushiInfo {
    public string name;
    public string description;
}

public class SushiPanelManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelPrefab;           // SushiPanelのプレハブ
    public Transform panelParent;            // 各パネルを配置する親Canvas内のTransform

    [Header("Sushi Data")]
    public List<SushiInfo> sushiList;        // 寿司の名前と説明リスト

    void Start()
    {
        foreach (var sushi in sushiList)
        {
            CreateSushiPanel(sushi);
        }
    }

    void CreateSushiPanel(SushiInfo sushi)
    {
        GameObject panel = Instantiate(panelPrefab, panelParent);

        // 子オブジェクトのTextMeshProUGUIを探す
        var texts = panel.GetComponentsInChildren<TextMeshProUGUI>();
        foreach (var text in texts)
        {
            if (text.name.Contains("Title"))
                text.text = sushi.name;
            else if (text.name.Contains("Description"))
                text.text = sushi.description;
        }
        panel.SetActive(false);
    }
}
