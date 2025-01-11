using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRotate : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector2 direction;
    [SerializeField] private float speed;
    [SerializeField] private float roSpeed;

    void Start()
    {
        // find player object
        player = GameObject.FindWithTag("Player");

        // set direction vector (player position - bullet position)
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position;
            direction = (targetPosition - transform.position).normalized; // ���� ���ͷ� ����
        }
    }

    void Update()
    {
        // move along the direction vector
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
        // rotate the bullet
        transform.Rotate(0, 0, roSpeed * Time.deltaTime);

        // destroy object if it goes out of screen
        if (Mathf.Abs(transform.position.x) > 20 || Mathf.Abs(transform.position.y) > 20)
        {
            Destroy(gameObject);
        }
    }
}