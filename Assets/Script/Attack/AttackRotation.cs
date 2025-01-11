using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private bool isRotate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RotationSpeedChange());
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
        while (Mathf.Abs(rotationSpeed) < 10)
        {
            rotationSpeed = Random.Range(-20, 20);
        }
        yield return new WaitForSeconds(5.0f);
    }
}
