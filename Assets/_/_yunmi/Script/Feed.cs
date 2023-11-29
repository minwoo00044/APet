using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Ursaanimation.CubicFarmAnimals;

public class Feed : MonoBehaviour
{
    public float speed = 5f;
    private GameObject targetFood;
    public Button feedButton;
    public AnimationController animationController;

    void Start()
    {
        feedButton.onClick.AddListener(MoveToTargetFood);
    }

    void MoveToTargetFood()
    {
        targetFood = GameObject.FindGameObjectWithTag("food");
        if (targetFood != null)
        {
            StartCoroutine(MoveTowards(targetFood.transform));
        }
    }

    IEnumerator MoveTowards(Transform target)
    {
        Vector3 targetDirection = (new Vector3(target.position.x, transform.position.y, target.position.z) - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            yield return null;
        }

        animationController.animator.Play(animationController.walkForwardAnimation);

        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z); // YÃà °íÁ¤
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }

        animationController.animator.Play(animationController.eatingAnimation);
        Destroy(target.gameObject, 3f);
    }
}
