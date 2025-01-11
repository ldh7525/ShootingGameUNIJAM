using UnityEngine;
using UnityEngine.EventSystems;

public class BigBullet : MonoBehaviour
{
    private int bulletCount = 30; // �߻��� źȯ�� ����
    private float spreadRadius = 1f; // źȯ�� ���� �Ÿ�
    private float smallBulletSpeed = 5f; // �߻�� źȯ�� �ӵ�
    private float bigBulletSpeed = 2f; // BigBullet�� �̵� �ӵ�
    private BulletPoolManager poolManager; // BulletPoolManager ����
    private Vector2 moveDirection;

    private void Update()
    {
        // BigBullet move
        transform.Translate(moveDirection * bigBulletSpeed * Time.deltaTime);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        // Ư�� Trigger Collider�� �������� ó��
        if (other.CompareTag("BigBulletTrigger")) // Trigger Zone�� �±� ����
        {
            HandleTriggerExit();
        }
    }

    private void HandleTriggerExit()
    {
        Debug.Log($"Bullet {name} exited the trigger zone!");
        SpreadBullets(); // źȯ �߻�
        Destroy(gameObject); // BigBullet Ǯ�� ��ȯ 
    }

    void SpreadBullets()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            // �� źȯ�� �߻� ���� ���
            float angle = 360f / bulletCount * i;
            float radian = angle * Mathf.Deg2Rad;

            // źȯ�� ��ġ ��� (BigBullet�� �ֺ�)
            Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0) * spreadRadius;

            // ������Ʈ Ǯ���� źȯ ��������
            GameObject bullet = poolManager.bulletPool.Get();
            bullet.transform.position = spawnPosition;
            bullet.transform.rotation = Quaternion.identity;

            // źȯ�� ���� ����
            Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;

            // źȯ �ʱ�ȭ
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                bulletComponent.SetDirection(direction);
                bulletComponent.Initialize(poolManager, smallBulletSpeed, 0); // �ӵ� �� ��� ����
            }
        }
    }

    public void Init(int bulletCount, float spreadRadius, float smallBulletSpeed, float bigBulletSpeed, BulletPoolManager poolManager)
    {
        this.bulletCount = bulletCount;
        this.spreadRadius = spreadRadius;
        this.smallBulletSpeed = smallBulletSpeed;
        this.bigBulletSpeed = bigBulletSpeed;
        this.poolManager = poolManager;
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
    }
}
