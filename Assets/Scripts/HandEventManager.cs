using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandEventManager : MonoBehaviour
{
    public static HandEventManager Instance { get; private set; }

    private Dictionary<string, Vector3> handPositions = new Dictionary<string, Vector3>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void UpdateHandPosition(string handKey, Vector3 position)
    {
        handPositions[handKey] = position;
    }

    public bool TryGetHandPosition(string handKey, out Vector3 position)
    {
        return handPositions.TryGetValue(handKey, out position);
    }

    public IEnumerable<Vector3> GetAllHandPositions()
    {
        return handPositions.Values;
    }
}
