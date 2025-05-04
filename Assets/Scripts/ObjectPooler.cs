using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject[] prefabsToPool;
    public int poolSize = 20;
    private List<GameObject> pool = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject prefab  = prefabsToPool[Random.Range(0, prefabsToPool.Length)];
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy) //オブジェクトがシーン上で非アクティブなら再利用
            {
                Debug.Log("Reusing object from pool");
                return obj;
            }
        }

        // プールが足りない場合は拡張（必要に応じて）
        GameObject prefab = prefabsToPool[Random.Range(0, prefabsToPool.Length)];
        GameObject newObj = Instantiate(prefab);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }
}
