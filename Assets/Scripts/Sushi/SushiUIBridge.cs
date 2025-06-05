using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiUIBridge : MonoBehaviour
{
    [SerializeField] private HandSushiSelector selector;
    [SerializeField] private SushiPanelManager panelManager;

    void Start()
    {
        selector.OnSushiNameSelected += name =>
        {
            if (!string.IsNullOrEmpty(name))
                panelManager.ShowPanel(name);
            else
                panelManager.HideAllPanels();
        };
    }
}
