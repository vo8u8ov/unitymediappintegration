using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LikeUIManager : MonoBehaviour
{
    public TextMeshProUGUI LikeText;
    // Start is called before the first frame update
    void Start()
    {
        LikeText.text = "";
        LikeText.gameObject.SetActive(false);
        LikeEventManager.Instance.OnLikeChanged += HandleLike;
    }

    // Update is called once per frame
    private void HandleLike(bool isLike, int likeCount)
    {
        if (isLike)
        {
            LikeText.text = "Like x" + likeCount.ToString();
        }
        LikeText.gameObject.SetActive(isLike);
    }
}
