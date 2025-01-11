using UnityEngine;
using System.Collections;

public class BulletVform : MonoBehaviour
{
    public BulletPoolManager poolManager; // BulletPoolManager ����
    public int bulletCount;          // number of bullets in one shot
    public float spawnInterval;    // number of seconds between shots
    public float speed; // speed of bullets
    public Transform player; 
    public float height;
    public float wight;

    void Start()
    {
        // ���� �ð� �������� ź�� �߻� ����
        InvokeRepeating(nameof(StartPattern), 0f, spawnInterval);
    }

    void StartPattern()
    {
        StartCoroutine(FireVformPattern());
    }

    IEnumerator FireVformPattern()
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
                bulletComponent.SetDirection(GetDirectionFromAngle(angle)); // ?
            }
            else
            {
                Debug.LogWarning("Bullet ��ũ��Ʈ�� �Ѿ˿� ÷�εǾ� ���� �ʽ��ϴ�.");
            }

            // �Ѿ� ��ġ�� ���� ����
            int offset = i - bulletCount / 2;
            int offset_abs = - Mathf.Abs(offset);
            Vector3 normDir = new Vector3(direction.x, direction.y, 0).normalized;
            Vector3 perpDir = new Vector3(-direction.y, direction.x, 0).normalized;
            bullet.transform.position = transform.position + (perpDir * offset * wight) + (normDir * offset_abs * height);
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
