using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class CircleEdgeCollider : MonoBehaviour
{
    public float radius = 1f; // 원의 반지름
    public int segments = 36; // 원을 구성할 점의 개수 (세그먼트)

    private void Start()
    {
        EdgeCollider2D edgeCollider = GetComponent<EdgeCollider2D>();

        // 원형 테두리의 점들을 계산
        Vector2[] points = new Vector2[segments + 1];
        float angleIncrement = 2 * Mathf.PI / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = i * angleIncrement;
            points[i] = new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
        }

        // 원을 닫기 위해 첫 번째 점을 다시 추가
        points[segments] = points[0];

        edgeCollider.points = points;
    }
}
