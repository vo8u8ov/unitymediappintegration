using System.Collections.Generic;
using UnityEngine;

public class FlowerGlowManager : MonoBehaviour
{
    public GameObject[] flowerPrefabs; // 5種類のPrefabを登録
    public int flowerCount = 50;
    public float glowDistance = 150f;
    public Color normalColor = Color.white;
    public Color glowColor = Color.yellow;

    private List<GameObject> flowerObjects = new List<GameObject>();
    private Vector3 handScreenPos;

    void Start()
    {
        HandEventManager.Instance.OnRightHandChanged += OnRightHandMoved;

        // 🌸花をランダムに画面内に配置
        for (int i = 0; i < flowerCount; i++)
        {
            GameObject prefab = flowerPrefabs[Random.Range(0, flowerPrefabs.Length)];
            GameObject flower = Instantiate(prefab);

            // 画面座標をランダム取得 → ワールド変換
            float x = Random.Range(0f, 1f);
            float y = Random.Range(0f, 1f);
            Vector3 screenPos = new Vector3(x * Screen.width, y * Screen.height, 15f);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            flower.transform.position = worldPos;

            flowerObjects.Add(flower);
        }
    }

    void OnDestroy()
    {
        // if (HandEventManager.Instance != null)
            // HandEventManager.Instance.OnRightHandChanged -= OnRightHandMoved;
    }

    void OnRightHandMoved(string key, Vector3 handWorldPos)
    {
        foreach (var flower in flowerObjects)
        {
            Debug.Log("Checking flower: " + flower.name);
            Vector2 flowerXY = new Vector2(flower.transform.position.x, flower.transform.position.y);
            Vector2 handXY = new Vector2(handWorldPos.x, handWorldPos.y);

            float dist = Vector2.Distance(flowerXY, handXY);
            if (dist < 1.0f)
            {
                FlowerInteraction fi = flower.GetComponent<FlowerInteraction>();
                if (fi != null)
                {
                    fi.TriggerPulse();
                }
            }
        }
    }
}
