using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LogController : MonoBehaviour
{
    public static Action<string> onPetChange;
    public static Action<string> onObjectChange;

    [SerializeField] float fadeOutTime;
    private TMP_Text log;
    private void Start()
    {
        log = GetComponent<TMP_Text>();
        onObjectChange = null;
        onPetChange = null;

        onPetChange += (name) => StartCoroutine(ChangeLogAndFadeOut($"You changed your pet to {name}. "));
        onObjectChange += (name) => StartCoroutine(ChangeLogAndFadeOut($"You can place {name} by touch."));
    }

    IEnumerator ChangeLogAndFadeOut(string txt)
    {
        log.text = txt;
        Color originalColor = log.color;
        float elapsedTime = 0;
        log.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);

        while (elapsedTime < fadeOutTime)
        {
            float alpha = Mathf.Lerp(1, 0, elapsedTime / fadeOutTime);
            log.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        log.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
    }
}
