using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class VFXController : MonoBehaviour
{
    public VisualEffect vfx; // VFX Graphをアタッチする
    public Vector2 vector2Value = new Vector2(1.0f, 1.0f); // Exposed名に合わせる

    void Update()
    {
        // VFX Graph 側の Exposed プロパティ名が "VelocityXY" の Vector2型と一致させる
        vfx.SetVector2("VelocityXY", vector2Value);

        // 上下キーで Y の値を動的に変更
        if (Input.GetKey(KeyCode.UpArrow))
            vector2Value.y += Time.deltaTime;

        if (Input.GetKey(KeyCode.DownArrow))
            vector2Value.y -= Time.deltaTime;

        // 左右キーで X の値を動的に変更
        if (Input.GetKey(KeyCode.RightArrow))
            vector2Value.x += Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
            vector2Value.x -= Time.deltaTime;
    }
}
