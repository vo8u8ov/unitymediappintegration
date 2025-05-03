using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpawner : MonoBehaviour
{
    public ObjectPooler pooler;
    private float _spawnInterval = 0.1f;
    private float _spawnTimer = 0f;
    private bool _isSpawning = false; 

    void Start()
    {
        _spawnTimer = _spawnInterval; // Initialize the spawn timer
    }

    // Update is called once per frame
    void Update()
    {
        if (_isSpawning)
        {
            _spawnTimer -= Time.deltaTime; // カウントダウン！
            if (_spawnTimer <= 0f)
            {
                SpawnFromPool();
                _spawnTimer = _spawnInterval;  // リセット
            }
        }
    }

    public void IsSpawn(bool isSpawn, Transform target = null)
    {
        if (target != null)
        {
            transform.position = target.position; // Set the spawner's position to the hand position
        }
        
        _isSpawning = isSpawn; // Set the spawning state

        if (!isSpawn)
        {
            _spawnTimer = _spawnInterval; // Reset the spawn timer when stopping
        }
    }

    void SpawnFromPool()
    {
        GameObject obj = pooler.GetPooledObject();
        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero; // 前の動きをリセット
            rb.AddForce(Vector3.up * 300f); // 上に力を加える
        }

        StartCoroutine(DisableAfterSeconds(obj, 5f));  // 自動で非アクティブ化
    }

    IEnumerator DisableAfterSeconds(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }
}
