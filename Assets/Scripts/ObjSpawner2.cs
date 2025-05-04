using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpawner2 : MonoBehaviour
{
    public ObjectPooler pooler;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 100; i++)  // プール数と同じ
        {
            SpawnFromPool();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnFromPool()
    {
        GameObject obj = pooler.GetPooledObject();
        // ランダムな位置を指定（例：x -5〜5, y -3〜3, z 2〜6）
        float randX = Random.Range(-5f, 5f);
        float randY = Random.Range(0f, 6f);

        obj.transform.position = new Vector3(randX, randY, 0);
        obj.SetActive(true);
    }
}
