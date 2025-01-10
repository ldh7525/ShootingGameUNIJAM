using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject pauseButton;
    public GameObject pauseMenu;

    public GameObject dialoguePanel; // ��ȭâ �г� (Image + Text)
    public TextMeshProUGUI dialogueText; // ��ȭâ �ؽ�Ʈ

    private bool isDialogueActive = false; // ��ȭâ Ȱ��ȭ ����
    private bool isPaused = true;

    private void Start()
    {
        // �ʿ��� GameObject���� Find�� ��������
        pauseButton = GameObject.Find("PauseButton");
        pauseMenu = GameObject.Find("PauseMenu");
        dialoguePanel = GameObject.Find("DialoguePanel");

        if (dialoguePanel != null)
        {
            dialogueText = dialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
        }

        // �ʱ� ���� ����
        if (pauseButton != null) pauseButton.SetActive(false);
        if (dialoguePanel != null) dialoguePanel.SetActive(false);

        pauseButton.SetActive(false);
        pauseMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    void Update()
    {
        // ����: �����̽��ٸ� ���� ��ȭâ Ȱ��ȭ/��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleDialogue("��ȭâ�� ��Ÿ�����ϴ�!");
        }
    }

    // ��ȭâ ��� �޼���
    public void ToggleDialogue(string message)
    {
        if (dialoguePanel == null) return;

        isDialogueActive = !isDialogueActive;
        dialoguePanel.SetActive(isDialogueActive);

        if (isDialogueActive && dialogueText != null)
        {
            // �ؽ�Ʈ �� �̹��� ����
            dialogueText.text = message;
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (pauseMenu != null) pauseMenu.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f; // �ð� ����
    }
}
