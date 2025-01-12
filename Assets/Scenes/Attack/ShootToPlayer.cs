using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootToPlayer : MonoBehaviour
{
    private GameObject player;
    private AudioSource audioSource;
    [SerializeField] private List<GameObject> Bullet;
    [SerializeField] private float shootSpeed;

    private float shootTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player");
        StartCoroutine(ShootSpeed());
    }

    private void Update()
    {
        shootTime += Time.deltaTime;
        if (shootTime >= shootSpeed)
        {
            GameObject bullet = Instantiate(Bullet[Random.Range(0,Bullet.Count)], transform.position , Quaternion.identity);
            audioSource.Play();
            shootTime = 0;
        }
    }

    IEnumerator ShootSpeed()
    {
        shootSpeed = Random.Range(0.4f, 2f);
        yield return new WaitForSeconds(10);
    }

}
