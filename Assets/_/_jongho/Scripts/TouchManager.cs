using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using TMPro;

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
    

    private void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
    }

    private void Update()
    {
        tmp1.text = "default";
        tmp2.text = "default";
        tmp3.text = "default";

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

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
    }

}
