using UnityEngine;

public class controller_player : MonoBehaviour
{
    private Rigidbody2D rbody;
    private Animator anim;

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float jumpForce = 16f;
    [SerializeField] private float gravityMultiplier = 4f; // để tăng tốc độ rơi

    private bool jumpPressed = false;
    private float horizontalInput = 0f;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        // Khóa xoay Rigidbody2D để không bị ngã, giữ thẳng đứng
        rbody.freezeRotation = true;

        // Tăng gravityScale cho rơi nhanh hơn, tạo cảm giác rơi thực tế hơn
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
            transform.localScale = new Vector3(1, 1, 1);
        else if (horizontalInput < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Nhấn phím nhảy (space hoặc mũi tên lên)
        // Bỏ giới hạn grounded, nhảy mọi lúc khi nhấn phím mới (GetKeyDown tránh nhảy liên tục khi giữ)
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            jumpPressed = true;
            anim.SetBool("jump", true);
        }
    }

    void FixedUpdate()
    {
        // Di chuyển nhân vật bằng Velocity, đảm bảo vật lý chính xác
        rbody.linearVelocity = new Vector2(horizontalInput * moveSpeed, rbody.linearVelocity.y);

        // Giữ nhân vật không bị xoay bằng cách reset rotation liên tục (phòng trường hợp freezeRotation không đủ)
        if (transform.rotation.eulerAngles.z != 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);

        // Xử lý nhảy
        if (jumpPressed)
        {
            rbody.linearVelocity = new Vector2(rbody.linearVelocity.x, jumpForce);
            jumpPressed = false;
        }
    }

    // Thay đổi logic kiểm tra ground để chỉ dùng animation, có thể bỏ biến isGrounded nếu không cần giới hạn nhảy
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("jump", false);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Xử lý khi chạm vật cản, ví dụ tắt nhân vật và pause game
            gameObject.SetActive(false);
            Time.timeScale = 0f;
        }
    }


}

