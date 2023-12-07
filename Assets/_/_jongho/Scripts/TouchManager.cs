using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using TMPro;
using System;

public class TouchManager : MonoBehaviour
{
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    
    private Vector3 indicatorPos;
    private PetController pet;
    public GameObject petStatus;

    public TMP_Text tmp1;
    public TMP_Text tmp2;
    public TMP_Text tmp3;

    public LayerMask mask;
    
    public static Action<string> onLog;


    //public GameObject EffectPrefab;
    //float spawnTime;
    //public float defaultTime = 0.05f;

    Touch touch;

    //private Vector2 UIPos;
    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        onLog += txt => tmp1.text = txt;
        //UIPos = gameObject.GetComponent<RectTransform>().position;
    }

    private void Update()
    {
        tmp1.text = "default";
        tmp2.text = "default";
        tmp3.text = "default";

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            

            // UI를 터치했는지 먼저   확인하기 위한 레이캐스트
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);


            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase != TouchPhase.Ended)
            {
                // UI를 터치한 경우의 처리를 여기에 작성합니다.
                
                
            }
            else if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase != TouchPhase.Ended)
            {
                // 레이캐스트를 생성하고 UI 레이어에서 레이캐스트 히트를 확인
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // 펫 터치
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 6))
                {
                    tmp2.text = "pet touch";
                    // 펫 상태창 팝업
                    petStatus.SetActive(true);
                }
                else
                {
                    indicatorPos = PlaceOnIndicator.currentAim.position;

                    // 선택 상태 아니라면
                    if (PlaceOnIndicator.placePrefab == null)
                    {
                        tmp3.text = "pet move";
                        // 펫을 indicator 위치로 이동시키는 함수 호출
                        pet = PetStatManager.Instance.GetCurrentPet().GetComponent<PetController>();
                        pet.MoveToThere(indicatorPos);
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
            
            //if (spawnTime >= defaultTime)
            //{
            //    EffectCreate();
            //    spawnTime = 0;
            //}
            //spawnTime += Time.deltaTime;
        }               
    }

    //void EffectCreate()
    //{
        
    //        // 터치한 위치에 프리팹 생성
    //        GameObject touchEff = Instantiate(EffectPrefab, transform);
    //        touchEff.transform.position = touch.position;

    //        // Canvas의 RectTransform에 맞춰 조정
    //        //RectTransform rectTransform = touchEff.GetComponent<RectTransform>();
    //        //rectTransform.anchoredPosition = touch.position + UIPos;
        
    //}
}
