using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stamina : MonoBehaviour
{
    PlayerMovement playerMovement;
    [SerializeField] TextMeshProUGUI dashCount;
    [SerializeField] RectTransform scale;

    void Start()
    {
        playerMovement = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();    
        scale = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float dashnum = playerMovement.stamina / 20;
        dashCount.text = dashnum.ToString("F1");
        
        scale.localScale = new Vector3 (playerMovement.stamina / 100 , 1, 1);
    }
}
