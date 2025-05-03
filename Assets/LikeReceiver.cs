using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class LikeReceiver : MonoBehaviour
{
    public ParticleSystem likeParticles;  // ← Unity上でInspectorにドラッグ
    private OSCReceiver receiver;
    // Start is called before the first frame update
    void Start()
    {
        receiver = gameObject.AddComponent<OSCReceiver>();
        receiver.LocalPort = 9000;
        receiver.Bind("/like", OnLike);
    }

    void OnLike(OSCMessage message)
    {
        Debug.Log(message.ToString());  // ← デバッグ用
        int likeState = message.Values[0].IntValue;

        if (likeState == 1)
        {
            if (!likeParticles.isPlaying)
            {
                likeParticles.Clear();
                likeParticles.Play();
            }
        }
        else
        {
            if (likeParticles.isPlaying)
            {
                likeParticles.Stop();
            }
        }
    }
}
