using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // On on Start
    [Header ("게임 시작시 항상 활성화")]
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject heart;
    [SerializeField] private GameObject stamina;

    // Not Always On
    [Header ("특정 상황에 사용")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    //Manager
    [Header("매니저")]
    [SerializeField] private GameObject phaseManager;
    [SerializeField] private GameObject spawbManager;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject player;

    [Header ("변수")]
    [SerializeField] private bool isDialogueActive = false; // 대화창 활성화 여부
    [SerializeField] private bool isPaused = true;

    private void Start()
    {
        if (dialoguePanel != null)
        {
            dialogueText = dialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
        }
        Time.timeScale = 1f;
    }

    void Update()
    {
        // 예시: 스페이스바를 눌러 대화창 활성화/비활성화
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleDialogue("대화창이 나타났습니다!");
        }

        IsOver();
    }

    public void GameStart()
    {
        StartButton.SetActive(false);
        player.SetActive(true);
        phaseManager.SetActive(true);
        spawbManager.SetActive(true);
        pauseButton.SetActive(true);
        heart.SetActive(true);
        stamina.SetActive(true);
    }

    // 대화창 토글 메서드
    public void ToggleDialogue(string message)
    {
        if (dialoguePanel == null) return;

        isDialogueActive = !isDialogueActive;
        dialoguePanel.SetActive(isDialogueActive);

        if (isDialogueActive && dialogueText != null)
        {
            // 텍스트 및 이미지 설정
            dialogueText.text = message;
        }
    }
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (pauseMenu != null) pauseMenu.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f; // 시간 조정
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void ExitPause()
    {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
    }
    void IsOver()
    {
        if (gameManager.GetComponent<GameManager>().isGameEnd)
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
            gameOverText.text = "Game Over";
        }
        else if (gameManager.GetComponent<GameManager>().isGameClear)
        {
            Time.timeScale = 0f;
            gameOverPanel.SetActive(true);
            gameOverText.text = "Game Clear";
        }
    }
    
}
