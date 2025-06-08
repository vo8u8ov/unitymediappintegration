using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandDetectionUIManager : MonoBehaviour
{
    [Header("手の検出範囲を制限する Transform")]
    public Transform minTransform;  // 左端（最小X）
    public Transform maxTransform;  // 右端（最大X）

    [Header("表示するテキスト")]
    public TextMeshProUGUI DetectionText;
    public string Text;
    public int FontSize = 36;

    void Start()
    {
        DetectionText.text = "";
        DetectionText.fontSize = FontSize;
        HandEventManager.Instance.OnRightHandChanged += ChangeText;
        HandEventManager.Instance.OnNoHandsDetected += HideText;
    }

    private void ChangeText(string handKey, Vector3 pos)
    {
        if (!handKey.ToLower().Contains("right"))
            return;

        // 範囲の取得
        float minX = Mathf.Min(minTransform.position.x, maxTransform.position.x);
        float maxX = Mathf.Max(minTransform.position.x, maxTransform.position.x);

        // 範囲外なら非表示
        if (pos.x < minX || pos.x > maxX)
        {
            HideText();
            return;
        }

        // 範囲内ならテキストを表示
        DetectionText.text = Text;
    }

    private void HideText()
    {
        DetectionText.text = "";
    }
}
