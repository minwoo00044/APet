using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    public InputAction touchAction;

    void OnEnable()
    {
        touchAction.Enable();
    }

    void OnDisable()
    {
        touchAction.Disable();
    }

    void Update()
    {
        if (touchAction.triggered)
        {
            Debug.Log("Screen touched");
        }
    }
}
