using UnityEngine;
using System.Collections;

public class LaserManager : MonoBehaviour
{
    public GameObject warningPrefab; // 경고선 프리팹
    public GameObject laserPrefab; // 레이저 프리팹

    public Vector2 startPoint; // 시작 위치
    public float angle; // 각도

    public float distance; // 길이
    public float warningTime; // 경고 시간
    public float duration; // 지속 시간
    public float expansionTime; // 확장 시간
    public float initialWidth; // 초기 너비
    public float finalWidth; // 최종 너비

    public void Init(Vector2 startPoint, float angle, float distance, float warningTime, float duration, float expanstionTime, float initalWidth, float finalWidth)
    {
        this.startPoint = startPoint;
        this.angle = angle;
        this.distance = distance;
        this.warningTime = warningTime;
        this.duration = duration;
        this.expansionTime = expanstionTime;
        this.initialWidth = initalWidth;
        this.finalWidth = finalWidth;
    }
    public void laserStart()
    {
        StartCoroutine(FireLaser());
    }

    IEnumerator FireLaser()
    {
        // 경고선 생성
        Debug.Log("start making Warning line");
        GameObject warning = Instantiate(warningPrefab, Vector3.zero, Quaternion.identity);
        LaserWarning warningScript = warning.GetComponent<LaserWarning>();
        warningScript.startPoint = startPoint;
        warningScript.angle = angle;
        warningScript.distance = distance;
        warningScript.warningTime = warningTime;
        warningScript.width = finalWidth;

        // 경고 시간이 지나면 레이저 생성
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
