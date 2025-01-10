using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f; // źȯ �ӵ�
    [SerializeField] private Vector2 direction;
    [SerializeField] private BulletPoolManager poolManager;

    public void Initialize(BulletPoolManager manager)
    {
        poolManager = manager; // PoolManager ���� ����
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized; // �̵� ���� ����
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        // ȭ�� ������ ������ Ǯ�� ��ȯ
        if (Mathf.Abs(transform.position.x) > 20 || Mathf.Abs(transform.position.y) > 20)
        {
            poolManager.ReturnBulletToPool(gameObject);
        }
    }
}
