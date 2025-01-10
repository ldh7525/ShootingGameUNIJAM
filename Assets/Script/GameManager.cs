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
        // ���� ���� �� UI �ʱ�ȭ
        SetActiveUI(mainUI, false);
        SetActiveUI(inGameUI, true);
        SetActiveUI(pauseUI, false);
        SetActiveUI(endUI, false);

        gameTime = 0f; // ���� �ð� �ʱ�ȭ
        isGameActive = true; // ���� Ȱ��ȭ
        isPaused = false; // �Ͻ����� ����

        // �ð� �ʱ�ȭ
        if (timeText != null)
        {
            timeText.text = "Time: 0.00 s";
        }
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            // ���� �簳
            SetActiveUI(pauseUI, false); // �Ͻ����� ȭ�� ����
            isPaused = false; // �Ͻ����� ����
        }
        else
        {
            // ���� �Ͻ�����
            SetActiveUI(pauseUI, true); // �Ͻ����� ȭ�� ǥ��
            isPaused = true; // �Ͻ����� Ȱ��ȭ
        }
    }

    public void RestartGame()
    {
        // ��� ���� �ʱ�ȭ �� ���� ȭ�� ����
        gameTime = 0f; // ���� �ð� �ʱ�ȭ
        isGameActive = false; // ���� ���� �ʱ�ȭ
        isPaused = false; // �Ͻ����� ���� �ʱ�ȭ

        // UI �ʱ�ȭ
        ShowMainUI();
    }

    public void ExitToEndScreen()
    {
        // End ȭ�� Ȱ��ȭ
        SetActiveUI(pauseUI, false);
        SetActiveUI(inGameUI, false);
        SetActiveUI(mainUI, false);
        SetActiveUI(endUI, true);

        isGameActive = false; // ���� ���� ��Ȱ��ȭ
        isPaused = false; // �Ͻ����� ����
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
        // ���� ȭ�� UI ǥ��
        SetActiveUI(mainUI, true);
        SetActiveUI(inGameUI, false);
        SetActiveUI(pauseUI, false);
        SetActiveUI(endUI, false);

        isGameActive = false; // ���� ���� �ʱ�ȭ
        isPaused = false; // �Ͻ����� ���� �ʱ�ȭ
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
