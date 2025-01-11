using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class LaserBeam : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private BoxCollider2D boxCollider;

    public Vector2 startPoint; // ������ ���� ��ġ
    public float angle; // ������ ����
    public float distance; // ������ ����
    public float initialWidth; // �ʱ� ������ �β�
    public float finalWidth; // ���� ������ �β�
    public float duration; // ������ ���� �ð�
    public float expansionTime; // ������ Ȯ�� �ð�

    private float timer = 0f;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        lineRenderer.positionCount = 2;

        // �������� �������� ������ ���
        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
        Vector2 endPoint = startPoint + direction * distance;

        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, endPoint);

        lineRenderer.startWidth = initialWidth;
        lineRenderer.endWidth = initialWidth;

        UpdateCollider();

        // ���� �ð��� ������ ����
        Destroy(gameObject, duration);
    }

    void Update()
    {
        timer += Time.deltaTime;

        // ������ �β��� Ȯ��
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
        //// LineRenderer�� �������� ���� ��������
        //Vector3 start = lineRenderer.GetPosition(0);
        //Vector3 end = lineRenderer.GetPosition(1);

        //// �������� �β��� �ݿ��� �ݶ��̴� �� ����
        //float width = lineRenderer.startWidth;
        //Vector2 direction = (end - start).normalized;
        //Vector2 perpendicular = new Vector2(-direction.y, direction.x) * width / 2;

        //// �ݶ��̴��� �� �� ���� (�������� ���)
        //Vector2[] points = new Vector2[4];
        //points[0] = (Vector2)start + perpendicular;
        //points[1] = (Vector2)start - perpendicular;
        //points[2] = (Vector2)end - perpendicular;
        //points[3] = (Vector2)end + perpendicular;

        //boxCollider.points = points;
        //// print four points
        //Debug.Log(string.Format("{0} {1} {2} {3}", points[0], points[1], points[2], points[3]));

        // LineRenderer�� �������� ���� ��������
        Vector3 start = lineRenderer.GetPosition(0);
        Vector3 end = lineRenderer.GetPosition(1);
        // Debug.Log(string.Format("start: {0} end: {1}", start, end));

        Vector3 midPoint = (start + end) / 2; // �������� ������ �߰� ����

        boxCollider.size = new Vector2(distance * 4.5f , lineRenderer.endWidth * 4.5f);
        boxCollider.offset = transform.InverseTransformPoint(midPoint);


        //�ݶ��̴� ȸ�� ����
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