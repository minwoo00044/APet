using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchTester : MonoBehaviour
{
    public LayerMask mask;
    public TMP_Text text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // UI�� ��ġ�ߴ��� ���� Ȯ���ϱ� ���� ����ĳ��Ʈ
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase != TouchPhase.Ended)
            {
                // UI�� ��ġ�� ����� ó���� ���⿡ �ۼ��մϴ�.
                Debug.Log("UI Touched");
                text.text = "UI Touched";
            }
            else if(!EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase != TouchPhase.Ended)
            {
                // UI�� �ƴ� �ٸ� ����� ��ġ�� ����� ó���� ���⿡ �ۼ��մϴ�.

                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // ������ ����ĳ������ �����Ͽ� �Ϲ� ���� ������Ʈ�� ��ġ�ߴ��� Ȯ���մϴ�.

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, mask.value))
                {
                    Debug.Log("GameObject Touched");
                    text.text = "GameObject Touched";
                }
                else
                {
                    Debug.Log("Empty Space Touched");
                    text.text = "Empty Space Touched";
                }
            }
        }

    }
}
