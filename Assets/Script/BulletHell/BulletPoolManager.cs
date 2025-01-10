using UnityEngine;
using UnityEngine.Pool;

public class BulletPoolManager : MonoBehaviour
{
    public GameObject bulletPrefab; // 탄환 프리팹
    public int defaultCapacity = 20; // 초기 풀 크기
    public int maxCapacity = 100;     // 최대 풀 크기

    public ObjectPool<GameObject> bulletPool; // 오브젝트 풀

    void Awake()
    {
        // 풀 생성
        bulletPool = new ObjectPool<GameObject>(
            CreateBullet,       // 생성 로직
            OnGetBullet,        // 풀에서 가져올 때 호출
            OnReleaseBullet,    // 풀로 반환할 때 호출
            OnDestroyBullet,    // 최대 풀을 초과할 때 삭제
            false,              // 컬렉션 초과 시 삭제 허용 여부
            defaultCapacity,
            maxCapacity
        );
    }

    GameObject CreateBullet()
    {
        // 탄환 생성
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false); // 초기 상태는 비활성화
        return bullet;
    }

    void OnGetBullet(GameObject bullet)
    {
        // 탄환 활성화
        bullet.SetActive(true);
    }

    void OnReleaseBullet(GameObject bullet)
    {
        // 탄환 비활성화
        bullet.SetActive(false);
    }

    void OnDestroyBullet(GameObject bullet)
    {
        // 탄환 삭제
        Destroy(bullet);
    }

    public void ReturnBulletToPool(GameObject bullet)
    {
        bulletPool.Release(bullet); // 풀로 반환
    }
}
