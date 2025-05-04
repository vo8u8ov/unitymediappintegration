using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScaler : MonoBehaviour
{
    private bool _isLiked = false;     // Pythonからの状態受け取り
    private float _minScale = 1f;    // 最小スケール
    private float _maxScale = 1.2f;    // 最大スケール
    private float _speed = 2f;         // 変化スピード
    private float _returnSpeed = 3f;   // 元に戻るスピード

    private Vector3 originalScale;   // 初期サイズ
    private float time;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (_isLiked)
        {
            time += Time.deltaTime * _speed;
            //Lerpの公式（線形補間) Lerp(min, max, t) = min + (max - min) × t 
            float scaleFactor = Mathf.Lerp(_minScale, _maxScale, Mathf.PingPong(time, 1f));
            transform.localScale = originalScale * scaleFactor;
        }
        else
        {
            // スムーズに元のサイズへ戻す
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, Time.deltaTime * _returnSpeed);
            time = 0f;
        }
    }

    // OSCなど外部からこの関数で状態を渡す
    public void SetLiked(bool liked)
    {
        _isLiked = liked;
    }
}
