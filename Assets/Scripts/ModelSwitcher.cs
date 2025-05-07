using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelSwitcher : MonoBehaviour
{
    public List<GameObject> models; // 複数のモデルをインスペクタで登録
    public ObjectRotatorByHand rotator; // Rotatorスクリプトへの参照

    private int currentIndex = -1;
    private void Start()
    {
        // 初期状態で全モデルを非表示に
        for (int i = 0; i < models.Count; i++)
        {
            models[i].SetActive(false);
        }

        // 最初のモデルを表示
        if (models.Count > 0)
        {
            OnSwitchToModel(0);
        }
    }
    
    public void OnSwitchToModel(int index)
    {
        if (index < 0 || index >= models.Count) return;

        // 全モデルを非表示に
        for (int i = 0; i < models.Count; i++)
        {
            models[i].SetActive(false);
        }

        // 対象モデルを表示
        models[index].SetActive(true);
        currentIndex = index;

        // Rotatorの対象を更新
        if (rotator != null)
        {
            rotator.targetObject = models[index].transform;
        }
    }
}
