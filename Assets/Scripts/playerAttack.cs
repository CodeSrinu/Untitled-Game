using UnityEngine;

public class playerAttack : MonoBehaviour
{
    [SerializeField] private Animator gunAnim;
    [SerializeField] private syringe syringeRef;
    
   

    [SerializeField] private Transform firePoint;
    public bool canShoot = true;



    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            Attack();
            canShoot = false;

        }
    }


    private void Attack()
    {

        gunAnim.SetTrigger("fire");
        int dir = transform.localScale.x >= 0 ? 1 : -1;
        syringeRef.Fire(firePoint.position, dir);
    }
  

}
