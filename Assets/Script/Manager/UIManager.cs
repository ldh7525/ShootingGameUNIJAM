using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("���� ��ũ��Ʈ��")]
    [SerializeField] private GameObject StartPanel;
    // On on Start
    [Header ("�������� ���۽� �׻� Ȱ��ȭ")]
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject heart;
    [SerializeField] private GameObject stamina;

    // Not Always On
    [Header ("Ư�� ��Ȳ�� ���")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;

    //Manager
    [Header("�Ŵ���")]
    [SerializeField] private GameObject phaseManager;
    [SerializeField] private GameObject spawbManager;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject player;

    [Header ("����")]
    [SerializeField] private bool isDialogueActive = false; // ��ȭâ Ȱ��ȭ ����
    [SerializeField] private bool isPaused = true;

    [Header("������ ��ũ��Ʈ")]
    [SerializeField] private string openingScript;

    private FadeController fade;
    private TypingEffect te;
    private bool isOpening;
    private void Start()
    {
        if (dialoguePanel != null)
        {
            dialogueText = dialoguePanel.GetComponentInChildren<TextMeshProUGUI>();
        }
        Time.timeScale = 1f;
        fade = GetComponent<FadeController>();
        te = GetComponent<TypingEffect>();
    }

    void Update()
    {
        // ����: �����̽��ٸ� ���� ��ȭâ Ȱ��ȭ/��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleDialogue("��ȭâ�� ��Ÿ�����ϴ�!");
        }
        if(isOpening && Input.GetKeyUp(KeyCode.Escape))
        {
            StageStart();
            isOpening = false;
        }
        if(isOpening && te.isTypingComplete)
        {
            StageStart();
        }


        IsOver();
    }

    public void GameStart()
    {
        isOpening = true;
        StartButton.SetActive(false);
        StartPanel.SetActive(true);
        StartCoroutine(te.DisplayTypingEffect(openingScript));
    }

    public void StageStart()
    {
        StartPanel.SetActive(false);
        player.SetActive(true);
        phaseManager.SetActive(true);
        spawbManager.SetActive(true);
        pauseButton.SetActive(true);
        heart.SetActive(true);
        stamina.SetActive(true);
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
        if (gameManager.GetComponent<GameManager>().isGameEnd || gameManager.GetComponent<GameManager>().isGameClear)
        {
            Time.timeScale = 0f;
            fade.StartFadeOut();
            StartCoroutine(FadeTime());
        }
        
    }

    IEnumerator FadeTime()
    {
        yield return new WaitForSecondsRealtime(5f);

        if (gameManager.GetComponent<GameManager>().isGameEnd)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "Game Over";
        }
        else if (gameManager.GetComponent<GameManager>().isGameClear)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "Game Clear";
        }
        fade.StartFadeIn();
    }
    
}
