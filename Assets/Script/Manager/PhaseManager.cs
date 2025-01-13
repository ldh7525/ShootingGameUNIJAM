using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PhaseManager : MonoBehaviour
{
    public enum GamePhase
    {
        Phase1,
        Phase2,
        Phase3,
        Phase4,
        Phase5
    }

    public GamePhase currentPhase;

    [Header("패턴 조합 인스펙터")]
    [SerializeField] private List<string> patternCombinationPhase1;
    [SerializeField] private List<string> patternCombinationPhase2;
    [SerializeField] private List<string> patternCombinationPhase3;
    [SerializeField] private List<string> patternCombinationPhase4;

    [Header("페이즈 전환 대기 시간")]
    [SerializeField] private float phase1TransitionDelay;
    [SerializeField] private float phase2TransitionDelay;
    [SerializeField] private float phase3TransitionDelay;
    [SerializeField] private float phase4TransitionDelay;

    [Header("페이즈 대사 출력 설정")]
    [SerializeField] private TypingEffect typingEffect;
    [SerializeField] public GameObject bosstalk;
    [SerializeField] private GameObject textBox;
    private TextMeshProUGUI textBoxText;
    [SerializeField] private string boss1Script1;
    [SerializeField] private string boss1Script2;
    [SerializeField] private string boss2Script1;
    [SerializeField] private string boss2Script2;
    [SerializeField] private string boss3Script1;
    [SerializeField] private string boss3Script2;
    [SerializeField] private string boss4Script1;
    [SerializeField] private string boss4Script2;


    [Header("페이즈 시작시 등장")]
    [SerializeField] private GameObject bossStart;

    [SerializeField] private GameObject phaseStartTextBox;
    private TextMeshProUGUI phaseStartText;
    [SerializeField] private string phase1Dialogue = "나랑 세계 최고의 보물을 훔치기로 했잖아!";
    [SerializeField] private string phase2Dialogue = "잡히면 나랑 결혼하는거다?";
    [SerializeField] private string phase3Dialogue = "페이즈 3이 활성화됩니다.";
    [SerializeField] private string phase4Dialogue = "지금 뭐하고 계시는 건가요?";

    [Header("게임 시작시 등장")]
    [SerializeField] private GameObject startPannel;
    [SerializeField] private string openingScript;
    private TextMeshProUGUI startText;

    [Space(10f)]
    [SerializeField] private TextMeshProUGUI timeText;
    private float timeFloat = 0f;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private Boss1 Boss1;
    [SerializeField] private Boss2 Boss2;
    [SerializeField] private Boss3 Boss3;
    [SerializeField] private Boss4 Boss4;

    public bool isStart = true;
    private bool isScript = false;

    void Start()
    {
        isStart = PlayerPrefs.GetInt("IsStart") == 1;   
        Debug.Log(isStart);
        SoundManager.Instance.StageBgmOn();
        phaseStartText = phaseStartTextBox.GetComponentInChildren<TextMeshProUGUI>();
        startText = startPannel.GetComponentInChildren<TextMeshProUGUI>();
        textBoxText = textBox.GetComponentInChildren<TextMeshProUGUI>();
        StartCoroutine(StartPhase(currentPhase));
    }

    private void Update()
    {
        if(!isScript)
        {
            timeFloat += Time.deltaTime;
        }
        timeText.text = timeFloat.ToString("F2");
    }
    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("IsStart",1);
    }

    IEnumerator StartPhase(GamePhase phase)
    {
        currentPhase = phase;
        Debug.Log($"{phase} 시작!");
        Debug.Log(isStart);
        if(isStart)
        {
            isScript = true;
            yield return StartCoroutine(typingEffect.DisplayTypingEffect(startPannel, startText, openingScript));
            isScript = false;
        }
        switch (phase)
        {
            case GamePhase.Phase1:
                Boss1.gameObject.SetActive(true);
                StartCoroutine(PhaseRoutine(patternCombinationPhase1, phase1TransitionDelay, phase1Dialogue));
                break;
            case GamePhase.Phase2:
                Boss2.gameObject.SetActive(true);
                StartCoroutine(PhaseRoutine(patternCombinationPhase2, phase2TransitionDelay, phase2Dialogue));
                break;
            case GamePhase.Phase3:
                Boss3.gameObject.SetActive(true);
                StartCoroutine(PhaseRoutine(patternCombinationPhase3, phase3TransitionDelay, phase3Dialogue));
                break;
            case GamePhase.Phase4:
                Boss4.gameObject.SetActive(true);
                StartCoroutine(PhaseRoutine(patternCombinationPhase4, phase4TransitionDelay, phase4Dialogue));
                break;
            case GamePhase.Phase5:
                gameManager.isGameClear = true;
                break;
        }
        PlayerPrefs.SetInt("IsStart", 0);
        isStart = false;
    }

    IEnumerator PhaseRoutine(List<string> patternCombination, float transitionDelay, string dialogue)
    {
        int count = 0;

        // 페이즈 대사 출력
        isScript = true;
        yield return StartCoroutine(typingEffect.DisplayTypingEffect(bossStart, phaseStartText, dialogue));
        isScript = false;
        yield return new WaitForSeconds(1f);
        foreach (string pattern in patternCombination)
        {
            if (count == 2)
            {
                StartCoroutine(EnemyTalkController(count));
            }
            else if (count == 4)
            {
                StartCoroutine(EnemyTalkController(count));
            }
            string[] splitPatterns = pattern.Split('/');
            List<Coroutine> coroutines = new List<Coroutine>();

            foreach (string subPattern in splitPatterns)
            {
                coroutines.Add(StartCoroutine(ExecutePattern(subPattern)));
            }

            foreach (Coroutine coroutine in coroutines)
            {
                yield return coroutine;
            }
            count++;
        }

        Debug.Log($"{currentPhase} 완료!");

        // 전환 대기 시간
        yield return new WaitForSeconds(transitionDelay);

        // 다음 페이즈로 전환
        if (currentPhase < GamePhase.Phase5)
        {
            StartCoroutine(StartPhase(currentPhase + 1));
        }
        else
        {
            gameManager.isGameClear = true;
        }
    }

    IEnumerator EnemyTalkController(int count)
    {
        switch(currentPhase)
        {
            case GamePhase.Phase1:
                bosstalk.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/Character/Detail/Boss1");
                if (count == 2)
                    textBoxText.text = boss1Script1;
                else if (count == 4)
                    textBoxText.text = boss1Script2;
                break;
            case GamePhase.Phase2:
                bosstalk.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/Character/Detail/Boss2");
                if (count == 2)
                    textBoxText.text = boss2Script1;
                else if (count == 4)
                    textBoxText.text = boss2Script2;
                break;
            case GamePhase.Phase3:
                bosstalk.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/Character/Detail/Boss3");
                if (count == 2)
                    textBoxText.text = boss3Script1;
                else if (count == 4)
                    textBoxText.text = boss3Script2;
                break;
            case GamePhase.Phase4:
                bosstalk.GetComponent<Image>().sprite = Resources.Load<Sprite>($"Sprites/Character/Detail/Boss4");
                if (count == 2)
                    textBoxText.text = boss4Script1;
                else if (count == 4)
                    textBoxText.text = boss4Script2;
                break;
        }
        bosstalk.SetActive(true);

        yield return new WaitForSeconds(3f);

        bosstalk.SetActive(false);
    }

    IEnumerator ExecutePattern(string pattern)
    {
        string[] parts = pattern.Split('-');
        if (parts.Length != 2)
        {
            Debug.LogError($"잘못된 패턴 형식: {pattern}");
            yield break;
        }

        string bossName = parts[0].Trim();
        string patternName = parts[1].Trim();

        if (!int.TryParse(bossName, out int bossNumber))
        {
            Debug.LogError($"잘못된 보스 이름: {bossName}");
            yield break;
        }

        switch (bossNumber)
        {
            case 1:
                yield return StartCoroutine(InvokeBossPattern(Boss1, patternName));
                break;
            case 2:
                yield return StartCoroutine(InvokeBossPattern(Boss2, patternName));
                break;
            case 3:
                yield return StartCoroutine(InvokeBossPattern(Boss3, patternName));
                break;
            case 4:
                yield return StartCoroutine(InvokeBossPattern(Boss4, patternName));
                break;
            default:
                Debug.LogError($"잘못된 보스 번호: {bossNumber}");
                break;
        }
    }

    IEnumerator InvokeBossPattern(object boss, string patternName)
    {
        var method = boss.GetType().GetMethod($"Pattern{patternName}");
        if (method != null && method.ReturnType == typeof(IEnumerator))
        {
            yield return (IEnumerator)method.Invoke(boss, null);
        }
        else
        {
            Debug.LogError($"보스 {boss}: Pattern{patternName} 메서드가 없습니다.");
        }
    }

    public List<string> GetCurrentPatternCombination()
    {
        switch (currentPhase)
        {
            case GamePhase.Phase1: return patternCombinationPhase1;
            case GamePhase.Phase2: return patternCombinationPhase2;
            case GamePhase.Phase3: return patternCombinationPhase3;
            case GamePhase.Phase4: return patternCombinationPhase4;
            default: return new List<string>();
        }
    }
}
