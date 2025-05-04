using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjSpawner2 : MonoBehaviour
{
    public ObjectPooler pooler;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 50; i++)  // プール数と同じ
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
        float randX = Random.Range(-12f, 12f);
        float randY = Random.Range(-6f, 6f);

        obj.transform.position = new Vector3(randX, randY, 0);
        obj.SetActive(true);

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;  // 重力の影響なし、動かない
        }
    }
}
