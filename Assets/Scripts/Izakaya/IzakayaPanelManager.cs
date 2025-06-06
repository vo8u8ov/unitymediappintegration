using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IzakayaPanelManager : MonoBehaviour
{
    [System.Serializable]
    public class MenuInfoPanel
    {
        public string name;
        public GameObject menuInfoPanel; // Optional, if you want to instantiate a sushi object
        [HideInInspector] public Vector2 originalPosition;
        [HideInInspector] public bool isAnimating;
    }

    [Header("Menu Data")]
    public List<MenuInfoPanel> menuPanelList;

    private Dictionary<string, MenuInfoPanel> menuPanels = new Dictionary<string, MenuInfoPanel>();
    private string currentVisibleName = "";

    [Header("Animation Settings")]
    public float slideDistance = 300f; // どのくらい上から落とすか
    public float duration = 0.4f;

    private Dictionary<string, Coroutine> runningCoroutines = new Dictionary<string, Coroutine>();



    void Start()
    {
        foreach (var panel in menuPanelList)
        {
            var rt = panel.menuInfoPanel.GetComponent<RectTransform>();
            panel.originalPosition = rt.anchoredPosition;
            panel.menuInfoPanel.SetActive(false);

            menuPanels[panel.name] = panel;
        }
    }
    

    public void ShowPanel(string sushiName)
{
    if (currentVisibleName == sushiName) return;

    // 現在のをSlideOut（強制停止付き）
    if (!string.IsNullOrEmpty(currentVisibleName) && menuPanels.ContainsKey(currentVisibleName))
    {
        StopIfRunning(currentVisibleName);
        var panel = menuPanels[currentVisibleName];
        runningCoroutines[currentVisibleName] = StartCoroutine(SlideOut(panel));
    }

    // 新しいのをSlideIn
    if (menuPanels.ContainsKey(sushiName))
    {
        StopIfRunning(sushiName);
        var panel = menuPanels[sushiName];
        runningCoroutines[sushiName] = StartCoroutine(SlideIn(panel));
        currentVisibleName = sushiName;
    }
}

public void HideAllPanels()
{
    foreach (var panel in menuPanels.Values)
    {
        StopIfRunning(panel.name);
        runningCoroutines[panel.name] = StartCoroutine(SlideOut(panel));
    }
    currentVisibleName = "";
}

private void StopIfRunning(string name)
{
    if (runningCoroutines.TryGetValue(name, out var co) && co != null)
    {
        StopCoroutine(co);
        runningCoroutines[name] = null;
        menuPanels[name].isAnimating = false; // 念のため状態をリセット
    }
}

    IEnumerator SlideIn(MenuInfoPanel panel)
    {
        if (panel.isAnimating) yield break;

        panel.isAnimating = true;
        var rt = panel.menuInfoPanel.GetComponent<RectTransform>();
        Vector2 startPos = panel.originalPosition + new Vector2(0, slideDistance);
        Vector2 endPos = panel.originalPosition;

        panel.menuInfoPanel.SetActive(true);
        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            rt.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        rt.anchoredPosition = endPos;
        panel.isAnimating = false;
    }

    IEnumerator SlideOut(MenuInfoPanel panel)
    {
        if (panel.isAnimating) yield break;

        panel.isAnimating = true;
        var rt = panel.menuInfoPanel.GetComponent<RectTransform>();
        Vector2 startPos = panel.originalPosition;
        Vector2 endPos = panel.originalPosition + new Vector2(0, slideDistance);

        float time = 0;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;
            rt.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
            yield return null;
        }

        panel.menuInfoPanel.SetActive(false);
        rt.anchoredPosition = panel.originalPosition;
        panel.isAnimating = false;
    }
}
