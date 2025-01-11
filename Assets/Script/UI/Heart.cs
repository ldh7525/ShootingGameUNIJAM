using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab; 
    [SerializeField] private Transform heartContainer; 
    private List<GameObject> hearts = new List<GameObject>(); 
    [SerializeField] private PlayerMovement player; 

    // Start is called before the first frame update
    void Start()
    {
        UpdateHearts();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHearts();
    }

    void UpdateHearts()
    {
        int currentHealth = player.playerHealth;

        // 현재 하트 수와 플레이어 체력을 비교하여 조정
        if (currentHealth > hearts.Count)
        {
            for (int i = hearts.Count; i < currentHealth; i++)
            {
                GameObject newHeart = Instantiate(heartPrefab, heartContainer);
                hearts.Add(newHeart);
            }
        }
        else if (currentHealth < hearts.Count)
        {
            for (int i = hearts.Count - 1; i >= currentHealth; i--)
            {
                Destroy(hearts[i]);
                hearts.RemoveAt(i);
            }
        }
    }
}
