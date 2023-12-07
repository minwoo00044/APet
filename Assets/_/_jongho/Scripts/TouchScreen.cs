using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreen : MonoBehaviour
{
    public GameObject EffectPrefab;
    float spawnTime;
    public float defaultTime = 0.05f;

    Touch touch;

    


    // Update is called once per frame
    void Update()
    {
        

        if (spawnTime >= defaultTime)
        {
            EffectCreate();
            spawnTime = 0;
        }
        spawnTime += Time.deltaTime;
    }

    private void EffectCreate()
    {

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            // ��ġ�� ��ġ�� ������ ����
            GameObject touchEff = Instantiate(EffectPrefab, transform);
            touchEff.transform.position = touch.position;

            Debug.Log("�׽�Ʈ1");
        }

        // Canvas�� RectTransform�� ���� ����
        //RectTransform rectTransform = touchEff.GetComponent<RectTransform>();
        //rectTransform.anchoredPosition = touch.position + UIPos;

    }
}
