using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandDetectionUIManager : MonoBehaviour
{
    public TextMeshProUGUI DetectionText;
    public string Text;
    public int FontSize = 36;
    // Start is called before the first frame update
    void Start()
    {
        DetectionText.text = "";
        DetectionText.fontSize = FontSize;
        HandEventManager.Instance.OnRightHandChanged += ChangeText;
        HandEventManager.Instance.OnNoHandsDetected += HideText;
    }

    // Update is called once per frame
    private void ChangeText(string handkey, Vector3 pos)
    {
        if (handkey.ToLower().Contains("right"))
        {
            DetectionText.text = Text;
        }
    }

    private void HideText()
    {
        DetectionText.text = "";
    }
}
