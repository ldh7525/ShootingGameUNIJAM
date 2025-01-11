using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss4 : MonoBehaviour
{
    public BulletPoolManager poolManager; // BulletPoolManager ����
    public Transform player;
    [Header("Pattern1 var")]    // ȸ���ϴ� �Ѿ�
    [SerializeField] private Transform[] pattern1BulletSpawner; // �Ѿ� �߻� ��������
    [SerializeField] private GameObject rotateBulletPrefab;
    [SerializeField] private float pattern1SpawnInterval;    // �Ѿ� �߻� ���� (�� ����)
    [SerializeField] private float pattern1BulletSpeed; // �Ѿ� �ӵ�
    [SerializeField] private float pattern1BulletRoSpeed; // �Ѿ� ȸ�� �ӵ�
    [SerializeField] private float pattern1RepeatCount; 


    [Header("Pattern2 var")]    // ������
    [SerializeField] private Transform[] pattern2BulletSpawner; // ������ ��������
    [SerializeField] private GameObject pattern2laserManagerPrefab;
    [SerializeField] private float pattern2beamAngle; // ������ ����
    [SerializeField] private float pattern2initalWidth; // ������ �ʱ� �ʺ�
    [SerializeField] private float pattern2finalWidth; // ������ �ʺ�
    [SerializeField] private float pattern2distance; // ������ ����
    [SerializeField] private float pattern2duration; // ������ ���ӽð�
    [SerializeField] private float pattern2expanstionTime; // ������ ��â �ð�
    [SerializeField] private float pattern2warningTime; // ������ ��� �ð�
    [SerializeField] public float pattern2SpawnInterval; // ���� �ð� ����
    [SerializeField] private float pattern2RepeatCount;

    [Header("Pattern3 var")]    // ������
    [SerializeField] private Transform[] pattern3BulletSpawner; // ������ ��������
    [SerializeField] private GameObject pattern3laserManagerPrefab;
    [SerializeField] private float pattern3beamAngle; // ������ ����
    [SerializeField] private float pattern3initalWidth; // ������ �ʱ� �ʺ�
    [SerializeField] private float pattern3finalWidth; // ������ �ʺ�
    [SerializeField] private float pattern3distance; // ������ ����
    [SerializeField] private float pattern3duration; // ������ ���ӽð�
    [SerializeField] private float pattern3expanstionTime; // ������ ��â �ð�
    [SerializeField] private float pattern3warningTime; // ������ ��� �ð�
    [SerializeField] public float pattern3SpawnInterval; // ���� �ð� ����
    [SerializeField] private float pattern3RepeatCount;

    public IEnumerator Pattern1()
    {
        int shootCount = 0;
        while (shootCount < pattern1RepeatCount)
        {
            // RotateBullet ����
            GameObject rotateBullet = Instantiate(rotateBulletPrefab, transform.position, Quaternion.identity);

            // ���� ��� (�÷��̾� ����)
            // Vector2 directionToPlayer = (player.position - pattern1BulletSpawner.position).normalized;

            // rotateBullet �ʱ�ȭ
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
             * ���� ��ġ�� �޾ƿ´�
             * ������ ����Ѵ�
             * �ʱ�ȭ�� �Ѵ�
             * �������� ���!
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
