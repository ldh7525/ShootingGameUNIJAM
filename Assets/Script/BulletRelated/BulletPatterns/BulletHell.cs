using UnityEngine;
using System.Collections;

public class BulletHell : MonoBehaviour
{
    public BulletPoolManager poolManager; // BulletPoolManager ����
    public int bulletCount;          // �� ���� �߻��� �Ѿ� ��
    public float spawnInterval;    // ź�� ��ü �߻� ����
    public float bulletSpawnDelay; // �� �Ѿ� �߻� ����

    void Start()
    {
        // ���� �ð� �������� ź�� �߻� ����
        InvokeRepeating(nameof(StartCircularPattern), 0f, spawnInterval);
    }

    void StartCircularPattern()
    {
        StartCoroutine(FireCircularPattern());
    }

    IEnumerator FireCircularPattern()
    {
        float angleStep = 360f / bulletCount; // �� �Ѿ� ������ ����
        float angle = 0f;

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
                bulletComponent.Initialize(poolManager, 5f, 0); // �Ѿ� ���� �ʱ�ȭ
                bulletComponent.SetDirection(GetDirectionFromAngle(angle)); // ?
            }
            else
            {
                Debug.LogWarning("Bullet ��ũ��Ʈ�� �Ѿ˿� ÷�εǾ� ���� �ʽ��ϴ�.");
            }

            // �Ѿ� ��ġ�� ���� ����
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity; // �ʱ�ȭ

            // ���� ������ �̵�
            angle += angleStep;

            // ���� �Ѿ� �߻���� ���
            yield return new WaitForSeconds(bulletSpawnDelay);
        }
    }

    // �����κ��� ���� ���͸� ����ϴ� �޼���
    private Vector2 GetDirectionFromAngle(float angle)
    {
        float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(dirX, dirY).normalized;
    }
}
