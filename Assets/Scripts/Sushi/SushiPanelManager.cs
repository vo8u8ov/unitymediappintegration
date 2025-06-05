using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class SushiInfoPanel
{
    public string name;
    public GameObject sushiInfoPanel; // Optional, if you want to instantiate a sushi object
}

public class SushiPanelManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelPrefab;
    public Transform panelParent;

    [Header("Sushi Data")]
    public List<SushiInfoPanel> sushiPanelList;

    private Dictionary<string, GameObject> sushiPanels = new Dictionary<string, GameObject>();
    private string currentVisibleName = "";

    void Start()
    {
        foreach (var panel in sushiPanelList)
        {
            panel.sushiInfoPanel.SetActive(false);

            sushiPanels[panel.name] = panel.sushiInfoPanel;
        }
    }

    public void ShowPanel(string sushiName)
    {
        if (currentVisibleName == sushiName) return;

        foreach (var kvp in sushiPanels)
        {
            kvp.Value.SetActive(kvp.Key == sushiName);
        }

        currentVisibleName = sushiName;
    }

    public void HideAllPanels()
    {
        foreach (var panel in sushiPanels.Values)
        {
            panel.SetActive(false);
        }
        currentVisibleName = "";
    }
}
