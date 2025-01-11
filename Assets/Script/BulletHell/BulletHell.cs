using UnityEngine;
using System.Collections;

public class BulletHell : MonoBehaviour
{
    public BulletPoolManager poolManager; // BulletPoolManager 참조
    public int bulletCount;          // 한 번에 발사할 총알 수
    public float spawnInterval;    // 탄막 전체 발사 간격
    public float bulletSpawnDelay; // 각 총알 발사 간격

    void Start()
    {
        // 일정 시간 간격으로 탄막 발사 시작
        InvokeRepeating(nameof(StartCircularPattern), 0f, spawnInterval);
    }

    void StartCircularPattern()
    {
        StartCoroutine(FireCircularPattern());
    }

    IEnumerator FireCircularPattern()
    {
        float angleStep = 360f / bulletCount; // 각 총알 사이의 각도
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            // 풀에서 총알 가져오기
            GameObject bullet = poolManager.bulletPool.Get();
            if (bullet == null)
            {
                Debug.LogWarning("탄환을 풀에서 가져올 수 없습니다.");
                continue;
            }

            // 총알 초기화 (BulletPoolManager를 전달)
            Bullet bulletComponent = bullet.GetComponent<Bullet>();
            if (bulletComponent != null)
            {
                bulletComponent.Initialize(poolManager);
                bulletComponent.SetDirection(GetDirectionFromAngle(angle)); // ?
            }
            else
            {
                Debug.LogWarning("Bullet 스크립트가 총알에 첨부되어 있지 않습니다.");
            }

            // 총알 위치와 방향 설정
            bullet.transform.position = transform.position;
            bullet.transform.rotation = Quaternion.identity; // 초기화

            // 다음 각도로 이동
            angle += angleStep;

            // 다음 총알 발사까지 대기
            yield return new WaitForSeconds(bulletSpawnDelay);
        }
    }

    // 각도로부터 방향 벡터를 계산하는 메서드
    private Vector2 GetDirectionFromAngle(float angle)
    {
        float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(dirX, dirY).normalized;
    }
}
