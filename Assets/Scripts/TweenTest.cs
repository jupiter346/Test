using UnityEngine;
using DG.Tweening;

public class TweenTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 260, 600
        GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 550), 0.7f);
        GetComponent<RectTransform>().DOScale(2.0f, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
