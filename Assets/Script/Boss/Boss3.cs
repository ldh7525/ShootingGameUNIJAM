using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : MonoBehaviour
{
    public BulletPoolManager poolManager; // BulletPoolManager ����
    public Transform player;
    [Header("Pattern1 var")]
    public float pattern1SpawnInterval;    // 총알 발사 간격 (초 단위)
    public float pattern1BulletSpeed; // 총알 속도


    // 각도에서 방향 벡터를 계산하는 메서드
    private Vector2 GetDirectionFromAngle(float angle)
    {
        float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(dirX, dirY).normalized;
    }
    IEnumerator Pattern1()
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
            bulletComponent.Initialize(poolManager, pattern1BulletSpeed, 1); // 총알 속도 초기화
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

    
}
