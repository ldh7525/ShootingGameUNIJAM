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
    [SerializeField] private GameObject laserManagerPrefab;
    [SerializeField] private float beamAngle; // ������ ����
    [SerializeField] private float initalWidth; // ������ �ʱ� �ʺ�
    [SerializeField] private float finalWidth; // ������ �ʺ�
    [SerializeField] private float distance; // ������ ����
    [SerializeField] private float duration; // ������ ���ӽð�
    [SerializeField] private float expanstionTime; // ������ ��â �ð�
    [SerializeField] private float warningTime; // ������ ��� �ð�
    [SerializeField] public float pattern2SpawnInterval; // ���� �ð� ����
    [SerializeField] private float pattern2RepeatCount;

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
