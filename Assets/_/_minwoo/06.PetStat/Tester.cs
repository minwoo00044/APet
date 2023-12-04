using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tester : MonoBehaviour
{
    public TMP_Text log;
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current);
            pointerData.position = Input.GetTouch(0).position;

            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, raycastResults);

            if (raycastResults.Count > 0)
            {
                print(raycastResults[0].gameObject.name);
                foreach (var result in raycastResults)
                {
                    log.text = result.gameObject.name;
                    Debug.Log("Hit " + result.gameObject.name);
                }
            }
        }
    }
}
