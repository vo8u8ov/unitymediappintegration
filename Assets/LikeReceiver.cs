using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class LikeReceiver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var receiver = gameObject.AddComponent<OSCReceiver>();
        receiver.LocalPort = 9000;
        receiver.Bind("/like", OnLike);
    }

    void OnLike(OSCMessage message)
    {
        Debug.Log("LIKE! を受信しました！");
    }
}
