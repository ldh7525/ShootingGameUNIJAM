using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public BulletPoolManager poolManager; // BulletPoolManager ����
    public Transform player;

    
    public Transform[] pattern3BulletSpawner;

    [Header("Pattern1 var")]
    
    public Transform[] pattern1BulletSpawner;
    public float pattern1SpawnInterval;    // number of seconds between shots
    public float pattern1BulletSpeed; // speed of bullets
    public float repeatCount;

    [Header("Pattern2 var")]
    public Transform[] pattern2BulletSpawner;
    public int pattern2BulletCount;          // number of bullets in one shot
    public float pattern2SpawnInterval;    // number of seconds between shots
    public float pattern2BulletSpeed; // speed of bullets
    public float pattern2AngleStep; // angle between bullets

    [Header("Pattern3 var")]
    public int pattern3BulletCount;          // number of bullets in one shot
    public float pattern3SpawnInterval;    // number of seconds between shots
    public float pattern3BulletSpeed; // speed of bullets
    public float pattern3Hight;
    public float pattern3Width;
    void Start()
    {
       
    }
    private Vector2 GetDirectionFromAngle(float angle)
    {
        float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(dirX, dirY).normalized;
    }
    public IEnumerator Pattern1()
    {
        int shootCount = 0;
        while(shootCount < repeatCount)
        {
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            GameObject bullet = poolManager.bulletPool.Get();
            if (bullet == null)
            {
                Debug.LogWarning("źȯ�� Ǯ���� ������ �� �����ϴ�.");
                yield return null;
            }

            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                bulletComponent.Initialize(poolManager, pattern1BulletSpeed, 0);
                bulletComponent.SetDirection(GetDirectionFromAngle(angle)); // ?
            }
            else
            {
                Debug.LogWarning("Bullet ��ũ��Ʈ�� �Ѿ˿� ÷�εǾ� ���� �ʽ��ϴ�.");
            }

            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            yield return new WaitForSeconds(pattern1SpawnInterval);
            shootCount++;
        }
    }

    public IEnumerator Pattern2()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        int shootCount = 0;
        while (shootCount < repeatCount)
        {
            for (int i = 0; i < pattern2BulletCount; i++)
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
                    bulletComponent.Initialize(poolManager, pattern2BulletSpeed, 0); // �Ѿ� ���� �ʱ�ȭ
                    int angleOffset = i - pattern2BulletCount / 2;  // bulletCount is odd number
                    bulletComponent.SetDirection(GetDirectionFromAngle(angle + pattern2AngleStep * angleOffset)); // ?
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
                yield return null;
            }
            yield return new WaitForSeconds(pattern2SpawnInterval);
            shootCount++;
        }
    }
}
