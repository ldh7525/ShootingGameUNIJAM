using UnityEngine;
using System.Collections;

public class BulletShotEven : MonoBehaviour
{
    public BulletPoolManager poolManager; // BulletPoolManager ����
    public int bulletCount;          // number of bullets in one shot
    public float spawnInterval;    // number of seconds between shots
    public float speed; // speed of bullets
    public Transform player; 
    public float angleStepHalf; // angle between bullets

    void Start()
    {
        // ���� �ð� �������� ź�� �߻� ����
        InvokeRepeating(nameof(StartPattern), 0f, spawnInterval);
    }

    void StartPattern()
    {
        StartCoroutine(FireShotgunPattern());
    }

    IEnumerator FireShotgunPattern()
    {
        // Calculate base direction towards the player
        Vector2 direction = player.position - transform.position;
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Spread bullets in an even arc
        float startAngle = baseAngle - angleStepHalf;
        float angleIncrement = (angleStepHalf * 2) / (bulletCount - 1);

        for (int i = 0; i < bulletCount; i++)
        {
            // Ǯ���� �Ѿ� ��������
            GameObject bullet = poolManager.bulletPool.Get();
            if (bullet == null)
            {
                Debug.LogWarning("źȯ�� Ǯ���� ������ �� �����ϴ�.");
                continue;
            }

            // �Ѿ� �ʱ�ȭ (BulletPoolManager�� ����)
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                bulletComponent.Initialize(poolManager, speed, 0); // �Ѿ� ���� �ʱ�ȭ
                int angleOffset = i - bulletCount / 2;  // bulletCount is odd number
                bulletComponent.SetDirection(GetDirectionFromAngle(startAngle + (i * angleIncrement))); // ?
            }
            else
            {
                Debug.LogWarning("Bullet ��ũ��Ʈ�� �Ѿ˿� ÷�εǾ� ���� �ʽ��ϴ�.");
            }

            // �Ѿ� ��ġ�� ���� ����
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity; // �ʱ�ȭ

            // ���� ������ �̵�
            // angle += angleStep;

            // ���� �Ѿ� �߻���� ���
           // yield return new WaitForSeconds(bulletSpawnDelay);
            
        }
        yield return null;
    }

    // �����κ��� ���� ���͸� ����ϴ� �޼���
    private Vector2 GetDirectionFromAngle(float angle)
    {
        float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(dirX, dirY).normalized;
    }
}
