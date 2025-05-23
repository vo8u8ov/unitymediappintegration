using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : MonoBehaviour
{
    public static ParticlePool Instance { get; private set; }

    public GameObject particlePrefab;
    public int poolSize = 50;

    private Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(particlePrefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public void PlayEffect(Vector3 position)
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.transform.position = position;
            obj.SetActive(true);
            ParticleSystem ps = obj.GetComponent<ParticleSystem>();
            ps.Play();

            float duration = ps.main.duration + ps.main.startLifetime.constantMax;
            StartCoroutine(DisableAfterSeconds(obj, duration));
        }
    }

    private IEnumerator DisableAfterSeconds(GameObject obj, float sec)
    {
        yield return new WaitForSeconds(sec);
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
