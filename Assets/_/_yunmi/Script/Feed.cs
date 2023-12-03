using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UI;
using Ursaanimation.CubicFarmAnimals;

public class Feed : MonoBehaviour
{
    public float speed = 1f;
    public GameObject bubbleParticle;
    private GameObject targetFood;
    private GameObject targetBall;
    //public Button feedButton;
    public Button ballButton;
    private AnimationController animationController;
    private Vector3 originalPosition;
    public Transform petMouth;
    public bool dancing = false;
    public float spawnInterval = 0.1f;
    private float lastSpawnTime = 0f;

    void Start()
    {
        originalPosition = transform.position;
        animationController = GetComponent<AnimationController>();
        //feedButton.onClick.AddListener(MoveToTargetFood);
        ballButton.onClick.AddListener(DanceAnimation);

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
        if (Input.GetMouseButtonDown(0))
        {
            PetShower();
        }

    }
    public void PetShower()
    {
        StartCoroutine(SpawnParticleCoroutine());
    }
    IEnumerator SpawnParticleCoroutine()
    {
        while (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == this.gameObject)
                {
                    Vector3 mousePosition = hit.point;
                    GameObject particleInstance = Instantiate(bubbleParticle, mousePosition, Quaternion.identity);
                    Destroy(particleInstance, 0.5f);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
        //washTouch = false;
    }
    
    public void DanceAnimation()
    {
        StartCoroutine(DanceRoutine());
    }
    IEnumerator DanceRoutine()
    {
        dancing = true;
        animationController.animator.Play(animationController.danceAnimation);
        transform.position += new Vector3(0, 0.75f, 0);

        yield return new WaitForSeconds(7);
        animationController.animator.Play(animationController.idleAnimation);
        dancing = false;
        transform.position = originalPosition;
    }
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

        ball.transform.SetParent(petMouth);
        ball.transform.localPosition = Vector3.zero;
        ball.transform.localRotation = Quaternion.identity;




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
