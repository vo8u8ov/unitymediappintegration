using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class OSCReceiverHandler : MonoBehaviour
{
    public static HashSet<string> currentActiveHands = new HashSet<string>();
    
    [Header("右手マッピング")]
    public float rightMinX = -60f;
    public float rightMaxX = 60f;
    public float rightMinY = -40f;
    public float rightMaxY = 40f;

    [Header("左手マッピング")]
    public float leftMinX = -30f;
    public float leftMaxX = 30f;
    public float leftMinY = -20f;
    public float leftMaxY = 20f;

    [Header("手")]
    public float handDepth = 0.5f; // 手の深さ
    public Vector3 handScale = new Vector3(0.1f, 0.1f, 0.1f); // 手のスケール
    private OSCReceiver receiver;
    public GameObject handColliderObj;
    private Dictionary<string, Transform> handObjects = new Dictionary<string, Transform>();
    
    private float lastActiveHandsTime = -999f;
    private float activeHandsTimeout = 1.0f;
    private bool wasPreviouslyActive = true;

    void Start()
    {
        receiver = gameObject.AddComponent<OSCReceiver>();
        receiver.LocalPort = 9000;

        receiver.Bind("/like", OnLikeReceived);
        receiver.Bind("/hand", OnHand);
        receiver.Bind("/hand_pos/*/*", OnHandPosition); 
        receiver.Bind("/active_hands", OnActiveHands);
    }

    void OnLikeReceived(OSCMessage message)
    {
        Debug.Log("Received /like: " + message.Values[0].IntValue);
        int likeState = message.Values[0].IntValue;
        bool isLike = likeState >= 1;

        LikeEventManager.Instance.NotifyLike(isLike, likeState);
    }

    void OnHand(OSCMessage message)
    {
        int handState = message.Values[0].IntValue;
        Debug.Log("Received /hand: " + handState);
    }

    void OnHandPosition(OSCMessage message)
    {
        // if (!isHand) return; 
        Debug.Log("Received /hand_pos" );
        string[] parts = message.Address.Split('/');

        if (message.Values.Count < 3) return;
        if (parts.Length < 4 || message.Values.Count < 3) return; // parts[0] は空なので 3つ必要  
        string handKey = parts[2] + "_" + parts[3];  // 例: right_0 や left_1
        string handSide = parts[2].ToLower(); 

        if (!handObjects.ContainsKey(handKey))
        {
            GameObject handObj = Instantiate(handColliderObj);
            handObjects[handKey] = handObj.transform;
        }

        float x = message.Values[0].FloatValue;
        float y = message.Values[1].FloatValue;
        float z = message.Values[2].FloatValue;

        // z が遠いほど x/y の動きが小さくなるので、逆に補正
        // 近いときは1.0倍、遠くなると最大3倍くらい動かすように
        // z が 0.6 → 1（＝この距離を「遠い」と定義）
        float zoomFactor = Mathf.Lerp(1.0f, 3.0f, Mathf.Clamp01(z / 0.6f));
        // Mediapipeからは手の座標が「0〜1の正規化値」で送られてくる
        // x - 0.5fは、x が中央からどれだけズレているか  x - 0.5f = 0.0 画面の中央 	= -0.5 かなり左にズレている +0.5 かなり右にズレている
        x = 0.5f + (x - 0.5f) * zoomFactor;
        y = 0.5f + (y - 0.5f) * zoomFactor;
            
        float mappedX, mappedY;

        if (handSide == "right")
        {
            mappedX = Mathf.Lerp(rightMinX, rightMaxX, x);
            mappedY = Mathf.Lerp(rightMinY, rightMaxY, 1f - y);
        }
        else if (handSide == "left")
        {
            mappedX = Mathf.Lerp(leftMinX, leftMaxX, x);
            mappedY = Mathf.Lerp(leftMinY, leftMaxY, 1f - y);
        }
        else
        {
            // 念のため fallback
            mappedX = Mathf.Lerp(-50f, 50f, x);
            mappedY = Mathf.Lerp(-30f, 30f, 1f - y);
        }

        Vector3 pos = new Vector3(mappedX, mappedY, handDepth);

        handObjects[handKey].position = pos;
        handObjects[handKey].localScale = handScale;

        // === 処理を分岐 ===
        if (handSide == "right")
        {
            HandEventManager.Instance.NotifyRightHandPos(handKey, pos);
        }
        else if (handSide == "left")
        {
            HandEventManager.Instance.NotifyLeftHandPos(handKey, pos);
        }
    }

    void Update()
    {
        Debug.Log($"[DEBUG] Time since last /active_hands: {Time.time - lastActiveHandsTime}");
        // MediaPipeから何も来なくなったとき（通信停止）
        if (Time.time - lastActiveHandsTime > activeHandsTimeout && wasPreviouslyActive)
        {
            Debug.LogWarning("🛑 /active_hands が途絶えています。MediaPipe無反応の可能性。");
            HandEventManager.Instance.NotifyHandsInactive();
            wasPreviouslyActive = false;
        }
    }
    void OnActiveHands(OSCMessage message)
    {
        lastActiveHandsTime = Time.time;
        Debug.Log("Received /active_hands");
        currentActiveHands.Clear();
        foreach (var val in message.Values)
        {
            currentActiveHands.Add(val.StringValue);
        }

        foreach (var key in new List<string>(handObjects.Keys))
        {
            handObjects[key].gameObject.SetActive(currentActiveHands.Contains(key));
        }
        
        if (currentActiveHands.Count > 0 && !wasPreviouslyActive)
        {
            wasPreviouslyActive = true;
        }

        // 手が消えた状態（明示的に [] が来た場合）にも対応
        if (currentActiveHands.Count == 0 && wasPreviouslyActive)
        {
            HandEventManager.Instance.NotifyHandsInactive();
            wasPreviouslyActive = false;
        }
    }
}
