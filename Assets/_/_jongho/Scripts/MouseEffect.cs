using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseEffect : MonoBehaviour
{
    public GameObject starPrefab;
    float spawnTime;
    public float defaultTime = 0.05f;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && spawnTime >= defaultTime)
        {
            StarCreate();
        }
        spawnTime += Time.deltaTime;
    }

    void StarCreate()
    {
        Vector3 mPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mPosition.z = 0;
        Instantiate(starPrefab, mPosition, Quaternion.identity);
    }
}
