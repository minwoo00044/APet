using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class ConfigPanelController : MonoBehaviour
{
    [SerializeField] Button configBtn;
    [SerializeField] GameObject configPanel;
    [SerializeField] float rotSpeed;
    [SerializeField] float duration;

    [SerializeField] Slider soundSlider;
    private bool isOpen = true;
    private float maxHeight;
    private RectTransform configRT;
    private RectTransform btnRT;

    private void Start()
    {
        configRT = configPanel.GetComponent<RectTransform>();
        btnRT = configBtn.GetComponent<RectTransform>();
        maxHeight = configRT.rect.height;
        configBtn.onClick.AddListener(() => StartCoroutine(ToggleAction()));
        StartCoroutine(ToggleAction());

        soundSlider.onValueChanged.AddListener(OnSoundSliderChanged);
    }
    IEnumerator ToggleAction()
    {
        float currentRotSpeed = isOpen ? rotSpeed : -rotSpeed;
        float targetHeight = isOpen ? 0 : maxHeight;
        float currentHeight = configRT.rect.height;
        float zCurrenRot = btnRT.eulerAngles.z;
        float elapsedTime = 0;
        if (!isOpen)
            ChildToggle(true);
        while (Mathf.Abs(configRT.rect.height - targetHeight) > 0.01f)
        {
            currentHeight = Mathf.Lerp(currentHeight, targetHeight, elapsedTime / duration);
            zCurrenRot = Mathf.Lerp(zCurrenRot, 360, elapsedTime / duration) * rotSpeed;
            btnRT.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zCurrenRot));
            configRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentHeight);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        btnRT.transform.rotation = Quaternion.Euler(Vector3.zero);
        isOpen = !isOpen;
        ChildToggle();
    }
    private void ChildToggle()
    {
        Transform[] childs = configRT.GetComponentsInChildren<Transform>(true);
        foreach (var item in childs)
            item.gameObject.SetActive(isOpen);
    }
    private void ChildToggle(bool flag)
    {
        Transform[] childs = configRT.GetComponentsInChildren<Transform>(true);
        foreach (var item in childs)
            item.gameObject.SetActive(flag);
    }

    private void OnSoundSliderChanged(float value)
    {
        AudioListener.volume = value;
    }


}
