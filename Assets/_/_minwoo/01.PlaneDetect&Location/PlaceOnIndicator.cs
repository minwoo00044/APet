using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PlaceOnIndicator : MonoBehaviour
{
    [SerializeField] GameObject placementIndicator;
    [SerializeField] GameObject placePrefab;

    GameObject spawnedObject;

    [SerializeField] InputAction touchInput;

    ARRaycastManager raycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

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
        if(raycastManager.Raycast(new Vector2(Screen.width/ 2, Screen.height /2), hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            placementIndicator.transform.SetPositionAndRotation(hitPose.position, hitPose.rotation);

            if(!placementIndicator.activeInHierarchy)
                placementIndicator.SetActive(true);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void placeObject()
    {
        if (!placementIndicator.activeInHierarchy)
            return;
        if(spawnedObject == null)
        {
            spawnedObject = Instantiate(placePrefab, placementIndicator.transform.position, placementIndicator.transform.rotation);
        }
    }
}
