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
    [SerializeField] private GameObject pattern2laserManagerPrefab;
    [SerializeField] private float pattern2beamAngle; // 레이저 각도
    [SerializeField] private float pattern2initalWidth; // 레이저 초기 너비
    [SerializeField] private float pattern2finalWidth; // 레이저 너비
    [SerializeField] private float pattern2distance; // 레이저 길이
    [SerializeField] private float pattern2duration; // 레이저 지속시간
    [SerializeField] private float pattern2expanstionTime; // 레이저 팽창 시간
    [SerializeField] private float pattern2warningTime; // 레이저 경고 시간
    [SerializeField] public float pattern2SpawnInterval; // 생성 시간 간격
    [SerializeField] private float pattern2RepeatCount;

    [Header("Pattern3 var")]    // 레이저
    [SerializeField] private Transform[] pattern3BulletSpawner; // 레이저 시작지점
    [SerializeField] private GameObject pattern3laserManagerPrefab;
    [SerializeField] private float pattern3beamAngle; // 레이저 각도
    [SerializeField] private float pattern3initalWidth; // 레이저 초기 너비
    [SerializeField] private float pattern3finalWidth; // 레이저 너비
    [SerializeField] private float pattern3distance; // 레이저 길이
    [SerializeField] private float pattern3duration; // 레이저 지속시간
    [SerializeField] private float pattern3expanstionTime; // 레이저 팽창 시간
    [SerializeField] private float pattern3warningTime; // 레이저 경고 시간
    [SerializeField] public float pattern3SpawnInterval; // 생성 시간 간격
    [SerializeField] private float pattern3RepeatCount;

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

            GameObject laserManager = Instantiate(pattern2laserManagerPrefab, transform.position, Quaternion.identity);
            LaserManager laserManagerComponent = laserManager.GetComponent<LaserManager>();
            if (laserManagerComponent != null)
            {
                // Vector2 startPoint, float angle, float distance, float warningTime, float duration, float expanstionTime, float initalWidth, float finalWidth
                laserManagerComponent.Init(startPoint, angle, pattern2distance, pattern2warningTime, pattern2duration, pattern2expanstionTime, pattern2initalWidth, pattern2finalWidth);
                laserManagerComponent.laserStart();
            }
            yield return new WaitForSeconds(pattern2SpawnInterval);
            shootCount++;
        }
    }
    public IEnumerator Pattern3()
    {
        int shootCount = 0;
        while (shootCount < 1)
        {
            Vector2 startPoint = transform.position;
            // float angle = GetAngleToOrigin(startPoint);
            float angle = -90f;

            GameObject laserManager0 = Instantiate(pattern3laserManagerPrefab, transform.position, Quaternion.identity);
            LaserManager laserManagerComponent0 = laserManager0.GetComponent<LaserManager>();
            if (laserManagerComponent0 != null)
            {
                // Vector2 startPoint, float angle, float distance, float warningTime, float duration, float expanstionTime, float initalWidth, float finalWidth
                laserManagerComponent0.Init(startPoint, angle, pattern3distance, pattern3warningTime, pattern3duration, pattern3expanstionTime, pattern3initalWidth, pattern3finalWidth);
                laserManagerComponent0.laserStart();
            }
            yield return new WaitForSeconds(pattern3SpawnInterval);
            shootCount++;
        }

        while (shootCount < 2)
        {
            Vector3 offset = new Vector3(1.5f, 0, 0);
            Vector2 startPoint = transform.position + offset;
            // float angle = GetAngleToOrigin(startPoint);
            float angle = -90f;

            GameObject laserManager1 = Instantiate(pattern3laserManagerPrefab, transform.position, Quaternion.identity);
            LaserManager laserManagerComponent1 = laserManager1.GetComponent<LaserManager>();
            if (laserManagerComponent1 != null)
            {
                // Vector2 startPoint, float angle, float distance, float warningTime, float duration, float expanstionTime, float initalWidth, float finalWidth
                laserManagerComponent1.Init(startPoint, angle, pattern3distance, pattern3warningTime, pattern3duration, pattern3expanstionTime, pattern3initalWidth, pattern3finalWidth);
                laserManagerComponent1.laserStart();
            }

            startPoint = transform.position - offset;
            // float angle = GetAngleToOrigin(startPoint);
            // float angle = -90f;

            GameObject laserManager2 = Instantiate(pattern3laserManagerPrefab, transform.position, Quaternion.identity);
            LaserManager laserManagerComponent2 = laserManager2.GetComponent<LaserManager>();
            if (laserManagerComponent2 != null)
            {
                // Vector2 startPoint, float angle, float distance, float warningTime, float duration, float expanstionTime, float initalWidth, float finalWidth
                laserManagerComponent2.Init(startPoint, angle, pattern3distance, pattern3warningTime, pattern3duration, pattern3expanstionTime, pattern3initalWidth, pattern3finalWidth);
                laserManagerComponent2.laserStart();
            }
            yield return new WaitForSeconds(pattern3SpawnInterval);
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
