using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.XR.Haptics;
using UnityEngine.UI;
using Ursaanimation.CubicFarmAnimals;

public class PetController : MonoBehaviour
{
    public float speed = 1f;
    public GameObject bubbleParticle;
    public GameObject heartParticle;
    private GameObject targetFood;
    private GameObject targetBall;
    public Button feedButton;
    public Button ballButton;
    private AnimationController animationController;
    private Vector3 originalPosition;
    private Vector3 playerPosition;
    public Transform petMouth;
    public Transform strokeHeart;
    public bool dancing = false;
    public float spawnInterval = 0.1f;
    private Coroutine moveCoroutine;
    void Start()
    {
        originalPosition = transform.position;
        playerPosition = Camera.main.transform.position + Camera.main.transform.forward * 2f;
        animationController = GetComponent<AnimationController>();
        feedButton.onClick.AddListener(PetShower);
        ballButton.onClick.AddListener(PetStroke);

    }
    void Update()
    {
        if (targetFood == null)
        {
            targetFood = GameObject.FindGameObjectWithTag("food");
            if (targetFood != null)
            {
                StartCoroutine(MoveToFood(targetFood.transform));
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
#if UNITY_EDITOR
        //if (Input.GetMouseButtonDown(0))
        //{
        //    //MoveToThere Test
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        MoveToThere(hit.point);
        //    }
        //}
#endif
    }
    public void PetStroke()
    {
        StartCoroutine(StrokeHeartParticle());
    }
    IEnumerator StrokeHeartParticle()
    {
        GameObject particleInstance = Instantiate(heartParticle, strokeHeart.position, Quaternion.identity);
        ParticleSystem particleSystem = particleInstance.GetComponentInChildren<ParticleSystem>();
        particleSystem.Play();
        yield return new WaitForSeconds(5f);
        particleSystem.Stop();
        Destroy(particleInstance);
        PetStatManager.Instance.NameStatPair[PetStatManager.Instance.GetCurrentName()].Love += 50;

    }
    public void PetShower()
    {
        StartCoroutine(SpawnParticleCoroutine());
       
    }
    IEnumerator SpawnParticleCoroutine()
    {
        GameObject particleInstance = Instantiate(bubbleParticle, transform.position, Quaternion.identity);
        ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>();
        particleSystem.Play();
        yield return new WaitForSeconds(2f);
        particleSystem.Stop();
        yield return new WaitWhile(() => particleSystem.IsAlive(true));

        PetStatManager.Instance.NameStatPair[PetStatManager.Instance.GetCurrentName()].Clean += 100;
        Destroy(particleInstance);
    }
    public void MoveToThere(Vector3 targetTransform)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        moveCoroutine = StartCoroutine(MoveToLocoation(targetTransform));
    }
    IEnumerator MoveToLocoation(Vector3 targetTransform)
    {
        Vector3 targetDirection = targetTransform - transform.position;
        //targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        float rotationSpeed = 2f;

        float angle = Quaternion.Angle(transform.rotation, targetRotation);
        Vector3 cross = Vector3.Cross(transform.forward, targetDirection);
        if (cross.y < 0) angle = -angle;

        if (angle > 0)
            animationController.animator.Play(animationController.turn90RAnimation);
        else
            animationController.animator.Play(animationController.turn90LAnimation);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.3f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        animationController.animator.Play(animationController.runForwardAnimation);

        Vector3 targetPosition = new Vector3(targetTransform.x, transform.position.y, targetTransform.z);
        while (Vector3.Distance(transform.position, targetPosition) > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, 5f * Time.deltaTime);
            yield return null;
        }
        animationController.animator.Play(animationController.idleAnimation);
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
        PetStatManager.Instance.NameStatPair[PetStatManager.Instance.GetCurrentName()].Health += 20;
        PetStatManager.Instance.NameStatPair[PetStatManager.Instance.GetCurrentName()].Love += 20;


    }
    IEnumerator MoveToBall(Transform ball)
    {
        //animationController.animator.Play(animationController.turn90LAnimation);
        Vector3 targetDirection = ball.position - transform.position;
        //targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        float rotatoinSpeed = 2f;
        float angel = Quaternion.Angle(transform.rotation, targetRotation);
        Vector3 cross = Vector3.Cross(transform.forward, targetDirection);
        if (cross.y < 0) angel = -angel;
        if (angel > 0)
            animationController.animator.Play(animationController.turn90RAnimation);
        else
            animationController.animator.Play(animationController.turn90LAnimation);
        while (Quaternion.Angle(transform.rotation, targetRotation) > 2f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotatoinSpeed * Time.deltaTime);
            yield return null;
        }
        animationController.animator.Play(animationController.runForwardAnimation);

        Vector3 targetPosition = new Vector3(ball.position.x, ball.position.y, ball.position.z);
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
        while (Vector3.Distance(transform.position, playerPosition) > 0.3f)
        {
            Vector3 returnDirection = playerPosition - transform.position;
            //returnDirection.y = 0;
            if (targetDirection.magnitude > 0.1f)
            {
                Quaternion returnRotation = Quaternion.LookRotation(returnDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, returnRotation, speed * Time.deltaTime);

                Vector3 returnPosition = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z);
                transform.position = Vector3.MoveTowards(transform.position, returnPosition, 5f * Time.deltaTime);
                yield return null;
            }
        }
        animationController.animator.Play(animationController.idleAnimation);
        Destroy(ball.gameObject, 1f);
        PetStatManager.Instance.NameStatPair[PetStatManager.Instance.GetCurrentName()].Health += 60;
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

    IEnumerator MoveToFood(Transform target)
    {
        Vector3 targetDirection = target.position - transform.position;
        //targetDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            animationController.animator.Play(animationController.turn90LAnimation);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
            yield return null;
        }
        animationController.animator.Play(animationController.walkForwardAnimation);
        float stoppingDistance = 0.65f;
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y, target.position.z) - transform.forward * stoppingDistance;
        while (Vector3.Distance(transform.position, targetPosition) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            yield return null;
        }
        animationController.animator.Play(animationController.eatingAnimation);

        Destroy(target.gameObject, 4f);
        PetStatManager.Instance.NameStatPair[PetStatManager.Instance.GetCurrentName()].Hunger += 100;


    }

}
