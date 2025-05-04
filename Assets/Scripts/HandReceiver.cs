using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class HandReceiver : MonoBehaviour
{
    public GameObject handColliderObj;
    private OSCReceiver receiver;
    private bool isHand = false;  
    private int handCount = 0; 

    // Start is called before the first frame update
    void Start()
    {
        receiver = gameObject.AddComponent<OSCReceiver>();
        receiver.LocalPort = 9000;
        receiver.Bind("/hand", OnHand);
        receiver.Bind("/hand_pos", OnHandPosition); 
    }

    void OnHand(OSCMessage message)
    {
        int handState = message.Values[0].IntValue;
        Debug.Log("Received /hand: " + handState);
        isHand = (handState >= 1);
        handCount = handState;
    }


    void OnHandPosition(OSCMessage message)
    {
        // if (!isHand) return; 
        Debug.Log("Received /hand_pos" );
        if (message.Values.Count < 3) return;

        float x = message.Values[0].FloatValue;
        float y = message.Values[1].FloatValue;

        Vector3 pos = new Vector3(
            Mathf.Lerp(-13f, 13f, x),
            Mathf.Lerp(-13f, 13f, 1f - y),  // y軸は上下反転
            0f
        );

        if (handColliderObj != null)
        {
            handColliderObj.transform.position = pos;
        }
    }
}
