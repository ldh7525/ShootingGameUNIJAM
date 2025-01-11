using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRotation : MonoBehaviour
{
    [SerializeField] private static float rotationSpeed; // static 변수로 선언
    [SerializeField] private bool isRotate;

    // Start is called before the first frame update
    void Start()
    {
        if (rotationSpeed == 0) // 처음 한 번만 코루틴 실행
        {
            StartCoroutine(RotationSpeedChange());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotate)
        {
            float rotationAmount = rotationSpeed * Time.deltaTime;

            transform.Rotate(0, 0, rotationAmount);
        }
    }

    IEnumerator RotationSpeedChange()
    {
        while (true)
        {
            rotationSpeed = Random.Range(-20, 20); // 모든 오브젝트가 공유하는 값
            yield return new WaitForSeconds(5.0f);
        }
    }
}
