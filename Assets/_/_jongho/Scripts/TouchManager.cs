using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

public class TouchManager : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    
    private Vector3 indicatorPos;
    private Feed pet;
    public GameObject petStatus;

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
                petStatus.SetActive(false);
                return;
            }
            // 펫 터치
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, 6))
            {
                // 펫 상태창 팝업
                petStatus.SetActive(true);
            }
            else
            {
                indicatorPos = PlaceOnIndicator.currentAim.position;

                // 선택된 상태 체크해서 
                // 선택 상태 아니라면
                if (PlaceOnIndicator.placePrefab == null)
                {
                    // 펫을 해당 위치로 이동시키는 함수 호출
                    pet = PetStatManager.Instance.GetCurrentPet().GetComponent<Feed>();
                    // pet.MoveToThere(indicatorPos);
                }
                // 선택 상태라면
                else
                {
                    // 인디케이터 위치에 해당 오브젝트 instantiate
                    Instantiate(PlaceOnIndicator.placePrefab, indicatorPos, Quaternion.identity);                    
                    PlaceOnIndicator.placePrefab = null;
                }
            }
        }
    }

}
