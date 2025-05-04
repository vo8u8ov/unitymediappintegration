using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class OSCReceiverHandler : MonoBehaviour
{
    private OSCReceiver receiver;

    void Start()
    {
        receiver = gameObject.AddComponent<OSCReceiver>();
        receiver.LocalPort = 9000;

        receiver.Bind("/like", OnLikeReceived);
    }

    void OnLikeReceived(OSCMessage message)
    {
        Debug.Log("Received /like: " + message.Values[0].IntValue);
        int likeState = message.Values[0].IntValue;
        bool isLike = likeState >= 1;

        LikeEventManager.Instance.NotifyLike(isLike, likeState);
    }
}
