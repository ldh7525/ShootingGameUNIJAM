using UnityEngine;

public class LaserWarning : MonoBehaviour
{
    public Vector2 startPoint; // 시작 위치
    public float angle; // 각도
    public float distance; // 길이
    public float warningTime; // 경고 시간
    public LineRenderer warningLine;
    public Color warningColor = Color.red;  // 빨간색 레이저
    public float width; // 너비


    void Start()
    {
        // 경고선 설정
        warningLine = GetComponent<LineRenderer>();
        warningLine.positionCount = 2;

        // 경고선 위치 설정
        Vector2 direction = Quaternion.Euler(0, 0, angle) * Vector2.right;
        Vector2 endPoint = startPoint + direction * distance;
        warningLine.startWidth = width; // 레이저 시작 두께
        warningLine.endWidth = width;   // 레이저 끝 두께
        warningLine.material = new Material(Shader.Find("Sprites/Default"));
        Color startColor = new Color(1f, 0f, 0f, 0.5f); // 빨간색, 투명도 50%
        Color endColor = new Color(1f, 0f, 0f, 0.5f);

        // LineRenderer의 startColor와 endColor에 적용
        warningLine.startColor = startColor;
        warningLine.endColor = endColor;
        //warningLine.sortingLayerName = "Default"; // 레이저가 그려질 레이어
        //warningLine.sortingOrder = 1; // 레이어 내 순서


        warningLine.SetPosition(0, startPoint);
        warningLine.SetPosition(1, endPoint);

        // 경고선 삭제
        Destroy(gameObject, warningTime);
    }
}
