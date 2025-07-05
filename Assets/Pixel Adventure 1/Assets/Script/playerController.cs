using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rbody;
    private Animator anim;

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravityMultiplier = 4f;

    private bool jumpPressed = false;
    private float horizontalInput = 0f;
    private bool isCollidingWithObstacle = false; // Biến trạng thái va chạm

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Tăng gravityScale cho rơi nhanh hơn
        rbody.gravityScale = gravityMultiplier;
    }

    void Update()
    {
        // Lấy input theo trục ngang (phím trái/phải)
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Gán animation chạy dựa theo input
        anim.SetBool("run", horizontalInput != 0);

        // Đảo mặt nhân vật theo chiều di chuyển
        if (horizontalInput > 0)
        {
            if (transform.localScale.x < 0) 
                transform.localScale = new Vector3(3, 3, 3);
        }
        else if (horizontalInput < 0)
        {
            if (transform.localScale.x > 0) 
                transform.localScale = new Vector3(-3, 3, 3);
        }

         // Bỏ giới hạn grounded, nhảy mọi lúc khi nhấn phím mới (GetKeyDown tránh nhảy liên tục khi giữ)
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
            anim.SetBool("jump", true);
            FindObjectOfType<SoundManager>().PlayJump();
        }

        //xử lý animtor khi rơi
        if (rbody.linearVelocity.y > 0.1f)
        {
            anim.SetBool("jump", true);
            anim.SetBool("jump__fall", true);
            anim.SetBool("fall", false);
        }
        else if (rbody.linearVelocity.y < -0.1f)
        {
            anim.SetBool("jump", false);
            anim.SetBool("jump__fall", true);

            anim.SetBool("fall", true);
        }

        else
        {
            // Khi gần đứng yên theo trục y, không nhảy hoặc rơi
            anim.SetBool("jump", false);
            anim.SetBool("fall", false);
        }

    }

    void FixedUpdate()
    {
        // Nếu không va chạm với vật cản, cho phép di chuyển
        if (!isCollidingWithObstacle)
        {
            rbody.linearVelocity = new Vector2(horizontalInput * moveSpeed, rbody.linearVelocity.y);
        }
        else
        {
            // Nếu va chạm với vật cản, đặt velocity về 
            rbody.linearVelocity = new Vector2(0, rbody.linearVelocity.y);
        }

        // Di chuyển nhân vật
        rbody.linearVelocity = new Vector2(horizontalInput * moveSpeed, rbody.linearVelocity.y);

        /*// Giữ nhân vật không bị xoay bằng cách reset rotation liên tục (phòng trường hợp freezeRotation không đủ)
        if (transform.rotation.eulerAngles.z != 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);*/

        // Xử lý nhảy
        if (jumpPressed)
        {
            rbody.linearVelocity = new Vector2(rbody.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }
    }

    // Thay đổi logic kiểm tra ground để chỉ dùng animation
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("jump", false);
            anim.SetBool("fall", false);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameObject.SetActive(false);
            GameController.instance.GameOver();
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            gameObject.SetActive(false);
            GameController.instance.GameOver();
            Time.timeScale = 0;
        }
    }



    private void OnCollisionExit2D(Collision2D collision)
    {
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                isCollidingWithObstacle = false; // Đánh dấu không còn va chạm với vật cản
            }
    }
}