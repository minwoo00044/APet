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

        // �ּ� ũ��� ����
        transform.localScale = new Vector2(minSize, minSize);
        
        // ���� ����
        img.material.color = colors[Random.Range(0, colors.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        // �ִ� ũ���
        transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), Time.deltaTime);


        // �ð��� ������ ���� ���������.
        UnityEngine.Color color = img.material.color;
        color.a = Mathf.Lerp(color.a, 0, Time.deltaTime * colorSpeed);
        img.material.color = color;

        // ������ ������ ����
        if (color.a <= 0.01f)
        {
            Destroy(gameObject);
        }
    }
}
