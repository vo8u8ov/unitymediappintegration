using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI LikeText;
    // Start is called before the first frame update
    void Start()
    {
        LikeText.text = "";
        LikeText.gameObject.SetActive(false);
    }

    public void ShowLikeText(bool isLike, bool isSperLike, int likecount)
    {
        if (isLike)
        {
            if (isSperLike)
            {
                LikeText.text = "Like x" + likecount.ToString();
            }
            else
            {
                
                LikeText.text = "Like";
            }
        }
        LikeText.gameObject.SetActive(isLike);
    }
}
