using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f; // 탄환 속도
    private Vector2 direction;
    private BulletPoolManager poolManager;

    public void Initialize(BulletPoolManager manager)
    {
        poolManager = manager; // PoolManager 참조 설정
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized; // 이동 방향 설정
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // 화면 밖으로 나가면 풀로 반환
        if (Mathf.Abs(transform.position.x) > 20 || Mathf.Abs(transform.position.y) > 20)
        {
            print("asdf");
            poolManager.ReturnBulletToPool(gameObject);
        }
    }
}
