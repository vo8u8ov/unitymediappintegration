using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WagashiManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem flowerParticle;
    [SerializeField] private List<GameObject> wagashiObjects = new List<GameObject>();
    void Start()
    {
        HandEventManager.Instance.OnRightHandChanged += OnRightHandMoved;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnRightHandMoved(string key, Vector3 handWorldPos)
    {
        Debug.Log("OnRightHandMoved");
        if (flowerParticle != null)
        {
            flowerParticle.transform.position = handWorldPos;
        }

        foreach (var wagashi in wagashiObjects)
        {
            Debug.Log("WagashiManager OnRightHandMoved");
            Vector2 wagashiXY = new Vector2(wagashi.transform.position.x, wagashi.transform.position.y);
            Vector2 handXY = new Vector2(handWorldPos.x, handWorldPos.y);

            float dist = Vector2.Distance(wagashiXY, handXY);
            if (dist < 1.0f)
            {
                WagashiInteraction wi = wagashi.GetComponent<WagashiInteraction>();
                if (wi != null)
                {   Debug.Log("WagashiInteraction Triggered");
                    wi.TriggerPulse();
                }
            }
        }
    }
}
