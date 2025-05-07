using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikeHeartSpawner : MonoBehaviour
{
    public GameObject heartPrefab;                     // 浮かぶハートのPrefab
    public Vector3 spawnPosition = new Vector3(0, -5, 0);
    private bool isCurrentlyLiked = false;

    void Start()
    {
        LikeEventManager.Instance.OnLikeChanged += HandleLike;
    }

    private void OnDestroy()
    {
        if (LikeEventManager.Instance != null)
            LikeEventManager.Instance.OnLikeChanged -= HandleLike;
    }

    private void HandleLike(bool isLike, int likeCount)
    {
        if (isLike && !isCurrentlyLiked)
        {
            isCurrentlyLiked = true;
            SpawnHeart();
        }
        else if (!isLike && isCurrentlyLiked)
        {
            isCurrentlyLiked = false;
        }
    }

    public void SpawnHeart()
    {
        Debug.Log("SpawnHeart called at position: " + spawnPosition);
        Quaternion spawnRotation = Quaternion.Euler(-90f, 0f, 0f);
        Instantiate(heartPrefab, spawnPosition, spawnRotation, transform);
    }
}
