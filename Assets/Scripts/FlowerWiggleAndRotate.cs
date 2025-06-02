using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerWiggleAndRotate : MonoBehaviour
{
    public float wiggleSpeed = 2f;
    public float wiggleAmount = 0.1f;
    public float rotateSpeed = 30f;

    private Vector3 startPos;
    private float actualRotateSpeed;

    void Start()
    {
        startPos = transform.position;

        // 個別に揺れスピードをずらす
        wiggleSpeed *= Random.Range(0.8f, 1.2f);

        // 回転スピードをランダムに（正負どちらも）
        actualRotateSpeed = rotateSpeed * Random.Range(-1f, 1f);
    }

    void Update()
    {
        // ゆらゆら（Y軸に対して）
        float offset = Mathf.Sin(Time.time * wiggleSpeed + GetInstanceID() * 0.1f) * wiggleAmount;
        transform.position = new Vector3(startPos.x, startPos.y + offset, startPos.z);

        // Z軸で回転（ランダム方向）
        transform.Rotate(0, 0, actualRotateSpeed * Time.deltaTime);
    }
}
