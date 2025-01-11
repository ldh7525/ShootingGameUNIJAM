using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetedBullet : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float speed; 
    private Vector2 direction;

    void Start()
    {
        Vector2 playerPos = GameObject.FindWithTag("Player").transform.position;

        Vector2 randomTarget = playerPos + new Vector2(Random.Range(-range, range), Random.Range(-range, range));

        direction = (randomTarget - (Vector2)transform.position).normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if (Mathf.Abs(transform.position.x) > 20 || Mathf.Abs(transform.position.y) > 20)
        {
            Destroy(gameObject);
        }
    }
}
