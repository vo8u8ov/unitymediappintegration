using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SenseManager : MonoBehaviour
{
    [System.Serializable]
    public class SenseItem
    {
        public GameObject obj;
        public GameObject objToHide;
        [HideInInspector] public Vector3 startPos;
        [HideInInspector] public Vector3 centerPos;
        [HideInInspector] public float   depth;
    }

    [Tooltip("Quad（扇子）を登録。名前は HandleSelection の itemName と一致させておく")]
    [SerializeField] private List<SenseItem> items = new List<SenseItem>();

    [Tooltip("スライドにかける時間 (秒)")]
    [SerializeField] private float animationDuration = 0.5f;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;

        foreach (var si in items)
        {
            // 今の位置を開始位置としてキャッシュ
            si.startPos = si.obj.transform.position;

            // カメラからの距離を取得し、中心Y=0.5に対応するWorld位置を計算
            var vp = cam.WorldToViewportPoint(si.startPos);
            si.depth = vp.z;
            si.centerPos = cam.ViewportToWorldPoint(new Vector3(vp.x, 0.5f, si.depth));

            // 初期は全て非表示にしておく（閉じた扇子も含む）
            si.obj.SetActive(false);
        }
    }

    /// <summary>
    /// 選択した itemName の扇子だけ表示して中央へスライド、他の閉じ扇子は非表示、開いてる扇子は元位置へスライド
    /// </summary>
    public void ShowItem(string itemName)
    {
        foreach (var si in items)
        {
            if (si.obj.name == itemName)
            {
                // 選択アイテムは表示して中央へ
                si.obj.SetActive(true);
                si.objToHide.SetActive(false);
                StartCoroutine(MoveTo(si.obj.transform, si.centerPos));
            }
            else
            {
                // 他の“開いてる”扇子は元の位置に戻す
                si.obj.SetActive(true);
                StartCoroutine(MoveTo(si.obj.transform, si.startPos));
            }
        }
    }

    /// <summary>
    /// 全ての扇子を元位置に戻し、閉じた扇子は非表示
    /// </summary>
    public void HideAllItems()
    {
        foreach (var si in items)
        {
            si.obj.SetActive(true);
            si.objToHide.SetActive(true);
            StartCoroutine(MoveTo(si.obj.transform, si.startPos));
        }
    }

    private IEnumerator MoveTo(Transform tf, Vector3 target)
    {
        Vector3 from = tf.position;
        float elapsed = 0f;
        while (elapsed < animationDuration)
        {
            elapsed += Time.deltaTime;
            tf.position = Vector3.Lerp(from, target, elapsed / animationDuration);
            yield return null;
        }
        tf.position = target;
    }
}
