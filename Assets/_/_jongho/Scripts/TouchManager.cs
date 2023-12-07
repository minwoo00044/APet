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

            

            // UI�� ��ġ�ߴ��� ����   Ȯ���ϱ� ���� ����ĳ��Ʈ
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);


            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase != TouchPhase.Ended)
            {
                // UI�� ��ġ�� ����� ó���� ���⿡ �ۼ��մϴ�.
                
                
            }
            else if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase != TouchPhase.Ended)
            {
                // ����ĳ��Ʈ�� �����ϰ� UI ���̾�� ����ĳ��Ʈ ��Ʈ�� Ȯ��
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // �� ��ġ
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 6))
                {
                    tmp2.text = "pet touch";
                    // �� ����â �˾�
                    petStatus.SetActive(true);
                }
                else
                {
                    indicatorPos = PlaceOnIndicator.currentAim.position;

                    // ���� ���� �ƴ϶��
                    if (PlaceOnIndicator.placePrefab == null)
                    {
                        tmp3.text = "pet move";
                        // ���� indicator ��ġ�� �̵���Ű�� �Լ� ȣ��
                        pet = PetStatManager.Instance.GetCurrentPet().GetComponent<PetController>();
                        pet.MoveToThere(indicatorPos);
                    }
                    // ���� ���¶��
                    else
                    {
                        // �ε������� ��ġ�� �ش� ������Ʈ instantiate
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
        
    //        // ��ġ�� ��ġ�� ������ ����
    //        GameObject touchEff = Instantiate(EffectPrefab, transform);
    //        touchEff.transform.position = touch.position;

    //        // Canvas�� RectTransform�� ���� ����
    //        //RectTransform rectTransform = touchEff.GetComponent<RectTransform>();
    //        //rectTransform.anchoredPosition = touch.position + UIPos;
        
    //}
}
