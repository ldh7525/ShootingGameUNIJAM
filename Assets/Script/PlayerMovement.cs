using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header ("스텟")]
    [SerializeField] private Vector2 movement;
    public int playerHealth;

    [Header ("달리기")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] public float stamina;
    [SerializeField] private float maxStamina;

    [Header ("필수 항목")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator ani;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header ("대쉬 (작동 안함)")]
    [SerializeField] private float boostMultiplier = 2f;  // Boost multiplier
    [SerializeField] private float boostDuration = 0.5f;  // Boost duration
    private bool isBoosting = false;

    [Header ("걸을 수 있는 바닥")]
    [SerializeField] private LayerMask walkableLayer;  // Layer for walkable objects
    private bool canMove = false;

    void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = moveSpeed;
        spriteRenderer = GetComponent<SpriteRenderer>(); // SpriteRenderer component
        StartCoroutine(Stamina());
    }

    void Update()
    {
        if (IsOnWalkableSurface())
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            MoveSpriteManager();
            Boost();
            //Run();

            if (Time.timeScale != 0)
            {
                if (movement.x > 0)
                    spriteRenderer.flipX = false; // Facing right
                else if (movement.x < 0)
                    spriteRenderer.flipX = true;  // Facing left
            }
        }
        else
        {
            movement = Vector2.zero;
            transform.position = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement.normalized * currentSpeed * Time.deltaTime * 50;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            playerHealth--;
            Destroy(collision.gameObject);
        }
    }

    void MoveSpriteManager()
    {
        ani.SetBool("IsWalking", movement.x != 0 || movement.y != 0);
        ani.SetBool("Isleft", movement.x != 0);
        ani.SetBool("IsForward", movement.y > 0);
        ani.SetBool("IsBackward", movement.y < 0);
    }

    void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            currentSpeed = sprintSpeed;
            stamina = stamina - Time.deltaTime * 10;
            if(stamina < 0)
            {
                currentSpeed = moveSpeed;
            }
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            currentSpeed = moveSpeed;
        }
    }
    void Boost()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isBoosting && movement != Vector2.zero && stamina > 20)
        {
            stamina -= 20;
            StartCoroutine(ApplyBoost());
        }
    }
    IEnumerator Stamina()
    {
        while(true)
        {
            if(stamina < maxStamina)
            {
                stamina = stamina + Time.deltaTime * 2;
            }
            yield return null;
        }
    }
    IEnumerator ApplyBoost()
    {
        isBoosting = true;
        float originalSpeed = currentSpeed;
        currentSpeed *= boostMultiplier;

        yield return new WaitForSeconds(boostDuration);

        currentSpeed = originalSpeed;
        isBoosting = false;
    }
    bool IsOnWalkableSurface()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f, walkableLayer);
        return hit.collider != null;
    }
}
