using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

[System.Serializable]
public class MenuInfoPanel
{
    public string name;
    public GameObject menuInfoPanel; // Optional, if you want to instantiate a sushi object
}

public class IzakayaPanelManager : MonoBehaviour
{

    [Header("Menu Data")]
    public List<MenuInfoPanel> menuPanelList;

    private Dictionary<string, GameObject> menuPanels = new Dictionary<string, GameObject>();
    private string currentVisibleName = "";

    void Start()
    {
        foreach (var panel in menuPanelList)
        {
            panel.menuInfoPanel.SetActive(false);

            menuPanels[panel.name] = panel.menuInfoPanel;
        }
    }

    public void ShowPanel(string sushiName)
    {
        if (currentVisibleName == sushiName) return;

        foreach (var kvp in menuPanels)
        {
            kvp.Value.SetActive(kvp.Key == sushiName);
        }

        currentVisibleName = sushiName;
    }

    public void HideAllPanels()
    {
        foreach (var panel in menuPanels.Values)
        {
            panel.SetActive(false);
        }
        currentVisibleName = "";
    }
}
