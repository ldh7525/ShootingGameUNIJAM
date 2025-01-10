using UnityEngine;

public class PlayerAreaRestriction : MonoBehaviour
{
    [SerializeField] private Transform arena; // 원형 영역의 Transform
    [SerializeField] private float arenaRadius = 5f; // 원형 영역의 반지름

    private void Update()
    {
        if (arena != null)
        {
            RestrictPlayerPosition();
        }
        else
        {
            Debug.LogWarning("Arena Transform has not been assigned in PlayerAreaRestriction.");
        }
    }

    private void RestrictPlayerPosition()
    {
        // 플레이어의 현재 위치
        Vector3 playerPosition = transform.position;

        // 원형 영역의 중심 위치
        Vector3 arenaCenter = arena.position;

        // 중심에서 플레이어까지의 거리 계산
        Vector3 offset = playerPosition - arenaCenter;
        float distanceFromCenter = offset.magnitude;

        // 원 반지름 밖으로 나가면 위치 제한
        if (distanceFromCenter > arenaRadius)
        {
            // 원 경계 내의 가장 가까운 위치로 플레이어 위치 제한
            Vector3 clampedPosition = arenaCenter + offset.normalized * arenaRadius;

            // Y축(높이)은 변경하지 않고 위치를 제한
            transform.position = new Vector3(clampedPosition.x, playerPosition.y, clampedPosition.z);
        }
    }

    private void OnDrawGizmos()
    {
        // Scene 뷰에서 원형 영역을 시각적으로 확인
        if (arena != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(arena.position, arenaRadius);
        }
    }
}
