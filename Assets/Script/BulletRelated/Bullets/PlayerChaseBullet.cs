using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChaseBullet : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector2 direction;
    [SerializeField] float speed;

    void Start()
    {
        // 플레이어 오브젝트 찾기
        player = GameObject.FindWithTag("Player");

        // 방향 벡터 설정 (플레이어 위치 - 총알 위치)
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position;
            direction = (targetPosition - transform.position).normalized; // 단위 벡터로 설정
        }
    }

    void Update()
    {
        // 방향 벡터를 사용하여 이동
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // 화면 밖으로 나가면 오브젝트 제거
        if (Mathf.Abs(transform.position.x) > 20 || Mathf.Abs(transform.position.y) > 20)
        {
            Destroy(gameObject);
        }
    }
}
