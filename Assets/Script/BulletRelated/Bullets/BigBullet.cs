using UnityEngine;

public class BigBullet : MonoBehaviour
{
    public GameObject bulletPrefab; // �߻��� źȯ ������
    public int bulletCount = 30; // �߻��� źȯ�� ����
    public float spreadRadius = 1f; // źȯ�� ���� �Ÿ�
    public float smallBulletSpeed = 5f; // �߻�� źȯ�� �ӵ�
    public float bigBulletSpeed = 2f; // BigBullet�� �̵� �ӵ�
    public BulletPoolManager poolManager; // BulletPoolManager ����

    private void Update()
    {
        // BigBullet move
        transform.Translate(Vector2.right * bigBulletSpeed * Time.deltaTime);
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
}
