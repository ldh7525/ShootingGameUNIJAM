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
    public float contractionTime; // 레이저 확장 시간

    private float timer = 0f;

    void Start()
    {
        contractionTime = expansionTime;
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

        // 총 지속 시간 계산
        float totalDuration = expansionTime + duration + contractionTime;
        Destroy(gameObject, totalDuration);
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
        else if (timer > expansionTime && timer <= expansionTime + duration)
        {
            // 유지 단계
            lineRenderer.startWidth = finalWidth;
            lineRenderer.endWidth = finalWidth;
            UpdateCollider();
        }
        else if (timer > expansionTime + duration && timer <= expansionTime + duration + contractionTime)
        {
            // 축소 단계
            float contractionTimer = timer - (expansionTime + duration);
            float width = Mathf.Lerp(finalWidth, initialWidth, contractionTimer / contractionTime);
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
            UpdateCollider();
        }
    }

    void UpdateCollider()
    {

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