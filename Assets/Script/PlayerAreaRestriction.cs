using UnityEngine;

public class PlayerAreaRestriction : MonoBehaviour
{
    [SerializeField] private Transform arena; // ���� ������ Transform
    [SerializeField] private float arenaRadius = 5f; // ���� ������ ������

    private void Update()
    {
        if (arena != null)
        {
            RestrictPlayerPosition();
        }
        else
        {
            Debug.LogWarning("Arena Transform has not been assigned in PlayerAreaRestriction.");
        }
    }

    private void RestrictPlayerPosition()
    {
        // �÷��̾��� ���� ��ġ
        Vector3 playerPosition = transform.position;

        // ���� ������ �߽� ��ġ
        Vector3 arenaCenter = arena.position;

        // �߽ɿ��� �÷��̾������ �Ÿ� ���
        Vector3 offset = playerPosition - arenaCenter;
        float distanceFromCenter = offset.magnitude;

        // �� ������ ������ ������ ��ġ ����
        if (distanceFromCenter > arenaRadius)
        {
            // �� ��� ���� ���� ����� ��ġ�� �÷��̾� ��ġ ����
            Vector3 clampedPosition = arenaCenter + offset.normalized * arenaRadius;

            // Y��(����)�� �������� �ʰ� ��ġ�� ����
            transform.position = new Vector3(clampedPosition.x, playerPosition.y, clampedPosition.z);
        }
    }

    private void OnDrawGizmos()
    {
        // Scene �信�� ���� ������ �ð������� Ȯ��
        if (arena != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(arena.position, arenaRadius);
        }
    }
}
