using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss4 : MonoBehaviour
{
    public BulletPoolManager poolManager; // BulletPoolManager 참조
    public Transform player;
    [Header("Pattern1 var")]    // 회전하는 총알
    [SerializeField] private Transform[] pattern1BulletSpawner; // 총알 발사 시작지점
    [SerializeField] private GameObject rotateBulletPrefab;
    [SerializeField] private float pattern1SpawnInterval;    // 총알 발사 간격 (초 단위)
    [SerializeField] private float pattern1BulletSpeed; // 총알 속도
    [SerializeField] private float pattern1BulletRoSpeed; // 총알 회전 속도
    [SerializeField] private float pattern1RepeatCount; 


    [Header("Pattern2 var")]    // 레이저
    [SerializeField] private Transform[] pattern2BulletSpawner; // 레이저 시작지점
    [SerializeField] private GameObject laserManagerPrefab;
    [SerializeField] private float beamAngle; // 레이저 각도
    [SerializeField] private float initalWidth; // 레이저 초기 너비
    [SerializeField] private float finalWidth; // 레이저 너비
    [SerializeField] private float distance; // 레이저 길이
    [SerializeField] private float duration; // 레이저 지속시간
    [SerializeField] private float expanstionTime; // 레이저 팽창 시간
    [SerializeField] private float warningTime; // 레이저 경고 시간
    [SerializeField] public float pattern2SpawnInterval; // 생성 시간 간격
    [SerializeField] private float pattern2RepeatCount;

    public IEnumerator Pattern1()
    {
        int shootCount = 0;
        while (shootCount < pattern1RepeatCount)
        {
            // RotateBullet 생성
            GameObject rotateBullet = Instantiate(rotateBulletPrefab, transform.position, Quaternion.identity);

            // 방향 계산 (플레이어 방향)
            // Vector2 directionToPlayer = (player.position - pattern1BulletSpawner.position).normalized;

            // rotateBullet 초기화
            BulletRotate rotateBulletComponent = rotateBullet.GetComponent<BulletRotate>();
            if (rotateBulletComponent != null)
            {
                rotateBulletComponent.Init(pattern1BulletSpeed, pattern1BulletRoSpeed);
                rotateBullet.transform.position = transform.position;
            }
            yield return new WaitForSeconds(pattern1SpawnInterval);
            shootCount++;
        }
    }
    public IEnumerator Pattern2()
    {
        int shootCount = 0;
        while (shootCount < pattern2RepeatCount)
        {
            /*
             * 시작 위치를 받아온다
             * 각도를 계산한다
             * 초기화를 한다
             * 레이저를 쏜다!
             */
            Vector2 startPoint = transform.position;
            // float angle = GetAngleToOrigin(startPoint);
            float angle = -90f;

            GameObject laserManager = Instantiate(laserManagerPrefab, transform.position, Quaternion.identity);
            LaserManager laserManagerComponent = laserManager.GetComponent<LaserManager>();
            if (laserManagerComponent != null)
            {
                // Vector2 startPoint, float angle, float distance, float warningTime, float duration, float expanstionTime, float initalWidth, float finalWidth
                laserManagerComponent.Init(startPoint, angle, distance, warningTime, duration, expanstionTime, initalWidth, finalWidth);
                laserManagerComponent.laserStart();
            }
            yield return new WaitForSeconds(pattern2SpawnInterval);
            shootCount++;
        }
    }
    public static float GetAngleToOrigin(Vector2 point)
    {
        Vector2 directionToOrigin = -point;
        float radians = Mathf.Atan2(directionToOrigin.y, directionToOrigin.x);
        float degrees = radians * Mathf.Rad2Deg;

        return degrees;
    }


}
