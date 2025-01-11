using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3 : MonoBehaviour
{
    public BulletPoolManager poolManager; // BulletPoolManager 참조
    public Transform player;
    [Header("Pattern1 var")]
    [SerializeField] private Transform[] pattern1BulletSpawner;
    [SerializeField] private float pattern1SpawnInterval;    // 총알 발사 간격 (초 단위)
    [SerializeField] private float pattern1BulletSpeed; // 총알 속도
    [SerializeField] private float pattern1RepeatCount;
    

    [Header("Pattern2 var")]
    [SerializeField] private Transform pattern2BulletSpawner;
    public float pattern2BulletSpeed = 5f; // 탄환 속도
    public int pattern2Rows = 5; // row 개수
    public int pattern2Columns = 6; // column 개수
    public float pattern2SpawnInterval = 0.5f; // 탄환 생성 간격

    public float pattern2VerticalGap;
    public float pattern2RightSideBulletPosition; // right side Pos of crosspattern bullet

    [Header("Pattern3 var")]
    [SerializeField] private Transform pattern3BulletSpawner;
    [SerializeField] private GameObject bigBulletPrefab;
    [SerializeField] private float pattern3SpawnInterval;
    [SerializeField] private int pattern3BulletCount = 30; // 발사할 탄환의 개수
    [SerializeField] private float pattern3SpreadRadius = 1f; // 탄환이 퍼질 거리
    [SerializeField] private float pattern3SmallBulletSpeed = 5f; // 발사된 탄환의 속도
    [SerializeField] private float pattern3BigBulletSpeed = 2f; // BigBullet의 이동 속도
    [SerializeField] private float pattern3RepeatCount;
    // 각도에서 방향 벡터를 계산하는 메서드
    private Vector2 GetDirectionFromAngle(float angle)
    {
        float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
        return new Vector2(dirX, dirY).normalized;
    }
    public IEnumerator Pattern1()
    {
        int shootCount = 0;
        while (shootCount < pattern1RepeatCount)
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
            yield return new WaitForSeconds(pattern1SpawnInterval);
            shootCount++;
        }
    }

    public IEnumerator Pattern2()
    {
        for (int column = 0; column < pattern2Columns; column++) // column 기준 루프
        {
            for (int row = 0; row < pattern2Rows; row++) // row 기준 루프
            {
                // 총알 풀에서 총알 가져오기
                GameObject bullet = poolManager.bulletPool.Get();
                if (bullet == null) yield break;

                Bullet bulletComponent = bullet.GetComponent<Bullet>();
                if (bulletComponent == null) yield break;
                bulletComponent.Initialize(poolManager, pattern2BulletSpeed, 0);

                // 총알 초기화
                if (row % 2 == 1)
                {
                    bullet.transform.position = pattern2BulletSpawner.position + new Vector3(0, -row * pattern2VerticalGap, 0);
                    bullet.transform.rotation = Quaternion.identity;
                    bulletComponent.SetDirection(Vector2.right);
                }
                else
                {
                    //오른쪽에서 생성되어 왼쪽으로 
                    bullet.transform.position = pattern2BulletSpawner.position + new Vector3(pattern2RightSideBulletPosition, -row * pattern2VerticalGap, 0);
                    bullet.transform.rotation = Quaternion.identity;
                    bulletComponent.SetDirection(Vector2.left);
                }
            }
            // 다음 탄환 생성까지 대기
            yield return new WaitForSeconds(pattern2SpawnInterval);
        }
    }

    public IEnumerator Pattern3()
    {
        int shootCount = 0;
        while (shootCount < pattern3RepeatCount)
        {
            // BigBullet 생성
            GameObject bigBullet = Instantiate(bigBulletPrefab, pattern3BulletSpawner.position, Quaternion.identity);

            // 방향 계산 (플레이어 방향)
            Vector2 directionToPlayer = (player.position - pattern3BulletSpawner.position).normalized;

            // BigBullet 초기화
            BigBullet bigBulletComponent = bigBullet.GetComponent<BigBullet>();
            if (bigBulletComponent != null)
            {
                bigBulletComponent.Init(pattern3BulletCount, pattern3SpreadRadius, pattern3SmallBulletSpeed, pattern3BigBulletSpeed, poolManager);
                bigBulletComponent.SetDirection(directionToPlayer); // 방향 설정
            }
            yield return new WaitForSeconds(pattern3SpawnInterval);
            shootCount++;
        }
    }
}
