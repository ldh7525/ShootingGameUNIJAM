using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    public BulletPoolManager poolManager; // BulletPoolManager ����
    public Transform player;

    [Header("Pattern1 var")]
    
    [SerializeField] private Transform[] pattern1BulletSpawner;
    [SerializeField] private float pattern1SpawnInterval;    // number of seconds between shots
    [SerializeField] private float pattern1BulletSpeed; // speed of bullets
    [SerializeField] private float pattern1RepeatCount;

    [Header("Pattern2 var")]
    [SerializeField] private Transform[] pattern2BulletSpawner;
    [SerializeField] private int pattern2BulletCount;          // number of bullets in one shot
    [SerializeField] private float pattern2SpawnInterval;    // number of seconds between shots
    [SerializeField] private float pattern2BulletSpeed; // speed of bullets
    [SerializeField] private float pattern2AngleStep; // angle between bullets
    [SerializeField] private float pattern2RepeatCount;


    [Header("Pattern3 var")]
    [SerializeField] private Transform[] pattern3BulletSpawner;
    [SerializeField] private int pattern3BulletCount;          // number of bullets in one shot
    [SerializeField] private float pattern3SpawnInterval;    // number of seconds between shots
    [SerializeField] private float pattern3BulletSpeed; // speed of bullets
    [SerializeField] private float pattern3Height;
    [SerializeField] private float pattern3Width;
    [SerializeField] private float pattern3RepeatCount;

    [Header("Pattern4 var")]

    [SerializeField] private Transform[] pattern4BulletSpawner;
    [SerializeField] private float pattern4SpawnInterval;    // number of seconds between shots
    [SerializeField] private float pattern4BulletSpeed; // speed of bullets
    [SerializeField] private float pattern4RepeatCount;

    [Tooltip("샷건 너프 버전")]
    [Header("Pattern5 var")]
    [SerializeField] private Transform[] pattern5BulletSpawner;
    [SerializeField] private int pattern5BulletCount;          // number of bullets in one shot
    [SerializeField] private float pattern5SpawnInterval;    // number of seconds between shots
    [SerializeField] private float pattern5BulletSpeed; // speed of bullets
    [SerializeField] private float pattern5AngleStep; // angle between bullets
    [SerializeField] private float pattern5RepeatCount;
    private Vector2 GetDirectionFromAngle(float angle)
    {
        float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(dirX, dirY).normalized;
    }
    public IEnumerator Pattern1()
    {
        int shootCount = 0;
        while(shootCount < pattern1RepeatCount)
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
        

        int shootCount = 0;
        while (shootCount < pattern2RepeatCount)
        {
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
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

    public IEnumerator Pattern3()
    {
        int shootCount = 0;
        while (shootCount < pattern3RepeatCount)
        {
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            for (int i = 0; i < pattern3BulletCount; i++)
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
                    bulletComponent.Initialize(poolManager, pattern3BulletSpeed, 0); // �Ѿ� ���� �ʱ�ȭ
                    bulletComponent.SetDirection(GetDirectionFromAngle(angle)); // ?
                }
                else
                {
                    Debug.LogWarning("Bullet ��ũ��Ʈ�� �Ѿ˿� ÷�εǾ� ���� �ʽ��ϴ�.");
                }

                // �Ѿ� ��ġ�� ���� ����
                int offset = i - pattern3BulletCount / 2;
                int offset_abs = -Mathf.Abs(offset);
                Vector3 normDir = new Vector3(direction.x, direction.y, 0).normalized;
                Vector3 perpDir = new Vector3(-direction.y, direction.x, 0).normalized;
                bullet.transform.position = transform.position + (perpDir * offset * pattern3Width) + (normDir * offset_abs * pattern3Height);
                bullet.transform.rotation = Quaternion.identity; // �ʱ�ȭ

                // ���� ������ �̵�
                // angle += angleStep;

                // ���� �Ѿ� �߻���� ���
                // yield return new WaitForSeconds(bulletSpawnDelay);

            }
            yield return new WaitForSeconds(pattern3SpawnInterval);
            shootCount++;
        }
    }

    public IEnumerator Pattern4()
    {
        int shootCount = 0;
        while (shootCount < pattern4RepeatCount)
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
                bulletComponent.Initialize(poolManager, pattern4BulletSpeed, 0);
                bulletComponent.SetDirection(GetDirectionFromAngle(angle)); // ?
            }
            else
            {
                Debug.LogWarning("Bullet ��ũ��Ʈ�� �Ѿ˿� ÷�εǾ� ���� �ʽ��ϴ�.");
            }

            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity;

            yield return new WaitForSeconds(pattern4SpawnInterval);
            shootCount++;
        }
    }
    public IEnumerator Pattern5()
    {


        int shootCount = 0;
        while (shootCount < pattern5RepeatCount)
        {
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            for (int i = 0; i < pattern5BulletCount; i++)
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
                    bulletComponent.Initialize(poolManager, pattern5BulletSpeed, 0); // �Ѿ� ���� �ʱ�ȭ
                    int angleOffset = i - pattern5BulletCount / 2;  // bulletCount is odd number
                    bulletComponent.SetDirection(GetDirectionFromAngle(angle + pattern5AngleStep * angleOffset)); // ?
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
            yield return new WaitForSeconds(pattern5SpawnInterval);
            shootCount++;
        }
    }
}
