using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    [Header("패턴 조절 인스펙터")]
    [SerializeField] private List<string> patternCombinationPhase1;
    [SerializeField] private List<string> patternCombinationPhase2;
    [SerializeField] private List<string> patternCombinationPhase3;
    [SerializeField] private List<string> patternCombinationPhase4;

    [Header("페이즈 전환 시간 설정")]
    [SerializeField] private float phase1TransitionDelay;
    [SerializeField] private float phase2TransitionDelay;
    [SerializeField] private float phase3TransitionDelay;
    [SerializeField] private float phase4TransitionDelay;

    [Header("대사 출력 관련 설정")]
    [SerializeField] private TypingEffect typingEffect;
    [SerializeField] private string phase1Dialogue = "페이즈 1이 종료되었습니다.";
    [SerializeField] private string phase2Dialogue = "페이즈 2로 넘어갑니다.";
    [SerializeField] private string phase3Dialogue = "페이즈 3이 시작됩니다.";
    [SerializeField] private string phase4Dialogue = "최종 페이즈 준비 중입니다.";

    [SerializeField] private GameManager gameManager;

    [SerializeField] private Boss1 Boss1;
    [SerializeField] private Boss2 Boss2;
    [SerializeField] private Boss3 Boss3;
    [SerializeField] private Boss4 Boss4;

    void Start()
    {
        StartPhase(GamePhase.Phase1);
    }

    void StartPhase(GamePhase phase)
    {
        currentPhase = phase;
        Debug.Log($"{phase} 시작!");

        switch (phase)
        {
            case GamePhase.Phase1:
                StartCoroutine(PhaseRoutine(patternCombinationPhase1, phase1TransitionDelay, phase1Dialogue));
                break;
            case GamePhase.Phase2:
                StartCoroutine(PhaseRoutine(patternCombinationPhase2, phase2TransitionDelay, phase2Dialogue));
                break;
            case GamePhase.Phase3:
                StartCoroutine(PhaseRoutine(patternCombinationPhase3, phase3TransitionDelay, phase3Dialogue));
                break;
            case GamePhase.Phase4:
                StartCoroutine(PhaseRoutine(patternCombinationPhase4, phase4TransitionDelay, phase4Dialogue));
                break;
            case GamePhase.Phase5:
                break;
        }
    }

    IEnumerator PhaseRoutine(List<string> patternCombination, float transitionDelay, string dialogue)
    {
        foreach (string pattern in patternCombination)
        {
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
        }

        Debug.Log($"{currentPhase} 완료!");

        // 대사 출력
        yield return StartCoroutine(typingEffect.DisplayTypingEffect(dialogue));

        // 전환 딜레이
        yield return new WaitForSeconds(transitionDelay);

        // 다음 페이즈로 전환
        if (currentPhase < GamePhase.Phase5)
        {
            StartPhase(currentPhase + 1);
        }
        else
        {
            gameManager.isGameClear = true;
        }
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
            Debug.LogError($"해당 패턴 메서드가 없습니다: Pattern{patternName}");
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
