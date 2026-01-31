using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpHeight;
    public bool isOnGround;
    Rigidbody2D rb;


    //[SerializeField] private GameSceneManager gameSceneManagerScript;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    private void Update()
    {

        Move();
        Jump();

    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        //flip the player based on right and left movement

        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        

    }

    private void Jump()
    {
        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            isOnGround = false;
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        switch (collision.gameObject.tag)
        {
            case "Ground":
            case "Obj":
                isOnGround = true; 
                break;
            default:
                break;
        }
    }


    //public bool CanAttack()
    //{
    //    return isOnGround;
    //}

}
