using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IzakayaUIBridge : MonoBehaviour
{
    [SerializeField] private HandIzakayaFoodSelector selector;
    [SerializeField] private IzakayaPanelManager panelManager;
    [SerializeField] private List<GameObject> izakayaMenuObjects;

    void Start()
    {
        selector.OnMenuNameSelected += name =>
        {
            if (!string.IsNullOrEmpty(name))
            {
                panelManager.ShowPanel(name);
                foreach (var menu in izakayaMenuObjects)
                {
                    var controller = menu.GetComponent<IzakayaMenuEffectController>();
                    if (controller != null)
                    {
                        controller.SetSelected(menu.name == name);
                    }
                }
            }
            else
            {
                panelManager.HideAllPanels();
                foreach (var menu in izakayaMenuObjects)
                {
                    var controller = menu.GetComponent<IzakayaMenuEffectController>();
                    if (controller != null)
                    {
                        controller.SetSelected(false); // ✅ ここで止める
                    }
                }
            }
        };
    }
}
