using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LikeHeartSpawner : MonoBehaviour
{
    public GameObject heartPrefab;
    [Header("Spawn Settings")]
    public float minX = -3f;
    public float maxX = 3f;
    public float y = 0f;
    public float z = 0f;
    private bool isCurrentlyLiked = false;
    private int previousLikeCount = 0;

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
        if (isLike)
        {
            int diff = likeCount - previousLikeCount;

            if (diff > 0)
            {
                for (int i = 0; i < diff; i++)
                {
                    SpawnHeart(); // 差分だけ一気に放出
                }
            }

            isCurrentlyLiked = true;
        }
        else
        {
            isCurrentlyLiked = false;
        }

        previousLikeCount = likeCount;
    }


    public void SpawnHeart()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 randomSpawnPos = new Vector3(randomX, y, z);

        Quaternion spawnRotation = Quaternion.Euler(-90f, 0f, 0f);
        Instantiate(heartPrefab, randomSpawnPos, spawnRotation, transform);
    }
}
