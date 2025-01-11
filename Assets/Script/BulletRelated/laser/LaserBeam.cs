using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class LaserBeam : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private BoxCollider2D boxCollider;

    public Vector2 startPoint; // 레이저 시작 위치
    public float angle; // 레이저 각도
    public float distance; // 레이저 길이
    public float initialWidth; // 초기 레이저 두께
    public float finalWidth; // 최종 레이저 두께
    public float duration; // 레이저 지속 시간
    public float expansionTime; // 레이저 확장 시간

    private float timer = 0f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        lineRenderer.positionCount = 2;

        // 레이저의 시작점과 끝점을 계산
        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
        Vector2 endPoint = startPoint + direction * distance;

        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);

        lineRenderer.startWidth = initialWidth;
        lineRenderer.endWidth = initialWidth;

        UpdateCollider();

        // 일정 시간이 지나면 삭제
        // Destroy(gameObject, duration);
    }

    void Update()
    {
        timer += Time.deltaTime;

        // 레이저 두께를 확장
        if (timer <= expansionTime)
        {
            float width = Mathf.Lerp(initialWidth, finalWidth, timer / expansionTime);
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
            UpdateCollider();
        }
    }

    void UpdateCollider()
    {
        //// LineRenderer의 시작점과 끝점 가져오기
        //Vector3 start = lineRenderer.GetPosition(0);
        //Vector3 end = lineRenderer.GetPosition(1);

        //// 레이저의 두께를 반영한 콜라이더 점 설정
        //float width = lineRenderer.startWidth;
        //Vector2 direction = (end - start).normalized;
        //Vector2 perpendicular = new Vector2(-direction.y, direction.x) * width / 2;

        //// 콜라이더의 네 점 설정 (레이저의 경계)
        //Vector2[] points = new Vector2[4];
        //points[0] = (Vector2)start + perpendicular;
        //points[1] = (Vector2)start - perpendicular;
        //points[2] = (Vector2)end - perpendicular;
        //points[3] = (Vector2)end + perpendicular;

        //boxCollider.points = points;
        //// print four points
        //Debug.Log(string.Format("{0} {1} {2} {3}", points[0], points[1], points[2], points[3]));

        // LineRenderer의 시작점과 끝점 가져오기
        Vector3 start = lineRenderer.GetPosition(0);
        Vector3 end = lineRenderer.GetPosition(1);
        // Debug.Log(string.Format("start: {0} end: {1}", start, end));

        Vector3 midPoint = (start + end) / 2; // 시작점과 끝점의 중간 지점

        boxCollider.size = new Vector2(distance * 4.5f , lineRenderer.endWidth * 4.5f);
        boxCollider.offset = transform.InverseTransformPoint(midPoint);


        //콜라이더 회전 설정
        Vector3 direction = end - start;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        boxCollider.transform.rotation = Quaternion.Euler(0, 0, angle);

        //Debug.Log(string.Format(length);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player hit by laser!");
        }
    }
}