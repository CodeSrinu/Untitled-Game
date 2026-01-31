using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private playerHealth playerHealthScript;
    private Animator enemyAnim;
    //[SerializeField]private syringe syringScript;
    public float unconsciousTime = 6f;

    private bool isUnconscious = false;


    private void Start()
    {
        enemyAnim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (playerHealthScript.isDied || isUnconscious) return;


        moveTowardsPlayer();

    }



    private void moveTowardsPlayer()
    {

        Vector3 currentPos = transform.position;


        float newXPos = Mathf.MoveTowards(
            currentPos.x,
            playerTransform.position.x,
            moveSpeed *  Time.deltaTime
            );

        int dir = currentPos.x > playerTransform.position.x ? -1 : 1;

        transform.position = new Vector3(newXPos, currentPos.y);
        transform.localScale = new Vector3(dir, 1);
    }

    public void GetUnconcious()
    {
        enemyAnim.SetTrigger("injected");
        isUnconscious = true;


        StartCoroutine(UnconciousRoutine());
    }



    private IEnumerator UnconciousRoutine()
    {
        yield return new WaitForSeconds(unconsciousTime);
        WakeUp();
    }

    public void WakeUp()
    {
        enemyAnim.SetTrigger("wakeUp");
    }



    public void OnAnimationFinished()
    {
        isUnconscious = false;
    }
}
