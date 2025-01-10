using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootToPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject Bullet;
    [SerializeField] private float shootSpeed;

    private float shootTime;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        shootTime += Time.deltaTime;
        if (shootTime >= 1)
        {
            GameObject bullet = Instantiate(Bullet, transform.position , Quaternion.identity);
            shootTime = 0;
        }
    }

}
