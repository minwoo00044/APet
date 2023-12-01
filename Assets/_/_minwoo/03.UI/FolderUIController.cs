using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FolderUIController : MonoBehaviour
{
    [SerializeField] RectTransform folderRT;
    [SerializeField] Button toggleBtn;
    [SerializeField] float duration;
    private bool isOpen = false;
    private float maxWidth;

    private void Start()
    {
        maxWidth = folderRT.rect.width;
        ToggleFolder();
    }
    public void ToggleFolder()
    {
        StartCoroutine(ToggleAction());
    }
    IEnumerator ToggleAction()
    {
        float targetWidth = isOpen ? 0 : maxWidth;
        float currentWidth = folderRT.rect.width;
        float elapsedTime = 0; 

        while (Mathf.Abs(folderRT.rect.width - targetWidth) > 0.01f)
        {
            currentWidth = Mathf.Lerp(currentWidth, targetWidth, elapsedTime / duration); 
            folderRT.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
            elapsedTime += Time.deltaTime; 
            yield return null;
        }

        isOpen = !isOpen;
    }


}
