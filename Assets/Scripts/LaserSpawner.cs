using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
    public GameObject laserPrefab;      // レーザーのPrefab（URP用のエフェクトなど）
    public Transform[] spawnPoints;     // レーザーの発生位置（複数可）
    public float interval = 1.0f;       // レーザー生成間隔（秒）
    private bool isSpawning = false;

    void OnEnable()
    {
        LikeEventManager.Instance.OnLikeChanged += OnLikeChanged;
    }

    void OnDisable()
    {
        if (LikeEventManager.Instance != null)
            LikeEventManager.Instance.OnLikeChanged -= OnLikeChanged;
    }

    void OnLikeChanged(bool isLike, int likeCount)
    {
        if (isLike)
        {
            if (!isSpawning)
            {
                StartCoroutine(SpawnLoop());
            }
        }
        else
        {
            isSpawning = false;
        }
    }

    IEnumerator SpawnLoop()
    {
        isSpawning = true;

        while (isSpawning)
        {
            foreach (var point in spawnPoints)
            {
                GameObject laser = Instantiate(laserPrefab, point.position, point.rotation);
                // 例：3秒後に自動破棄
                Destroy(laser, 3f);
            }

            yield return new WaitForSeconds(interval);
        }
    }
}
