using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseMenu;

    public GameObject dialoguePanel; // 대화창 패널 (Image + Text)
    public TextMeshProUGUI dialogueText; // 대화창 텍스트

    private bool isDialogueActive = false; // 대화창 활성화 여부
    private bool isPaused = true;

    private void Start()
    {
        // 필요한 GameObject들을 Find로 가져오기
        pauseButton = GameObject.Find("PauseButton");
        pauseMenu = GameObject.Find("PauseMenu");
        dialoguePanel = GameObject.Find("DialoguePanel");

        if (dialoguePanel != null)
        {
            dialogueText = dialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
        }

        // 초기 상태 설정
        if (pauseButton != null) pauseButton.SetActive(false);
        if (dialoguePanel != null) dialoguePanel.SetActive(false);

        pauseButton.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    void Update()
    {
        // 예시: 스페이스바를 눌러 대화창 활성화/비활성화
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleDialogue("대화창이 나타났습니다!");
        }
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
}
