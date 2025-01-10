using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer 가져오기
    }

    void Update()
    {
        // 이동 입력 감지
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

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
}
