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

            // UI를 터치했는지 먼저 확인하기 위한 레이캐스트
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = new Vector2(touch.position.x, touch.position.y);

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase != TouchPhase.Ended)
            {
                // UI를 터치한 경우의 처리를 여기에 작성합니다.
                Debug.Log("UI Touched");
                text.text = "UI Touched";
            }
            else if(!EventSystem.current.IsPointerOverGameObject(touch.fingerId) && touch.phase != TouchPhase.Ended)
            {
                // UI가 아닌 다른 대상을 터치한 경우의 처리를 여기에 작성합니다.

                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // 물리적 레이캐스팅을 수행하여 일반 게임 오브젝트를 터치했는지 확인합니다.

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
