using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;

public class TouchEffect : MonoBehaviour
{
    Image img;

    public float sizeSpeed = 1;
    public UnityEngine.Color[] colors;
    public float colorSpeed = 5;

    public float minSize = 0.01f;
    public float maxSize = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();

        // 최소 크기로 시작
        transform.localScale = new Vector2(minSize, minSize);
        
        // 랜덤 색상
        img.material.color = colors[Random.Range(0, colors.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        // 최대 크기로
        transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), Time.deltaTime);


        // 시간이 지남에 따라 희미해진다.
        UnityEngine.Color color = img.material.color;
        color.a = Mathf.Lerp(color.a, 0, Time.deltaTime * colorSpeed);
        img.material.color = color;

        // 보이지 않으면 삭제
        if (color.a <= 0.01f)
        {
            Destroy(gameObject);
        }
    }
}
