using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using static UnityEditor.Experimental.GraphView.GraphView;

public class TouchManager : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    
    public GameObject petObject;

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // 레이캐스트를 생성하고 UI 레이어에서 레이캐스트 히트를 확인
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            
            // UI 터치
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 5))
            {                
                return;
            }
            // 펫 터치
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, 7)) //  펫 layer로 수정
            {
                // 펫을 해당 위치로 이동시키는 함수 호출
            }
            else
            {

                // 선택된 상태 체크해서 
                // 선택 상태 아니라면
                if (PlaceOnIndicator.placePrefab != null)
                {

                }
                // 선택 상태라면
                else
                {

                }                    

                    // 터치된 곳이 plane detection을 통해 인식된 바닥이라면, 특정 오브젝트를 해당 위치로 이동
                    if (arRaycastManager.Raycast(touch.position, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    
                    

                }
                //
                else
                {

                }
            }            
        }
    }
    
}
