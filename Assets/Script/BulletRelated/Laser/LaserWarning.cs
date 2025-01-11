using UnityEngine;

public class LaserWarning : MonoBehaviour
{
    public Vector2 startPoint; // ���� ��ġ
    public float angle; // ����
    public float distance; // ����
    public float warningTime; // ��� �ð�
    public LineRenderer warningLine;
    public Color warningColor = Color.red;  // ������ ������
    public float width; // �ʺ�


    void Start()
    {
        // ��� ����
        warningLine = GetComponent<LineRenderer>();
        warningLine.positionCount = 2;

        // ��� ��ġ ����
        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
        Vector2 endPoint = startPoint + direction * distance;
        warningLine.startWidth = width; // ������ ���� �β�
        warningLine.endWidth = width;   // ������ �� �β�
        warningLine.material = new Material(Shader.Find("Sprites/Default"));
        Color startColor = new Color(1f, 0f, 0f, 0.5f); // ������, ���� 50%
        Color endColor = new Color(1f, 0f, 0f, 0.5f);

        // LineRenderer�� startColor�� endColor�� ����
        warningLine.startColor = startColor;
        warningLine.endColor = endColor;
        //warningLine.sortingLayerName = "Default"; // �������� �׷��� ���̾�
        //warningLine.sortingOrder = 1; // ���̾� �� ����


        warningLine.SetPosition(0, startPoint);
        warningLine.SetPosition(1, endPoint);

        // ��� ����
        Destroy(gameObject, warningTime);
    }
}
