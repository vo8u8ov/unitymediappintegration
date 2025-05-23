using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandButtonSelector : MonoBehaviour
{
    public List<Button> buttons;
    public RectTransform uiCanvas;
    public RectTransform handCursorUI;

    public float dwellTime = 1.5f; // 止めてからクリックされるまでの秒数
    public float cursorX = 0f;
    private int currentIndex = -1;
    private float dwellTimer = 0f;
    private bool isHovering = false;
    private int clickedIndex = -1; // 最後にクリックされたボタン

    void OnEnable()
    {
        HandEventManager.Instance.OnLeftHandChanged += HandleLeftHand;
    }

    void OnDisable()
    {
        if (HandEventManager.Instance != null)
            HandEventManager.Instance.OnLeftHandChanged -= HandleLeftHand;
    }

    void Update()
    {
        bool isLeftHandActive = OSCReceiverHandler.currentActiveHands.Contains("left_0");

        if (handCursorUI != null && handCursorUI.gameObject.activeSelf != isLeftHandActive)
        {
            handCursorUI.gameObject.SetActive(isLeftHandActive);
        }

        if (isHovering)
        {
            dwellTimer += Time.deltaTime;

            if (dwellTimer >= dwellTime)
            {
                ClickCurrentButton();
                dwellTimer = 0f;
            }
        }
    }

    void HandleLeftHand(string handKey, Vector3 worldPos)
    {
        // スクリーン座標に変換（worldPos.x = 0 → 画面中央）
        Vector2 screenPos = Camera.main.WorldToScreenPoint(worldPos);

        // ✅ 修正：Xを固定（カーソルと当たり判定を一致させる）
        screenPos.x =  Screen.width / 2 + cursorX; // cursorX は -700 なので、左側にオフセット

        // UI座標に変換
        RectTransformUtility.ScreenPointToLocalPointInRectangle(uiCanvas, screenPos, null, out Vector2 localPoint);

        // カーソル位置を更新（Xを固定）
        if (handCursorUI != null)
        {
            localPoint.x = cursorX;
            handCursorUI.anchoredPosition = localPoint;
        }

        // ボタン当たり判定
        int hoveredIndex = -1;
        for (int i = 0; i < buttons.Count; i++)
        {
            RectTransform btnRect = buttons[i].GetComponent<RectTransform>();
            RectTransformUtility.ScreenPointToLocalPointInRectangle(btnRect, screenPos, null, out Vector2 btnLocalPoint);

            if (btnRect.rect.Contains(btnLocalPoint))
            {
                hoveredIndex = i;
                Debug.Log($"Button {i}: local check {btnLocalPoint}, rect {btnRect.rect}");
                break;
            }
        }

        if (hoveredIndex != currentIndex)
        {
            currentIndex = hoveredIndex;
            dwellTimer = 0f;
            HighlightButton(currentIndex);
            isHovering = true;
        }
        else if (currentIndex != -1)
        {
            isHovering = true; // 継続ホバー中
        }
        else
        {
            isHovering = false; // ボタンに乗ってない
            dwellTimer = 0f;
        }
    }

    void HighlightButton(int index)
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            Image img = buttons[i].GetComponent<Image>();
            if (img != null)
            {
                if (i == clickedIndex)
                    img.color = Color.blue;  // ✅ クリック済み
                else if (i == index)
                    img.color = Color.yellow; // ✅ 今ホバー中
                else
                    img.color = Color.white; // その他
            }
        }
    }

    void ClickCurrentButton()
    {
         Debug.Log("ClickCurrentButton");
        if (currentIndex >= 0 && currentIndex < buttons.Count)
        {
            Debug.Log("Auto-clicked: " + buttons[currentIndex].name);
            buttons[currentIndex].onClick.Invoke();
            clickedIndex = currentIndex;
            HighlightButton(currentIndex); // ✅ 色を即更新
        }
    }
}
