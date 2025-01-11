using UnityEngine;
using System.Collections;
using UnityEditor.Experimental.GraphView;

public class CrossPattern : MonoBehaviour
{
    public BulletPoolManager poolManager; // BulletPoolManager 참조
    public float bulletSpeed = 5f; // 탄환 속도
    public Transform spawnPoint; // 탄환 시작 위치
    public int rows = 5; // row 개수
    public int columns = 6; // column 개수
    public float spawnInterval = 0.5f; // 탄환 생성 간격
    public float patternInterval;

    public float verticalGap; 
    public float rightSideBulletPosition; // right side Pos of crosspattern bullet

    void Start()
    {
        // FireCrossPattern 코루틴 시작
        InvokeRepeating(nameof(StartPattern), 0f, patternInterval);
    }

    void StartPattern()
    {
        StartCoroutine(FireCrossPattern());
    }

    IEnumerator FireCrossPattern()
    {
        for (int column = 0; column < columns; column++) // column 기준 루프
        {
            for (int row = 0; row < rows; row++) // row 기준 루프
            {
                // 총알 풀에서 총알 가져오기
                GameObject bullet = poolManager.bulletPool.Get();
                if (bullet == null) yield break;

                Bullet bulletComponent = bullet.GetComponent<Bullet>();
                if (bulletComponent == null) yield break;
                bulletComponent.Initialize(poolManager, bulletSpeed, 0);

                // 총알 초기화
                if (row % 2 == 1)
                {
                    bullet.transform.position = spawnPoint.position + new Vector3(0, -row * verticalGap, 0);
                    bullet.transform.rotation = Quaternion.identity; 
                    bulletComponent.SetDirection(Vector2.right); 
                }
                else
                {
                    //오른쪽에서 생성되어 왼쪽으로 
                    bullet.transform.position = spawnPoint.position + new Vector3(rightSideBulletPosition, -row * verticalGap, 0);
                    bullet.transform.rotation = Quaternion.identity;
                    bulletComponent.SetDirection(Vector2.left); 
                }



            }
            // 다음 탄환 생성까지 대기
            yield return new WaitForSeconds(spawnInterval);

        }
    }
}
