using UnityEngine;

public class syringe : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private playerAttack playerAttackRef;
    [SerializeField] private playerMovement playerMovementRef;

    public bool isFlying = false;
    private Vector2 fireDir;

    [SerializeField] float injectDelay = 0.15f;
    
    public bool isInjected = false;
    private bool isReturning = false;
    [SerializeField] private Transform returnTarget;

    private Vector3 returnOffset;

    [SerializeField] private LineRenderer ropeLine;
    [SerializeField] private Transform ropeSlot;
    public int maxRopePoints = 10;
    public float maxRopeLenght = 10f;
    private float currentDistance;

    [SerializeField] private float sageAmount = 0.5f;
    [SerializeField] private enemyMovement enemyMovementRef;

    [SerializeField] private Animator syringeAnim;
    private bool isMedComplete = false;



    private void Update()
    {
        //Debug.Log($"{name} | isFlying:{isFlying} | isInjected:{isInjected}");

        if (isFlying)
        {
            transform.Translate(bulletSpeed * fireDir * Time.deltaTime, Space.World);    
        }
        else if (isReturning)
        {
            ReturnToGun();
        }

        if(isFlying || isReturning || isInjected)
        {
            updateRope();
        }
        else
        {
            ropeLine.enabled = false;
            GetComponent<Collider2D>().enabled = false;

        }

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("syringe triggered with the obj: " + collision.gameObject.name);
        if (!isFlying) return;

        
        switch(collision.gameObject.tag)
        {
            case "Player":
            case "Gun":
                return;
            case "Enemy":
                injectDelay = 1.5f;
                syringeAnim.SetTrigger("Inject");
                enemyMovementRef.GetUnconcious();
                isMedComplete = true;
                break;
            case "Button":
                injectDelay = 0.2f;
                break;
            default:
                injectDelay = 0f;
                break;
        }

        isFlying = false;
        isInjected = true;

        //GetComponent<Collider2D>().enabled = false;
        Invoke(nameof(InjectionCompleted), injectDelay);
    }
    

    private void InjectionCompleted()
    {
        isInjected = false;
        isReturning = true;
        
    }



    private void ReturnToGun()
    {
        transform.position = Vector2.MoveTowards(transform.position, returnTarget.position + returnOffset , bulletSpeed * Time.deltaTime);


        //if syringe reaches the gun, player can attack
        if(Vector2.Distance(transform.position, returnTarget.position + returnOffset) < 0.05f)
        {
            transform.SetParent(returnTarget);
            transform.position = returnTarget.position + returnOffset;
            //transform.localScale = new Vector3(returnTarget.localScale.x, transform.localScale.y);
            transform.localScale = Vector3.one;
            transform.localRotation = Quaternion.identity;



            isReturning = false;
            playerAttackRef.canShoot = true;


            if (isMedComplete)
            {
                syringeAnim.SetTrigger("Reload");
                isMedComplete = false;
            }
            GetComponent<Collider2D>().enabled = false;

        }
    }



    public void Fire(Vector2 startPos, int dir)
    {
        transform.SetParent(null);
        transform.localRotation = returnTarget.rotation;

        isFlying = true;
        isInjected = false;
        isReturning = false;
        

        transform.position = startPos;
        fireDir = returnTarget.right * playerMovementRef.facingDir;


        returnOffset = transform.position - returnTarget.position;

        GetComponent<Collider2D>().enabled = true;
    }


    

    private void updateRope()
    {
        ropeLine.enabled = true;

        currentDistance = Vector2.Distance(ropeSlot.position, transform.position);
        float distanceRatio = currentDistance / maxRopeLenght;
        distanceRatio = Mathf.Clamp01(distanceRatio);
        int activeRopePoints = Mathf.RoundToInt(distanceRatio * maxRopePoints);

        activeRopePoints = Mathf.Clamp(activeRopePoints, 2, maxRopePoints-1);

        ropeLine.positionCount = activeRopePoints;

        for (int i = 0; i < activeRopePoints; i++)
        {
            float t = i / (float)(activeRopePoints - 1);

            Vector3 pointPos = Vector3.Lerp(ropeSlot.position, transform.position, t);

            //for the rope curve(realisic rope like)
            float sag = Mathf.Sin(t * Mathf.PI) * sageAmount;
            pointPos.y -= sag;

            ropeLine.SetPosition(i, pointPos);
        }

        
    }
}