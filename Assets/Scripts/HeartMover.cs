using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartMover : MonoBehaviour
{
    public float floatSpeed = 1.0f;
    public float swayAmount = 0.5f;
    public float swaySpeed = 2.0f;
    public float lifeTime = 10.0f;

    private Vector3 startPos;
    private float timer = 0f;

    void Start()
    {
        startPos = transform.position;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        timer += Time.deltaTime;
        float sway = Mathf.Sin(timer * swaySpeed) * swayAmount;
        Vector3 newPos = startPos + new Vector3(sway, timer * floatSpeed, 0);
        transform.position = newPos;
    }
}
