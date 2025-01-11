using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChaseBullet : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector2 direction;
    [SerializeField] float speed;

    void Start()
    {
        // �÷��̾� ������Ʈ ã��
        player = GameObject.FindWithTag("Player");

        // ���� ���� ���� (�÷��̾� ��ġ - �Ѿ� ��ġ)
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position;
            direction = (targetPosition - transform.position).normalized; // ���� ���ͷ� ����
        }
    }

    void Update()
    {
        // ���� ���͸� ����Ͽ� �̵�
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        // ȭ�� ������ ������ ������Ʈ ����
        if (Mathf.Abs(transform.position.x) > 20 || Mathf.Abs(transform.position.y) > 20)
        {
            Destroy(gameObject);
        }
    }
}
