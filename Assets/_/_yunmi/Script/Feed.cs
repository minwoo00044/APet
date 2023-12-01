using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Ursaanimation.CubicFarmAnimals;

public class Feed : MonoBehaviour
{
    public float speed = 1f;
    private GameObject targetFood;
    private GameObject targetBall;
    //public Button feedButton;
    //public Button ballButton;
    private AnimationController animationController;
    private Vector3 originalPosition;

    void Start()
    {
        originalPosition = transform.position;
        animationController = GetComponent<AnimationController>();
        //feedButton.onClick.AddListener(MoveToTargetFood);
        //ballButton.onClick.AddListener(MoveToTargetBall);
    }
    void Update()
    {
        if (targetFood == null)
        {
            targetFood = GameObject.FindGameObjectWithTag("food");
            if (targetFood != null)
            {
                StartCoroutine(MoveTowards(targetFood.transform));
            }
        }

        if (targetBall == null)
        {
            targetBall = GameObject.FindGameObjectWithTag("ball");
            if (targetBall != null)
            {
                StartCoroutine(MoveToBall(targetBall.transform));
            }
        }
    }

    //public void MoveToTargetBall()
    //{
    //    targetBall = GameObject.FindGameObjectWithTag("ball");
    //    if (targetBall != null)
    //    {
    //        StartCoroutine(MoveToBall(targetBall.transform));
    //    }
    //}
    IEnumerator MoveToBall(Transform ball)
    {
        //animationController.animator.Play(animationController.turn90LAnimation);
        Vector3 targetDirection = ball.position - transform.position;
        targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            animationController.animator.Play(animationController.turn90LAnimation);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            yield return null;
        }

        animationController.animator.Play(animationController.runForwardAnimation);

        Vector3 targetPosition = new Vector3(ball.position.x, transform.position.y, ball.position.z);
        while (Vector3.Distance(transform.position, targetPosition) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);
            yield return null;
        }

        animationController.animator.Play(animationController.eatingAnimation);
        yield return new WaitForSeconds(0.6f);

        animationController.animator.Play(animationController.runForwardAnimation);
        while (Vector3.Distance(transform.position, originalPosition) > 1f)
        {
            Vector3 returnDirection = originalPosition - transform.position;
            returnDirection.y = 0;
            Quaternion returnRotation = Quaternion.LookRotation(returnDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, returnRotation, speed * Time.deltaTime);

            Vector3 returnPosition = new Vector3(originalPosition.x, transform.position.y, originalPosition.z);
            transform.position = Vector3.MoveTowards(transform.position, returnPosition, 5f * Time.deltaTime);
            yield return null;
        }
        animationController.animator.Play(animationController.idleAnimation);
        yield return null;

    }

    //public void MoveToTargetFood()
    //{
    //    targetFood = GameObject.FindGameObjectWithTag("food");
    //    if (targetFood != null)
    //    {
    //        StartCoroutine(MoveTowards(targetFood.transform));
    //    }
    //}

    IEnumerator MoveTowards(Transform target)
    {
        Vector3 targetDirection = target.position - transform.position;
        targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            animationController.animator.Play(animationController.turn90LAnimation);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            yield return null;
        }
        animationController.animator.Play(animationController.walkForwardAnimation);
        float stoppingDistance = 0.65f;
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z) - transform.forward * stoppingDistance;
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        animationController.animator.Play(animationController.eatingAnimation);
        Destroy(target.gameObject, 4f);
    }
}
