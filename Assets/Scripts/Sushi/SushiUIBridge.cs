using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SushiUIBridge : MonoBehaviour
{
    [SerializeField] private HandSushiSelector selector;
    [SerializeField] private SushiPanelManager panelManager;
    [SerializeField] private List<GameObject> sushiObjects; // 寿司本体を登録しておく

    void Start()
    {
        selector.OnSushiNameSelected += name =>
        {
            if (!string.IsNullOrEmpty(name))
            {
                panelManager.ShowPanel(name);
                foreach (var sushi in sushiObjects)
                {
                    var controller = sushi.GetComponent<SushiEffectController>();
                    if (controller != null)
                    {
                        controller.SetSelected(sushi.name == name);
                    }
                }
            }
            else
            {
                panelManager.HideAllPanels();
            }
        };
    }
}
