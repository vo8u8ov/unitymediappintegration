using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class LikeReceiver : MonoBehaviour
{
    public ParticleSystem likeParticles;
    public ParticleSystem auraEffect;
    public ObjSpawner objSpawner;
    private OSCReceiver receiver;
    private bool isLike = false;  // 現在Like中かどうかを記憶
    private bool isFist = false;
    private Vector3 lastHandPosition = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
        receiver = gameObject.AddComponent<OSCReceiver>();
        receiver.LocalPort = 9000;
        receiver.Bind("/like", OnLike);
        receiver.Bind("/hand_pos", OnHandPosition);  // ← 追加！
        receiver.Bind("/fist", OnFist);
    }

    void OnLike(OSCMessage message)
    {
        int likeState = message.Values[0].IntValue;
        isLike = (likeState == 1);

        // Debug.Log("Received /like: " + likeState);

        if (isLike)
        {
            auraEffect.Play();
            likeParticles.Play();
            objSpawner.IsSpawn(true, likeParticles.transform);
        }
        else
        {
            objSpawner.IsSpawn(false);
            if (likeParticles.isPlaying)
            {
                auraEffect.Stop();
                likeParticles.Stop();
            }
        }
    }

    void OnFist(OSCMessage message)
    {
        int fistState = message.Values[0].IntValue;
        isFist = (fistState == 1);

        Debug.Log("Received /fist: " + fistState);
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

        lastHandPosition = pos;

        // パーティクル位置を更新
        if (likeParticles != null)
        {
            likeParticles.transform.position = pos;
        }

        if (auraEffect != null)
        {
            auraEffect.transform.position = pos;
        }
    }
}
