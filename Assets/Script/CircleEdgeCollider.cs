using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class CircleEdgeCollider : MonoBehaviour
{
    public float radius = 1f; // ���� ������
    public int segments = 36; // ���� ������ ���� ���� (���׸�Ʈ)

    private void Start()
    {
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();

        // ���� �׵θ��� ������ ���
        Vector2[] points = new Vector2[segments + 1];
        float angleIncrement = 2 * Mathf.PI / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleIncrement;
            points[i] = new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
        }

        // ���� �ݱ� ���� ù ��° ���� �ٽ� �߰�
        points[segments] = points[0];

        edgeCollider.points = points;
    }
}
