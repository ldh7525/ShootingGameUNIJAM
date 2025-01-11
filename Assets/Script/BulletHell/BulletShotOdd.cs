using UnityEngine;
using System.Collections;

public class BulletShotOdd : MonoBehaviour
{
    public BulletPoolManager poolManager; // BulletPoolManager ����
    public int bulletCount;          // number of bullets in one shot
    public float spawnInterval;    // number of seconds between shots
    public float speed; // speed of bullets
    public Transform player; 
    public float angleStep; // angle between bullets

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
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        

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
                bulletComponent.SetDirection(GetDirectionFromAngle(angle + angleStep * angleOffset)); // ?
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
