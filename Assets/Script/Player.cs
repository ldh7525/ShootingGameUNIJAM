using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;
    private Animator ani;
    [SerializeField] private Vector2 movement;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer 가져오기
    }

    void Update()
    {
        // 이동 입력 감지
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        MoveSpriteManager();

        // 스프라이트 뒤집기
        if (movement.x > 0)
            spriteRenderer.flipX = false; // 오른쪽으로 이동
        else if (movement.x < 0)
            spriteRenderer.flipX = true;  // 왼쪽으로 이동
    }

    void FixedUpdate()
    {
        // 물리 기반 이동 처리
        rb.velocity = movement.normalized * moveSpeed;
    }

    void MoveSpriteManager()
    {

        if (movement.x != 0 || movement.y != 0)
            ani.SetBool("IsWalking", true);
        else
            ani.SetBool("IsWalking", false);

        if (movement.x != 0)
            ani.SetBool("Isleft", true);
        else
            ani.SetBool("Isleft", false);

        if (movement.y > 0)
            ani.SetBool("IsForward", true);
        else if (movement.y <= 0)
            ani.SetBool("IsForward", false);

        if (movement.y < 0)
            ani.SetBool("IsBackward", true);
        else if (movement.y >= 0)
            ani.SetBool("IsBackward", false);
    }
}
