using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject pauseButton;



    public void OnStartButtonPressed()
    {
        mainMenu.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
    }
}
