using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static PhaseManager;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    [Header("���� ��ũ��Ʈ��")]
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject StartText;
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
    [SerializeField] private GameObject gameOverImage;
    [SerializeField] private CameraShakeOnHit camShake;

    //Manager
    [Header("�Ŵ���")]
    [SerializeField] private GameObject phaseManager;
    [SerializeField] private GameObject gameManager;
    [SerializeField] private GameObject player;

    [Header ("����")]
    [SerializeField] private bool isDialogueActive = false; // ��ȭâ Ȱ��ȭ ����
    [SerializeField] private bool isPaused = true;

    private FadeController fade;
    private TypingEffect te;
    public bool isOpening;
    private void Start()
    {
        Time.timeScale = 1f;
        fade = GetComponent<FadeController>();
        te = GetComponent<TypingEffect>();
        if (!PlayerPrefs.HasKey("IsOpening"))
        {
            PlayerPrefs.SetInt("IsOpening", 1); // �⺻�� true
            PlayerPrefs.Save();
        }
        isOpening = PlayerPrefs.GetInt("IsOpening", 1) == 1;
        if (!isOpening)
        {
            StartButton.SetActive(false);
            StageStart();
        }
        
    }

    void Update()
    {
        IsOver();
    }

    public void GameStart()
    {
        StartButton.SetActive(false);
        StartPanel.SetActive(true);
        StartText.SetActive(true);
        StageStart();
    }

    public void StageStart()
    {
        isOpening = false;
        StartPanel.SetActive(false);
        player.SetActive(true);
        phaseManager.SetActive(true);
        pauseButton.SetActive(true);
        heart.SetActive(true);
        stamina.SetActive(true);
    }
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (pauseMenu != null) pauseMenu.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f; // �ð� ����
    }
    public void ReloadScene()
    {
        PlayerPrefs.SetInt("IsOpening", 0);
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
            phaseManager.GetComponent<PhaseManager>().bosstalk.SetActive(false);
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
    private void OnApplicationQuit()
    {
        // ����Ƽ �÷��̰� ����� �� isOpening ���� �ʱ�ȭ
        PlayerPrefs.SetInt("IsOpening", 1);
        PlayerPrefs.Save();
    }


}
