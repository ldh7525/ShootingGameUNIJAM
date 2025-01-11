using UnityEngine;
using System.Collections;

public class BulletOne : MonoBehaviour
{
    public BulletPoolManager poolManager; // BulletPoolManager ����
    public float spawnInterval;    // number of seconds between shots
    public float speed; // speed of bullets
    public Transform player;

    void Start()
    {
        // ���� �ð� �������� ź�� �߻� ����
        InvokeRepeating(nameof(StartPattern), 0f, spawnInterval);
    }

    void StartPattern()
    {
        StartCoroutine(FirePattern());
    }

    IEnumerator FirePattern()
    {
        Vector2 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Ǯ���� �Ѿ� ��������
        GameObject bullet = poolManager.bulletPool.Get();
        if (bullet == null)
        {
            Debug.LogWarning("źȯ�� Ǯ���� ������ �� �����ϴ�.");
            yield return null;
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
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

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
