using UnityEngine;
using System.Collections;

public class BulletVform : MonoBehaviour
{
    //public int pattern3BulletCount;          // number of bullets in one shot
    //public float pattern3SpawnInterval;    // number of seconds between shots
    //public float pattern3BulletSpeed; // speed of bullets
    //public float pattern3Hight;
    //public float pattern3Width;

    //void Start()
    //{
    //    // ���� �ð� �������� ź�� �߻� ����
    //    InvokeRepeating(nameof(StartPattern), 0f, pattern3SpawnInterval);
    //}

    //void StartPattern()
    //{
    //    StartCoroutine(FireVformPattern());
    //}

    //IEnumerator FireVformPattern()
    //{
    //    //Vector2 direction = player.position - transform.position;
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        

    //    for (int i = 0; i < pattern3BulletCount; i++)
    //    {
    //        // Ǯ���� �Ѿ� ��������
    //        //GameObject bullet = poolManager.bulletPool.Get();
    //        if (bullet == null)
    //        {
    //            Debug.LogWarning("źȯ�� Ǯ���� ������ �� �����ϴ�.");
    //            continue;
    //        }

    //        // �Ѿ� �ʱ�ȭ (BulletPoolManager�� ����)
    //        //Bullet bulletComponent = bullet.GetComponent<Bullet>();
    //        if (bulletComponent != null)
    //        {
    //            //bulletComponent.Initialize(poolManager, pattern3BulletSpeed, 0); // �Ѿ� ���� �ʱ�ȭ
    //            bulletComponent.SetDirection(GetDirectionFromAngle(angle)); // ?
    //        }
    //        else
    //        {
    //            Debug.LogWarning("Bullet ��ũ��Ʈ�� �Ѿ˿� ÷�εǾ� ���� �ʽ��ϴ�.");
    //        }

    //        // �Ѿ� ��ġ�� ���� ����
    //        int offset = i - pattern3BulletCount / 2;
    //        int offset_abs = - Mathf.Abs(offset);
    //        Vector3 normDir = new Vector3(direction.x, direction.y, 0).normalized;
    //        Vector3 perpDir = new Vector3(-direction.y, direction.x, 0).normalized;
    //        bullet.transform.position = transform.position + (perpDir * offset * pattern3Width) + (normDir * offset_abs * pattern3Hight);
    //        bullet.transform.rotation = Quaternion.identity; // �ʱ�ȭ

    //        // ���� ������ �̵�
    //        // angle += angleStep;

    //        // ���� �Ѿ� �߻���� ���
    //       // yield return new WaitForSeconds(bulletSpawnDelay);
            
    //    }
    //    yield return null;
    //}

    //// �����κ��� ���� ���͸� ����ϴ� �޼���
    //private Vector2 GetDirectionFromAngle(float angle)
    //{
    //    float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
    //    float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
    //    return new Vector2(dirX, dirY).normalized;
    //}
}
