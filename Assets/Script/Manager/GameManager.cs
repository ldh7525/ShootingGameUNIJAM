using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameEnd;
    public bool isGameClear;
    [SerializeField] private GameObject player;

    private void Update()
    {
        GameOver();
    }

    void GameOver()
    {
        if (player != null && !isGameEnd)
        {
            if (player.GetComponent<PlayerMovement>().playerHealth <= 0)
            {
                SoundManager.Instance.EffectSoundOn("GameOver");
                //Time.timeScale = 0.0f;
                isGameEnd = true;
            }
        }
    }
}
