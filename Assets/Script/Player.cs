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
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer ��������
    }

    void Update()
    {
        // �̵� �Է� ����
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        MoveSpriteManager();

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
