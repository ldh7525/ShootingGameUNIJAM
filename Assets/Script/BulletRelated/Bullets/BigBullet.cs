using UnityEngine;
using UnityEngine.UIElements;

public class BigBullet : MonoBehaviour
{
    public GameObject bulletPrefab; // 발사할 탄환 프리팹
    public int bulletCount = 30; // 발사할 탄환의 개수
    public float spreadRadius = 1f; // 탄환이 퍼질 거리
    public float smallBulletSpeed = 5f; // 발사된 탄환의 속도
    public float bigBulletSpeed = 2f; // 발사된 탄환의 속도


    private void Update()
    {
        transform.Translate(Vector2.right * bigBulletSpeed * Time.deltaTime);
    }
        
    public void OnTriggerExit2D(Collider2D other)
    {
        // 특정 Trigger Collider를 기준으로 처리
        if (other.CompareTag("Ground")) // Trigger Zone에 태그 설정
        {
            HandleTriggerExit();
        }
    }

    private void HandleTriggerExit()
    {
        Debug.Log($"Bullet {name} exited the trigger zone!");
        SpreadBullets(); // 탄환 발사
        Destroy(gameObject); // BigBullet 파괴
    }

    void SpreadBullets()
    {
        for (int i = 0; i < bulletCount; i++)
        {
            // 각 탄환의 발사 각도 계산
            float angle = 360f / bulletCount * i;
            float radian = angle * Mathf.Deg2Rad;

            // 탄환의 위치 계산 (BigBullet의 주변)
            Vector3 spawnPosition = transform.position + new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0) * spreadRadius;

            // 탄환 생성
            GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

            // 탄환의 방향 설정
            Vector2 direction = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)).normalized;

            // Rigidbody2D를 사용해 속도 적용
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * smallBulletSpeed;
            }
        }
    }
}
