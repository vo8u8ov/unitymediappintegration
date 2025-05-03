using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _objectsToSpawn;
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
                SpawnObjects();
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

    private void SpawnObjects()
    {
        int randomIndex = Random.Range(0, _objectsToSpawn.Length);
        GameObject objToSpawn = _objectsToSpawn[randomIndex];

        GameObject spawned = Instantiate(objToSpawn, transform.position, transform.rotation);
        Rigidbody rb = spawned.GetComponent<Rigidbody>();
        if (rb != null)
        {
            float upwardForce = 500f; // 上に向かう力を設定
            rb.AddForce(Vector3.up * upwardForce);
        }
    }
}
