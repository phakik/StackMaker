using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvas : MonoBehaviour
{
    [SerializeField] bool isDestroyOnClose = false;
    private void Awake()
    {
        RectTransform rect = GetComponent<RectTransform>();
        float ratio = (float)Screen.width / (float)Screen.height;
        if (ratio > 2.1f)
        {
            Vector2 leftBotton = rect.offsetMin;
            Vector2 rightTop = rect.offsetMax;
            leftBotton.y = 0f;
            rightTop.y = -100f;
            rect.offsetMin = leftBotton;
            rect.offsetMax = rightTop;
        }
    }
    // goi trc khi canvas duoc active
    public virtual void SetUp()
    {

    }
    public virtual void Open()
    {
        gameObject.SetActive(true);
    }
    public virtual void Close(float time)
    {
        Invoke(nameof(CloseDirectly), time);
    }
    public virtual void CloseDirectly()
    {
        if (isDestroyOnClose)
        {
            Destroy(this.gameObject);
        }
        else
        {
            gameObject.SetActive(false);

        }
    }
}
