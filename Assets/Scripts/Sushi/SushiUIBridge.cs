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
                TriggerEffect(name);
            }
            else
            {
                panelManager.HideAllPanels();
            }
        };
    }
    
    void TriggerEffect(string sushiName)
    {
        var sushi = sushiObjects.Find(obj => obj.name == sushiName);
        if (sushi != null)
        {
            var effect = sushi.GetComponent<SushiEffectController>();
            if (effect != null)
            {
                effect.PlayEffect();
            }
        }
    }
}
