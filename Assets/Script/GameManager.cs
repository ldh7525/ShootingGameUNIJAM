using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject mainUI;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject endUI;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI recordText;
    [SerializeField] private TextMeshProUGUI bestRecordText;
    [SerializeField] private GameObject pauseButton;

    private float gameTime = 0f;
    private bool isGameActive = false;
    private bool isPaused = false;
    private float bestRecord = 0f;

    private void Start()
    {
        ShowMainUI();
    }

    private void Update()
    {
        if (isGameActive && !isPaused)
        {
            gameTime += Time.deltaTime;
            UpdateTimeText();
        }
    }

    private void UpdateTimeText()
    {
        if (timeText != null)
        {
            timeText.text = $"Time: {gameTime:F2} s";
        }
    }

    public void StartGame()
    {
        // 게임 시작 시 UI 초기화
        SetActiveUI(mainUI, false);
        SetActiveUI(inGameUI, true);
        SetActiveUI(pauseUI, false);
        SetActiveUI(endUI, false);

        gameTime = 0f; // 게임 시간 초기화
        isGameActive = true; // 게임 활성화
        isPaused = false; // 일시정지 해제

        // 시간 초기화
        if (timeText != null)
        {
            timeText.text = "Time: 0.00 s";
        }
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            // 게임 재개
            SetActiveUI(pauseUI, false); // 일시정지 화면 숨김
            isPaused = false; // 일시정지 해제
        }
        else
        {
            // 게임 일시정지
            SetActiveUI(pauseUI, true); // 일시정지 화면 표시
            isPaused = true; // 일시정지 활성화
        }
    }

    public void RestartGame()
    {
        // 모든 상태 초기화 및 메인 화면 복귀
        gameTime = 0f; // 게임 시간 초기화
        isGameActive = false; // 게임 상태 초기화
        isPaused = false; // 일시정지 상태 초기화

        // UI 초기화
        ShowMainUI();
    }

    public void ExitToEndScreen()
    {
        // End 화면 활성화
        SetActiveUI(pauseUI, false);
        SetActiveUI(inGameUI, false);
        SetActiveUI(mainUI, false);
        SetActiveUI(endUI, true);

        isGameActive = false; // 게임 상태 비활성화
        isPaused = false; // 일시정지 해제
        UpdateEndUI();
    }

    public void GameOver()
    {
        SetActiveUI(inGameUI, false);
        SetActiveUI(pauseUI, false);
        SetActiveUI(endUI, true);

        isGameActive = false;
        UpdateEndUI();
    }

    private void UpdateEndUI()
    {
        if (recordText != null)
        {
            recordText.text = $"Your Record: {gameTime:F2} s";
        }
        if (gameTime > bestRecord)
        {
            bestRecord = gameTime;
        }
        if (bestRecordText != null)
        {
            bestRecordText.text = $"Best Record: {bestRecord:F2} s";
        }
    }

    public void ShowMainUI()
    {
        // 메인 화면 UI 표시
        SetActiveUI(mainUI, true);
        SetActiveUI(inGameUI, false);
        SetActiveUI(pauseUI, false);
        SetActiveUI(endUI, false);

        isGameActive = false; // 게임 상태 초기화
        isPaused = false; // 일시정지 상태 초기화
    }

    public void StartButtonPressed()
    {
        StartGame();
    }

    private void SetActiveUI(GameObject uiElement, bool isActive)
    {
        if (uiElement != null)
        {
            uiElement.SetActive(isActive);
        }
    }
}
