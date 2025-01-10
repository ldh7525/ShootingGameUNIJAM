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
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer ��������
    }

    void Update()
    {
        // �̵� �Է� ����
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // ��������Ʈ ������
        if (movement.x > 0)
            spriteRenderer.flipX = false; // ���������� �̵�
        else if (movement.x < 0)
            spriteRenderer.flipX = true;  // �������� �̵�
    }

    void FixedUpdate()
    {
        // ���� ��� �̵� ó��
        rb.velocity = movement.normalized * moveSpeed;
    }
}
