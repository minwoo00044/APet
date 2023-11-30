using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class feedTest : MonoBehaviour
{
    public GameObject food;
    public Button feedBtnTest;
    public float spawnRadius = 5f;
    // Start is called before the first frame update
    void Start()
    {
        feedBtnTest.onClick.AddListener(SpawnFood);
    }

    private void SpawnFood()
    {

        Vector3 randomPosition = UnityEngine.Random.insideUnitSphere * spawnRadius;
        randomPosition.y = 1;

        Instantiate(food, randomPosition, Quaternion.identity);


    }
}
