using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f; // źȯ �ӵ�
    public int mode = 0;
    private Vector2 direction;
    private BulletPoolManager poolManager;

    private Vector2 perpendicularDirection;
    public float amplitude = 1f; // sin 경로의 진폭
    public float frequency = 1f; // sin 경로의 주기
    public Vector2 startPos;
    public float startTime;

    // 이거 sin으로 움직이는 bullet object를 따로 만들어서 mode 분리해 주세요. 제가 unity를 잘 몰라서 sprite는 잘 못 만지겠어요.
    public void Initialize(BulletPoolManager manager, float speed, int mode)
    {
        poolManager = manager; // PoolManager ���� ����
        poolManager = manager; // PoolManager ���� ����
        this.speed = speed; // źȯ �ӵ� ����
        this.mode = mode;
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized; // �̵� ���� ����
        direction = dir.normalized; // �̵� ���� ����
        this.perpendicularDirection = new Vector2(-direction.y, direction.x).normalized;
    }

    public void GetPosition(Vector2 pos)
    {
        startPos = pos;
    }

    public void GetTime(float time)
    {
        transform.Translate(direction * speed * Time.deltaTime);
        startTime = time;
    }

    void Update()
    {
        if (mode == 0) // move forward
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
        else if (mode == 1) // move sin x
        {
            // 방향 벡터를 따라 이동 (x축에 해당)
            float dTime = Time.time - startTime;
            float linearProgress = dTime * speed;
            Vector2 linearMovement = direction * linearProgress;

            // sin(x) 기반으로 수직 이동 계산 (y축에 해당)
            float sinWaveOffset = Mathf.Sin(linearProgress * frequency) * amplitude;
            Vector2 waveMovement = perpendicularDirection * sinWaveOffset;

            // 최종 위치 계산
            Vector2 newPosition = startPos + linearMovement + waveMovement;

            // 위치 업데이트
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
        }

        // ȭ�� ������ ������ Ǯ�� ��ȯ
        // ȭ�� ������ ������ Ǯ�� ��ȯ
        if (Mathf.Abs(transform.position.x) > 20 || Mathf.Abs(transform.position.y) > 20)
        {
            poolManager.ReturnBulletToPool(gameObject);
        }
    }
}
