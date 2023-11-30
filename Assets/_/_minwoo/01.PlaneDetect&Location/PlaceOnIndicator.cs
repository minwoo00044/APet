using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public enum TouchState
{
    sheep,
    food
}
public class PlaceOnIndicator : MonoBehaviour
{
    public static Pose currentAim;
    [SerializeField] GameObject placementIndicator;
    [SerializeField] GameObject placePrefab;
    [SerializeField] GameObject sheep;

    GameObject spawnedObject;

    [SerializeField] InputAction touchInput;

    ARRaycastManager raycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    TouchState state = TouchState.sheep;
    private void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
        touchInput.performed += _ => { placeObject(); };
        placementIndicator.SetActive(false);
    }
    private void OnEnable()
    {
        touchInput.Enable();
    }
    private void OnDisable()
    {
        touchInput.Disable();
    }
    private void Update()
    {
        if (raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            currentAim = hitPose;
            placementIndicator.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);

            if (!placementIndicator.activeInHierarchy)
                placementIndicator.SetActive(true);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void placeObject()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                // UI 클릭이므로 이후 로직을 중단합니다.
                return;
            }
        }
        if (!placementIndicator.activeInHierarchy)
            return;
        switch (state)
        {
            case TouchState.sheep:
                sheep.transform.position = placementIndicator.transform.position;
                break;
            case TouchState.food:
                spawnedObject = Instantiate(placePrefab, placementIndicator.transform.position, placementIndicator.transform.rotation);
                break;
        }
    }
    public void ToggleState()
    {
        if(state == TouchState.sheep)
            state = TouchState.food;
        else
            state = TouchState.sheep;
    }
}
