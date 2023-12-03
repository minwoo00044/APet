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

            // ����ĳ��Ʈ�� �����ϰ� UI ���̾�� ����ĳ��Ʈ ��Ʈ�� Ȯ��
            Ray ray = Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            
            // UI ��ġ
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, 5))
            {                
                return;
            }
            // �� ��ġ
            else if (Physics.Raycast(ray, out hit, Mathf.Infinity, 7)) //  �� layer�� ����
            {
                // ���� �ش� ��ġ�� �̵���Ű�� �Լ� ȣ��
            }
            else
            {

                // ���õ� ���� üũ�ؼ� 
                // ���� ���� �ƴ϶��
                if (PlaceOnIndicator.placePrefab != null)
                {

                }
                // ���� ���¶��
                else
                {

                }                    

                    // ��ġ�� ���� plane detection�� ���� �νĵ� �ٴ��̶��, Ư�� ������Ʈ�� �ش� ��ġ�� �̵�
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
