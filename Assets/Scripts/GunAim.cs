using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunAim : MonoBehaviour
{

    Camera cam;
    [SerializeField]private Transform playerTransform;
    

    private void Start()
    {
        cam  = Camera.main;
    }



    private void Update()
    {
        if (FindAnyObjectByType<GunController>().isEquipped)
        {
            Aim();
        }
    }

    void Aim()
    {
        //bool isFacingRight = transform.root.localScale.x > 0;

        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = mousePos - (Vector2)transform.position;

        float facing = Mathf.Sign(playerTransform.localScale.x);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg * facing;



        //if (!isFacingRight)
        //{
        //    angle += 180f;
        //}

        angle = Mathf.Clamp(angle, -40f, 40f);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
