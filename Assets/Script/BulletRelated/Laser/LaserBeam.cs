using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Vector2 startPoint; // 시작 위치
    public float angle; // 각도
    public float distance; // 길이
    public float initialWidth; // 초기 너비
    public float finalWidth; // 최종 너비
    public float duration; // 지속 시간
    public float expansionTime; // 확장 시간
    public LineRenderer laserLine;

    private float timer = 0f;

    void Start()
    {
        // 레이저 설정
        laserLine = GetComponent<LineRenderer>();
        laserLine.positionCount = 2;

        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
        Vector2 endPoint = startPoint + direction * distance;

        laserLine.SetPosition(0, startPoint);
        laserLine.SetPosition(1, endPoint);

        laserLine.startWidth = initialWidth;
        laserLine.endWidth = initialWidth;

        // 일정 시간이 지나면 삭제
        Destroy(gameObject, duration);
    }

    void Update()
    {
        timer += Time.deltaTime;

        // 너비가 확장되는 애니메이션
        if (timer <= expansionTime)
        {
            float width = Mathf.Lerp(initialWidth, finalWidth, timer / expansionTime);
            laserLine.startWidth = width;
            laserLine.endWidth = width;
        }
    }
}
