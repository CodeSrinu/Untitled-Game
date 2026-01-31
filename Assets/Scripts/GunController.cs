using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isEquipped = false;
    [SerializeField] private Transform gunHolder;
    [SerializeField] private Collider2D gunCollider;
    bool isPlayerNear = false;
    [SerializeField] private Animator gunAnim;
    [SerializeField] private playerMovement playerMovementRef;
    Vector3 OgGunScale;
    private void Start()
    {
        gunAnim = GetComponent<Animator>();
    }

    private void Update()
    {


        if (isEquipped)
        {
            transform.localRotation = Quaternion.identity;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isEquipped && playerMovementRef.isOnGround)
            {
                OgGunScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                DropTheGun();
            }
            else if(!isEquipped && isPlayerNear)
            {
                EquipTheGun();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isPlayerNear = false;
        }
    }


    private void DropTheGun()
    {
        gunAnim.SetTrigger("dropTheGun");
        isEquipped = false;
        transform.SetParent(null);
        gunCollider.enabled = true;
        gunCollider.isTrigger = true;

    }

    private void EquipTheGun()
    {
        gunAnim.SetTrigger("pickUp");
        isEquipped = true;
        transform.SetParent(gunHolder);
        gunCollider.enabled = false;
        isPlayerNear = false;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = OgGunScale;
    }

}
