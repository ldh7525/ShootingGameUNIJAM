using UnityEngine;
using System.Collections;

public class BulletOneSin : MonoBehaviour
{
    public BulletPoolManager poolManager; // BulletPoolManager 참조
    public float spawnInterval;    // 총알 발사 간격 (초 단위)
    public float speed; // 총알 속도
    public Transform player; // 플레이어 위치 참조

    void Start()
    {
        // 일정 시간 간격으로 총알 발사 패턴 실행
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

        // 풀에서 총알 가져오기
        GameObject bullet = poolManager.bulletPool.Get();
        if (bullet == null)
        {
            Debug.LogWarning("총알 풀에서 총알을 가져올 수 없습니다.");
            yield return null;
        }

        // 총알 초기화 (BulletPoolManager에서 설정)
        Bullet bulletComponent = bullet.GetComponent<Bullet>();
        if (bulletComponent != null)
        {
            bulletComponent.Initialize(poolManager, speed, 1); // 총알 속도 초기화
            bulletComponent.SetDirection(GetDirectionFromAngle(angle)); // 방향 설정
        }
        else
        {
            Debug.LogWarning("Bullet 스크립트가 총알에 연결되지 않았습니다.");
        }

        // 총알 위치와 회전 초기화
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;
        bulletComponent.GetPosition(transform.position); // 현재 위치 전달
        bulletComponent.GetTime(Time.time); // 현재 시간 전달

        yield return null;
    }

    // 각도에서 방향 벡터를 계산하는 메서드
    private Vector2 GetDirectionFromAngle(float angle)
    {
        float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(dirX, dirY).normalized;
    }
}
