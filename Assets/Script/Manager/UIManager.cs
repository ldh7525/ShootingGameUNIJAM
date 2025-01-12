using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static PhaseManager;

public class UIManager : MonoBehaviour
{
    [Header("시작 스크립트용")]
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject StartText;
    // On on Start
    [Header ("스테이지 시작시 항상 활성화")]
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject heart;
    [SerializeField] private GameObject stamina;

    // Not Always On
    [Header ("특정 상황에 사용")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject gameOverImage;
    //[SerializeField] private GameObject dialoguePanel;
    //[SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private CameraShakeOnHit camShake;

    //Manager
    [Header("매니저")]
    [SerializeField] private GameObject phaseManager;
    [SerializeField] private GameObject spawbManager;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject player;

    [Header ("변수")]
    [SerializeField] private bool isDialogueActive = false; // 대화창 활성화 여부
    [SerializeField] private bool isPaused = true;

    [Header("오프닝 스크립트")]
    [SerializeField] private string openingScript;

    private FadeController fade;
    private TypingEffect te;
    private bool isOpening;
    private void Start()
    {
        
        Time.timeScale = 1f;
        fade = GetComponent<FadeController>();
        te = GetComponent<TypingEffect>();
    }

    void Update()
    {
        // 예시: 스페이스바를 눌러 대화창 활성화/비활성화
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ToggleDialogue("대화창이 나타났습니다!");
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
        StartText.SetActive(true);
        StartCoroutine(te.DisplayTypingEffectWithPause(openingScript));
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

    // 대화창 토글 메서드
/*    public void ToggleDialogue(string message)
    {
        if (dialoguePanel == null) return;

        isDialogueActive = !isDialogueActive;
        dialoguePanel.SetActive(isDialogueActive);

        if (isDialogueActive && dialogueText != null)
        {
            // 텍스트 및 이미지 설정
            dialogueText.text = message;
        }
    }*/
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
        if (gameManager.GetComponent<GameManager>().isGameEnd || gameManager.GetComponent<GameManager>().isGameClear)
        {
            Time.timeScale = 0f;
            fade.StartFadeOut();
            StartCoroutine(FadeTime());
        }
        
    }

    IEnumerator FadeTime()
    {
        yield return new WaitForSecondsRealtime(3f);

        if (gameManager.GetComponent<GameManager>().isGameEnd)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "Game Over";
            GamePhase currentPhase = GameObject.Find("PhaseManager").GetComponent<PhaseManager>().currentPhase;
            switch(currentPhase)
            {
                case GamePhase.Phase1:
                    gameOverImage.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/Character/Detail/Boss1");
                    break;
                case GamePhase.Phase2:
                    gameOverImage.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/Character/Detail/Boss2");
                    break;
                case GamePhase.Phase3:
                    gameOverImage.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/Character/Detail/Boss3");
                    break;
                case GamePhase.Phase4:
                    gameOverImage.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/Character/Detail/Boss4");
                    break;
            }
            
        }
        else if (gameManager.GetComponent<GameManager>().isGameClear)
        {
            gameOverPanel.SetActive(true);
            gameOverText.text = "Game Clear";
            gameOverImage.GetComponent<Image>().sprite = Resources.Load<Sprite>($"img/ending_img");
        }
        camShake.StopAllCoroutines();
        fade.StartFadeIn();
    }
    
}
