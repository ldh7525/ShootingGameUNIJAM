using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
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
    [Tooltip("1-2/2-3 이렇게 입력하면 보스1의 2번째 패턴, 보스2의 3번째 패턴이 동시에 실행 *공백 들어가면 망함*")]
    [SerializeField] private List<string> patternCombinationPhase1;
    [Tooltip("1-2/2-3 이렇게 입력하면 보스1의 2번째 패턴, 보스2의 3번째 패턴이 동시에 실행 *공백 들어가면 망함*")]
    [SerializeField] private List<string> patternCombinationPhase2;
    [Tooltip("1-2/2-3 이렇게 입력하면 보스1의 2번째 패턴, 보스2의 3번째 패턴이 동시에 실행 *공백 들어가면 망함*")]
    [SerializeField] private List<string> patternCombinationPhase3;
    [Tooltip("1-2/2-3 이렇게 입력하면 보스1의 2번째 패턴, 보스2의 3번째 패턴이 동시에 실행 *공백 들어가면 망함*")]
    [SerializeField] private List<string> patternCombinationPhase4;

    [Space (10f)]
    [SerializeField] private TextMeshProUGUI timeText;
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
                StartCoroutine(PhaseRoutine(patternCombinationPhase1));
                break;
            case GamePhase.Phase2:
                StartCoroutine(PhaseRoutine(patternCombinationPhase2));
                break;
            case GamePhase.Phase3:
                StartCoroutine(PhaseRoutine(patternCombinationPhase3));
                break;
            case GamePhase.Phase4:
                StartCoroutine(PhaseRoutine(patternCombinationPhase4)); // 반복 사용 예시
                break;
            case GamePhase.Phase5:
                break;
        }
    }

    IEnumerator PhaseRoutine(List<string> patternCombination)
    {
        foreach (string pattern in patternCombination)
        {
            // "/"로 패턴 그룹을 나누고 동시에 실행
            string[] splitPatterns = pattern.Split('/');
            List<Coroutine> coroutines = new List<Coroutine>();

            foreach (string subPattern in splitPatterns)
            {
                coroutines.Add(StartCoroutine(ExecutePattern(subPattern)));
            }

            // 모든 코루틴이 완료될 때까지 대기
            foreach (Coroutine coroutine in coroutines)
            {
                yield return coroutine;
            }
        }

        Debug.Log($"{currentPhase} 완료!");

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
}