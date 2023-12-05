using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartSceneManager : MonoBehaviour
{
    [SerializeField] Button gameStartBtn;
    [SerializeField] GameObject logo;
    [SerializeField] float animatingTime;
    void Start()
    {
        gameStartBtn.onClick.AddListener(AnimatingAndStartGame);
    }

    private void AnimatingAndStartGame()
    {
        StartCoroutine(AnimationSequence());
    }

    private IEnumerator AnimationSequence()
    {
        RectTransform gameStartBtnRect = gameStartBtn.GetComponent<RectTransform>();
        RectTransform logoRect = logo.GetComponent<RectTransform>();

        StartCoroutine(UIAnimation(gameStartBtnRect, true, animatingTime));
        yield return new WaitForSeconds(animatingTime / 2);
        yield return  StartCoroutine(UIAnimation(logoRect, false, animatingTime));
        SceneManager.LoadScene(1);
    }

    IEnumerator UIAnimation(RectTransform rect, bool isRight, float duration)
    {
        Vector2 targetPosition;
        if (isRight)
        {
            targetPosition = new Vector2(Screen.width, rect.anchoredPosition.y);
        }
        else
        {
            targetPosition = new Vector2(-Screen.width, rect.anchoredPosition.y);
        }

        float elapsedTime = 0;
        Vector2 startingPos = rect.anchoredPosition;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            rect.anchoredPosition = Vector2.Lerp(startingPos, targetPosition, elapsedTime / duration);
            yield return null;
        }
        rect.anchoredPosition = targetPosition;
    }
}
