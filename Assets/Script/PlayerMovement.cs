using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("스텟")]
    [SerializeField] private Vector2 movement;
    public int playerHealth;

    [Header("달리기")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float currentSpeed;
    [SerializeField] public float stamina;
    [SerializeField] private float maxStamina;

    [Header("필수 항목")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator ani;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("대쉬 (작동 안함)")]
    [SerializeField] private float boostMultiplier = 2f;
    [SerializeField] private float boostDuration = 0.5f;
    private bool isBoosting = false;

    [Header("걸을 수 있는 바닥")]
    [SerializeField] private LayerMask walkableLayer;
    private bool canMove = false;

    [Header("무적 상태 설정")]
    [SerializeField] private int invincibleTime; // 무적 시간
    private bool isInvincible = false; // 무적 상태 플래그

    [SerializeField] private CameraShakeOnHit hit;

    void Start()
    {
        hit = GetComponent<CameraShakeOnHit>();
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = moveSpeed;
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Stamina());
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        MoveSpriteManager();
        Boost();
        //Run();

        if (Time.timeScale != 0)
        {
            if (movement.x > 0)
                spriteRenderer.flipX = false;
            else if (movement.x < 0)
                spriteRenderer.flipX = true;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = movement.normalized * currentSpeed * Time.deltaTime * 50;
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
            if (stamina < 0)
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
        while (true)
        {
            if (stamina < maxStamina)
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

    IEnumerator BecomeInvincible()
    {
        isInvincible = true; // 무적 상태 활성화
        float elapsedTime = 0f;

        while (elapsedTime < invincibleTime)
        {
            // alpha 값을 점진적으로 감소(1 -> 0.5)
            yield return StartCoroutine(FadeAlpha(1f, 0.2f, 0.5f));

            // alpha 값을 점진적으로 증가(0.5 -> 1)
            yield return StartCoroutine(FadeAlpha(0.2f, 1f, 0.5f));

            // 한 번의 깜빡임에 총 1초 소요
            elapsedTime += 1f;
        }

        // 무적 상태 해제 (alpha 값을 1로 복구)
        SetSpriteAlpha(1f);
        isInvincible = false; // 무적 상태 비활성화
    }

    IEnumerator FadeAlpha(float startAlpha, float endAlpha, float duration)
    {
        float timer = 0f;
        Color spriteColor = spriteRenderer.color;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            spriteColor.a = Mathf.Lerp(startAlpha, endAlpha, t); // alpha 값을 선형 보간
            spriteRenderer.color = spriteColor;
            yield return null; // 다음 프레임까지 대기
        }

        // 정확히 목표 alpha 값으로 설정
        spriteColor.a = endAlpha;
        spriteRenderer.color = spriteColor;
    }

    void SetSpriteAlpha(float alpha)
    {
        Color spriteColor = spriteRenderer.color;
        spriteColor.a = alpha;
        spriteRenderer.color = spriteColor;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet") && !isInvincible) // 무적 상태가 아닐 때만 피해를 받음
        {
            playerHealth--;
            StartCoroutine(BecomeInvincible()); // 무적 상태 활성화
            Destroy(collision.gameObject);
            hit.TriggerShake();
        }
    }
}
