using UnityEngine;

/// <summary>
/// 選択状態に応じてマテリアルを切り替える汎用コントローラー
/// </summary>
[RequireComponent(typeof(Renderer))]
public class ItemMaterialController : MonoBehaviour
{
    [Tooltip("通常時のマテリアル（未選択時）")]
    [SerializeField] private Material defaultMaterial;

    [Tooltip("選択時のマテリアル")]
    [SerializeField] private Material selectedMaterial;

    private Renderer itemRenderer;

    void Awake()
    {
        itemRenderer = GetComponent<Renderer>();

        // 初期化（defaultMaterialが空なら現在のマテリアルを使う）
        if (defaultMaterial == null && itemRenderer != null)
        {
            defaultMaterial = itemRenderer.sharedMaterial;
        }
    }

    /// <summary>
    /// 選択状態の切り替え
    /// </summary>
    public void SetSelected(bool isSelected)
    {
        if (itemRenderer == null) return;

        itemRenderer.material = isSelected ? selectedMaterial : defaultMaterial;
    }
}
