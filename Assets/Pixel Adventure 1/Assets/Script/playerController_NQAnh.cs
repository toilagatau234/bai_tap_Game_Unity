using UnityEngine;

public class playerController_NQAnh : MonoBehaviour
{
    private Rigidbody2D rbody;
    private Animator anim;

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravityMultiplier = 4f;

    private bool jumpPressed = false;
    private float horizontalInput = 0f;
    private bool isCollidingWithObstacle = false;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        
        rbody.gravityScale = gravityMultiplier;
    }

    void Update()
    {
        
        horizontalInput = Input.GetAxisRaw("Horizontal");

        
        anim.SetBool("run", horizontalInput != 0);

        
        if (horizontalInput > 0)
        {
            if (transform.localScale.x < 0) 
                transform.localScale = new Vector3(1, 1, 1);
        }
        else if (horizontalInput < 0)
        {
            if (transform.localScale.x > 0) 
                transform.localScale = new Vector3(-1, 1, 1);
        }


        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
            anim.SetBool("jump", true);
        } else
        {
            anim.SetBool("jump", false);
        }


    }

    void FixedUpdate()
    {
        
        if (!isCollidingWithObstacle)
        {
            rbody.linearVelocity = new Vector2(horizontalInput * moveSpeed, rbody.linearVelocity.y);
        }
        else
        {
            
            rbody.linearVelocity = new Vector2(0, rbody.linearVelocity.y);
        }

        
        rbody.linearVelocity = new Vector2(horizontalInput * moveSpeed, rbody.linearVelocity.y);

        
        if (jumpPressed)
        {
            rbody.linearVelocity = new Vector2(rbody.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }
    }

    
    private void OnCollisionEnter2D_NQAnh(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground_NQAnh"))
        {
            anim.SetBool("jump", false);
            
        }
        else if (collision.gameObject.CompareTag("Obstacle_NQAnh"))
        {
            gameObject.SetActive(false);
            Time.timeScale = 0f;
        }
    }



    private void OnCollisionExit2D_NQAnh(Collision2D collision)
    {
            if (collision.gameObject.CompareTag("Obstacle_NQAnh"))
            {
                isCollidingWithObstacle = false; 
            }
    }
}