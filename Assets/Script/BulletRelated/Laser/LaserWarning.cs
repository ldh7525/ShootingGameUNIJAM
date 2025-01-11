using UnityEngine;

public class LaserWarning : MonoBehaviour
{
    public Vector2 startPoint; // ���� ��ġ
    public float angle; // ����
    public float distance; // ����
    public float warningTime; // ��� �ð�
    public LineRenderer warningLine;

    void Start()
    {
        // ��� ����
        warningLine = GetComponent<LineRenderer>();
        warningLine.positionCount = 2;

        // ��� ��ġ ����
        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
        Vector2 endPoint = startPoint + direction * distance;

        warningLine.SetPosition(0, startPoint);
        warningLine.SetPosition(1, endPoint);

        // ��� ����
        Destroy(gameObject, warningTime);
    }
}
