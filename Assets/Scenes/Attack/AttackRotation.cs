using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRotation : MonoBehaviour
{
    [SerializeField] private static float rotationSpeed; // static ������ ����
    [SerializeField] private bool isRotate;

    // Start is called before the first frame update
    void Start()
    {
        if (rotationSpeed == 0) // ó�� �� ���� �ڷ�ƾ ����
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
            rotationSpeed = Random.Range(-20, 20); // ��� ������Ʈ�� �����ϴ� ��
            yield return new WaitForSeconds(5.0f);
        }
    }
}
