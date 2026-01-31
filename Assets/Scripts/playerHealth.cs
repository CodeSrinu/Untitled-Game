using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class playerHealth : MonoBehaviour
{
    public bool isDied = false;
    private Animator playerAnim;

    [SerializeField] private float deathDelay = 3f;
    private playerMovement playerMovementScript;
    private playerAttack playerAttackScript;


    private void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerMovementScript = GetComponent<playerMovement>();
        playerAttackScript = GetComponent<playerAttack>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        switch (collision.gameObject.tag) 
        {
            case "Enemy":
                Die();
                break;
            default:
                break;
        }

    }


    public void Die()
    {
        if (isDied) return;

        isDied = true;

        playerAnim.SetBool("playerDied", isDied);
        playerMovementScript.enabled = false;
        playerAttackScript.enabled = false;
        

        StartCoroutine(DeathRoutine());

    }

    private IEnumerator DeathRoutine()
    {
        yield return new WaitForSeconds(deathDelay);

        FindObjectOfType<GameSceneManager>().ReloadLevel();
    }
}
