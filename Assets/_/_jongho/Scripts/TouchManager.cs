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

            // ����ĳ��Ʈ�� �����ϰ� UI ���̾�� ����ĳ��Ʈ ��Ʈ�� Ȯ��
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;

            // UI ��ġ
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 5))
            {
                petStatus.SetActive(false);
                return;
            }
            // �� ��ġ
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, 6))
            {
                // �� ����â �˾�
                petStatus.SetActive(true);
            }
            else
            {
                indicatorPos = PlaceOnIndicator.currentAim.position;

                // ���õ� ���� üũ�ؼ� 
                // ���� ���� �ƴ϶��
                if (PlaceOnIndicator.placePrefab == null)
                {
                    // ���� �ش� ��ġ�� �̵���Ű�� �Լ� ȣ��
                    pet = PetStatManager.Instance.GetCurrentPet().GetComponent<Feed>();
                    // pet.MoveToThere(indicatorPos);
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
