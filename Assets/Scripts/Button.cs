using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Animator btnAnim;
    [SerializeField] private Door doorRef;

    private void Start()
    {
        btnAnim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Syringe":
                btnAnim.SetTrigger("click");
                doorRef.OpenDoor();
                break;
            case "Player":
                break;
            default:
                break;
        }
    }



}
