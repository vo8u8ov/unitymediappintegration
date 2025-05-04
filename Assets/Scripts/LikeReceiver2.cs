using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class LikeReceiver2 : MonoBehaviour
{
    private OSCReceiver receiver;
    private bool isLike = false;  // 現在Like中かどうかを記憶
    private int likeCount = 0; // Likeのカウントを保持
    public GameObject logoObj;

    // Start is called before the first frame update
    void Start()
    {
        receiver = gameObject.AddComponent<OSCReceiver>();
        receiver.LocalPort = 9000;
        receiver.Bind("/like", OnLike);
        receiver.Bind("/hand_pos", OnHandPosition); 
    }

    void OnLike(OSCMessage message)
    {
        int likeState = message.Values[0].IntValue;
        Debug.Log("Received /like: " + likeState);
        isLike = (likeState >= 1);
        likeCount = likeState;
 
    }


    void OnHandPosition(OSCMessage message)
    {
        if (!isLike) return; // Like中だけ反応

        if (message.Values.Count < 3) return;

        float x = message.Values[0].FloatValue;
        float y = message.Values[1].FloatValue;

        // MediaPipe（0〜1）→ Unityワールド座標（調整可能）
        Vector3 pos = new Vector3(
            Mathf.Lerp(-5f, 5f, x),
            Mathf.Lerp(-3f, 3f, 1f - y),  // y軸は上下反転
            0f
        );

        if (logoObj != null)
        {
            logoObj.transform.position = pos; // ロゴの位置を更新
            Debug.Log("Logo Position: " + pos);
        }
    }
}
