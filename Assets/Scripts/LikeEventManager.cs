using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)] 
public class LikeEventManager : MonoBehaviour
{
    public static LikeEventManager Instance { get; private set; }

    public event Action<bool, int> OnLikeChanged;

    private bool currentLikeState = false;
    private int currentLikeCount = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void NotifyLike(bool isLike, int likeCount)
    {
        Debug.Log("NotifyLike called: " + isLike + ", " + likeCount);
        if (isLike != currentLikeState || likeCount != currentLikeCount)
        {
            currentLikeState = isLike;
            currentLikeCount = likeCount;
            OnLikeChanged?.Invoke(isLike, likeCount);
        }
    }
}
