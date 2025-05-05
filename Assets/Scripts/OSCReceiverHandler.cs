using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class OSCReceiverHandler : MonoBehaviour
{
    private OSCReceiver receiver;
    public GameObject handColliderObj;
    private Dictionary<string, Transform> handObjects = new Dictionary<string, Transform>();
    private HashSet<string> currentActiveHands = new HashSet<string>();

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
        Debug.Log("handKeys" +handKey);

        if (!handObjects.ContainsKey(handKey))
        {
            GameObject handObj = Instantiate(handColliderObj);
            handObjects[handKey] = handObj.transform;
        }

        float x = message.Values[0].FloatValue;
        float y = message.Values[1].FloatValue;

        Vector3 pos = new Vector3(
            Mathf.Lerp(-13f, 13f, x),
            Mathf.Lerp(-5f, 13f, 1f - y),
            0f
        );

        handObjects[handKey].position = pos;

        // HandEventManager.Instance.UpdateHandPosition(handKey, pos);
    }

    void OnActiveHands(OSCMessage message)
    {
        Debug.Log("Received /active_hands" );
        currentActiveHands.Clear();
        foreach (var val in message.Values)
        {
            Debug.Log("Active hands: " + val.StringValue);
            currentActiveHands.Add(val.StringValue);
        }

        foreach (var key in new List<string>(handObjects.Keys))
        {
            Debug.Log("key: " + key);
            handObjects[key].gameObject.SetActive(currentActiveHands.Contains(key));
        }
    }
}
