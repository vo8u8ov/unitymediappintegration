using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class LikeReceiver : MonoBehaviour
{
    public ParticleSystem[] likeParticles;
    public ParticleSystem auraEffect;
    public GameObject logoObj;
    public ObjSpawner objSpawner;
    private OSCReceiver receiver;
    private bool isLike = false;  // 現在Like中かどうかを記憶
    private int likeCount = 0; // Likeのカウントを保持
    private Vector3 lastHandPosition = Vector3.zero;
    private LogoScaler logoScaler; // LogoScalerのインスタンスを保持
    
    // Start is called before the first frame update
    void Start()
    {
        receiver = gameObject.AddComponent<OSCReceiver>();
        receiver.LocalPort = 9000;
        receiver.Bind("/like", OnLike);
        receiver.Bind("/hand_pos", OnHandPosition); 

        logoScaler = logoObj.GetComponent<LogoScaler>();
    }

    void OnLike(OSCMessage message)
    {
        int likeState = message.Values[0].IntValue;
        Debug.Log("Received /like: " + likeState);
        isLike = (likeState >= 1);
        likeCount = likeState;

        // Debug.Log("Received /like: " + likeState);

        if (isLike)
        {
            if (likeCount == 1)
            {
                // 0番目だけ再生、それ以外は停止
                for (int i = 0; i < likeParticles.Length; i++)
                {
                    if (i == 0)
                    {
                        if (!likeParticles[i].isPlaying)
                            likeParticles[i].Play();
                    }
                    else
                    {
                        if (likeParticles[i].isPlaying)
                            likeParticles[i].Stop();
                    }
                }
            }
            else if (likeCount == 0)
            {
                for (int i = 0; i < likeParticles.Length; i++)
                {
                    if (likeParticles[i].isPlaying)
                        likeParticles[i].Stop();
                }
            }
            else if (likeCount >= 2)
            {
                for (int i = 0; i < likeParticles.Length; i++)
                {
                    likeParticles[i].Play();
                }
            }

            auraEffect.Play();
            logoScaler.SetLiked(true);
            objSpawner.IsSpawn(true, likeParticles[0].transform);
        }
        else
        {
            objSpawner.IsSpawn(false);
            logoScaler.SetLiked(false);
            likeCount = 0; // Likeのカウントをリセット

            for (int i = 0; i < likeParticles.Length; i++)
            {
                if (likeParticles[i].isPlaying)
                {
                   likeParticles[i].Stop();
                }
            }

            if (auraEffect.isPlaying)
            {
                Debug.Log("Aura not playing");
                auraEffect.Stop();
            }
        }
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

        if (logoObj != null)
        {
            logoObj.transform.position = pos; // ロゴの位置を更新
        }

        // パーティクル位置を更新
        if (likeParticles != null)
        {
            if (likeCount == 1)
            {
                likeParticles[0].transform.position = pos;
            }
            else if (likeCount >= 2)
            {
                for (int i = 0; i < likeParticles.Length; i++)
                {
                    likeParticles[i].transform.position = pos;
                }
            }
        }

        if (auraEffect != null)
        {
            pos.z = 0.5f; 
            auraEffect.transform.position = pos;
        }
    }
}
