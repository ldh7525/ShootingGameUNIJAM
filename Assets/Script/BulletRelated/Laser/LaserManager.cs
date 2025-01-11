using UnityEngine;
using System.Collections;

public class LaserManager : MonoBehaviour
{
    public GameObject warningPrefab; // ��� ������
    public GameObject laserPrefab; // ������ ������

    public Vector2 startPoint; // ���� ��ġ
    public float angle; // ����
    public float distance; // ����
    public float warningTime; // ��� �ð�
    public float duration; // ���� �ð�
    public float expansionTime; // Ȯ�� �ð�
    public float initialWidth; // �ʱ� �ʺ�
    public float finalWidth; // ���� �ʺ�

    void Start()
    {
        StartCoroutine(FireLaser());
    }

    IEnumerator FireLaser()
    {
        // ��� ����
        Debug.Log("start making Warning line");
        GameObject warning = Instantiate(warningPrefab, Vector3.zero, Quaternion.identity);
        LaserWarning warningScript = warning.GetComponent<LaserWarning>();
        warningScript.startPoint = startPoint;
        warningScript.angle = angle;
        warningScript.distance = distance;
        warningScript.warningTime = warningTime;
        warningScript.width = finalWidth;

        // ��� �ð��� ������ ������ ����
        yield return new WaitForSeconds(warningTime);

        GameObject laser = Instantiate(laserPrefab, Vector3.zero, Quaternion.identity);
        LaserBeam laserScript = laser.GetComponent<LaserBeam>();
        laserScript.startPoint = startPoint;
        laserScript.angle = angle;
        laserScript.distance = distance;
        laserScript.initialWidth = initialWidth;
        laserScript.finalWidth = finalWidth;
        laserScript.duration = duration;
        laserScript.expansionTime = expansionTime;
    }
}
