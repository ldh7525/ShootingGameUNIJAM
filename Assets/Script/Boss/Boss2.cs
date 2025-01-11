using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : MonoBehaviour
{
    public BulletPoolManager poolManager; // BulletPoolManager ����
    public Transform player;
    [Header("Pattern1 var")]
    [SerializeField] private Transform[] pattern1BulletSpawner;
    [SerializeField] private int pattern1BulletCount;          // number of bullets in one shot
    [SerializeField] private float pattern1SpawnInterval;    // number of seconds between shots
    [SerializeField] private float pattern1BulletSpeed; // speed of bullets
    [SerializeField] private float pattern1AngleStepHalf; // angle between bullets
    //[Header("Pattern2 var")]
    //[Header("Pattern3 var")]

    private Vector2 GetDirectionFromAngle(float angle)
    {
        float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(dirX, dirY).normalized;
    }
    public IEnumerator Pattern1()
    {
        // Calculate base direction towards the player
        Vector2 direction = player.position - transform.position;
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Spread bullets in an even arc
        float startAngle = baseAngle - pattern1AngleStepHalf;
        float angleIncrement = (pattern1AngleStepHalf * 2) / (pattern1BulletCount - 1);

        for (int i = 0; i < pattern1BulletCount; i++)
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
                bulletComponent.Initialize(poolManager, pattern1BulletSpeed, 0); // �Ѿ� ���� �ʱ�ȭ
                int angleOffset = i - pattern1BulletCount / 2;  // bulletCount is odd number
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
}
