using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    TouchState state = TouchState.sheep;
    [SerializeField] ARPointCloudManager pointCloudManager;
    [SerializeField] ARRaycastManager arRaycastManager;
    [SerializeField] TMP_Text logTxt;
    [SerializeField] TMP_Text logTxt1;
    [SerializeField] TMP_Text logTxt2;
    private List<ARPointCloud> pointClouds;
    private void Awake()
    {
        touchInput.performed += _ => { placeObject(); };
        //placementIndicator.SetActive(false);
        pointClouds = new List<ARPointCloud>();
        pointCloudManager.pointCloudsChanged += OnPointCloudsChanged;
    }
    private void OnEnable()
    {
        touchInput.Enable();
    }
    private void OnDisable()
    {
        touchInput.Disable();
    }

    private void OnPointCloudsChanged(ARPointCloudChangedEventArgs args)
    {
        // �߰��� ����Ʈ Ŭ���带 ����Ʈ�� �߰��մϴ�.
        foreach (var added in args.added)
        {
            pointClouds.Add(added);
            logTxt.text = $"{pointClouds.Count}";
        }

        // ������Ʈ�� ����Ʈ Ŭ���带 ����Ʈ���� ã�� ���� �����մϴ�.
        foreach (var updated in args.updated)
        {
            int index = pointClouds.FindIndex(x => x.trackableId == updated.trackableId);
            if (index != -1)
            {
                pointClouds[index] = updated;
                logTxt1.text = $"{pointClouds.Count}";
            }
        }

        // ���ŵ� ����Ʈ Ŭ���带 ����Ʈ���� �����մϴ�.
        foreach (var removed in args.removed)
        {
            pointClouds.RemoveAll(x => x.trackableId == removed.trackableId);
        }
    }
    void Update()
    {
        Vector2 screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        float nearestPointDistance = float.MaxValue;
        Pose nearestPointPose = new Pose();

        foreach (var pointCloud in pointClouds)
        {
            foreach (var point in pointCloud.positions)
            {
                Vector3 pointPosition = point;
                float pointDistance = Vector3.Distance(Camera.main.transform.position, pointPosition);
                if (pointDistance < nearestPointDistance)
                {
                    List<ARRaycastHit> arHits = new List<ARRaycastHit>();
                    if (arRaycastManager.Raycast(screenCenter, arHits, TrackableType.FeaturePoint)) // ���� Ư¡���� ã���ϴ�.
                    {
                        ARRaycastHit hit = arHits[0];
                        float distance = Vector3.Distance(hit.pose.position, pointPosition);
                        if (distance < 1f)
                        {
                            nearestPointDistance = pointDistance;
                            nearestPointPose = hit.pose;
                        }
                    }
                    else if (arRaycastManager.Raycast(screenCenter, arHits, TrackableType.Planes)) // Ư¡���� ã�� ���ϸ� ����� ã���ϴ�.
                    {
                        ARRaycastHit hit = arHits[0];
                        nearestPointDistance = pointDistance;
                        nearestPointPose = hit.pose;
                    }
                    else
                    {
                        placementIndicator.SetActive(false);
                    }
                }
            }
        }

        if (nearestPointDistance < float.MaxValue)
        {
            logTxt2.text = $"active";
            placementIndicator.transform.position = nearestPointPose.position;
            placementIndicator.SetActive(true);
        }

    }


    private void placeObject()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == UnityEngine.TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                // UI Ŭ���̹Ƿ� ���� ������ �ߴ��մϴ�.
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
        if (state == TouchState.sheep)
            state = TouchState.food;
        else
            state = TouchState.sheep;
    }
}