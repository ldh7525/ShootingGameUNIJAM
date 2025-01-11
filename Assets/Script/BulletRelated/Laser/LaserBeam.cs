using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public Vector2 startPoint; // ���� ��ġ
    public float angle; // ����
    public float distance; // ����
    public float initialWidth; // �ʱ� �ʺ�
    public float finalWidth; // ���� �ʺ�
    public float duration; // ���� �ð�
    public float expansionTime; // Ȯ�� �ð�
    public LineRenderer laserLine;

    private float timer = 0f;

    void Start()
    {
        // ������ ����
        laserLine = GetComponent<LineRenderer>();
        laserLine.positionCount = 2;

        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
        Vector2 endPoint = startPoint + direction * distance;

        laserLine.SetPosition(0, startPoint);
        laserLine.SetPosition(1, endPoint);

        laserLine.startWidth = initialWidth;
        laserLine.endWidth = initialWidth;

        // ���� �ð��� ������ ����
        Destroy(gameObject, duration);
    }

    void Update()
    {
        timer += Time.deltaTime;

        // �ʺ� Ȯ��Ǵ� �ִϸ��̼�
        if (timer <= expansionTime)
        {
            float width = Mathf.Lerp(initialWidth, finalWidth, timer / expansionTime);
            laserLine.startWidth = width;
            laserLine.endWidth = width;
        }
    }
}
