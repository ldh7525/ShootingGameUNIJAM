using UnityEngine;
using UnityEngine.Pool;

public class BulletPoolManager : MonoBehaviour
{
    public GameObject bulletPrefab; // źȯ ������
    public int defaultCapacity = 20; // �ʱ� Ǯ ũ��
    public int maxCapacity = 100;     // �ִ� Ǯ ũ��

    public ObjectPool<GameObject> bulletPool; // ������Ʈ Ǯ

    void Awake()
    {
        // Ǯ ����
        bulletPool = new ObjectPool<GameObject>(
            CreateBullet,       // ���� ����
            OnGetBullet,        // Ǯ���� ������ �� ȣ��
            OnReleaseBullet,    // Ǯ�� ��ȯ�� �� ȣ��
            OnDestroyBullet,    // �ִ� Ǯ�� �ʰ��� �� ����
            false,              // �÷��� �ʰ� �� ���� ��� ����
            defaultCapacity,
            maxCapacity
        );
    }

    GameObject CreateBullet()
    {
        // źȯ ����
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false); // �ʱ� ���´� ��Ȱ��ȭ
        return bullet;
    }

    void OnGetBullet(GameObject bullet)
    {
        // źȯ Ȱ��ȭ
        bullet.SetActive(true);
    }

    void OnReleaseBullet(GameObject bullet)
    {
        // źȯ ��Ȱ��ȭ
        bullet.SetActive(false);
    }

    void OnDestroyBullet(GameObject bullet)
    {
        // źȯ ����
        Destroy(bullet);
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bulletPool.Release(bullet); // Ǯ�� ��ȯ
    }
}
