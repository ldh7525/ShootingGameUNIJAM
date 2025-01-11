using UnityEngine;

public class LaserWarning : MonoBehaviour
{
    public Vector2 startPoint; // 시작 위치
    public float angle; // 각도
    public float distance; // 길이
    public float warningTime; // 경고 시간
    public LineRenderer warningLine;

    void Start()
    {
        // 경고선 설정
        warningLine = GetComponent<LineRenderer>();
        warningLine.positionCount = 2;

        // 경고선 위치 설정
        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
        Vector2 endPoint = startPoint + direction * distance;

        warningLine.SetPosition(0, startPoint);
        warningLine.SetPosition(1, endPoint);

        // 경고선 삭제
        Destroy(gameObject, warningTime);
    }
}
