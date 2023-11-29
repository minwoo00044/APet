using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Feed : MonoBehaviour
{
    public GameObject food;
    public Transform pet;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        food = GameObject.FindGameObjectWithTag("food");

        
    }
}
