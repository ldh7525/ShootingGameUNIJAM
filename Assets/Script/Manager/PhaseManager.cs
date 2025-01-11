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

    [Header("���� ���� �ν�����")]
    [SerializeField] private List<string> patternCombinationPhase1;
    [SerializeField] private List<string> patternCombinationPhase2;
    [SerializeField] private List<string> patternCombinationPhase3;
    [SerializeField] private List<string> patternCombinationPhase4;

    [Header("������ ��ȯ �ð� ����")]
    [SerializeField] private float phase1TransitionDelay;
    [SerializeField] private float phase2TransitionDelay;
    [SerializeField] private float phase3TransitionDelay;
    [SerializeField] private float phase4TransitionDelay;

    [Header("��� ��� ���� ����")]
    [SerializeField] private TypingEffect typingEffect;
    [SerializeField] private string phase1Dialogue = "������ 1�� ����Ǿ����ϴ�.";
    [SerializeField] private string phase2Dialogue = "������ 2�� �Ѿ�ϴ�.";
    [SerializeField] private string phase3Dialogue = "������ 3�� ���۵˴ϴ�.";
    [SerializeField] private string phase4Dialogue = "���� ������ �غ� ���Դϴ�.";

    [Space (10f)]
    [SerializeField] private TextMeshProUGUI timeText;
    private float timeFloat = 0f;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private Boss1 Boss1;
    [SerializeField] private Boss2 Boss2;
    [SerializeField] private Boss3 Boss3;
    [SerializeField] private Boss4 Boss4;

    void Start()
    {
        StartPhase(GamePhase.Phase3);
    }

    private void Update()
    {
        timeFloat += Time.deltaTime;
        timeText.text = timeFloat.ToString("F2");
    }

    void StartPhase(GamePhase phase)
    {
        currentPhase = phase;
        Debug.Log($"{phase} ����!");

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

        Debug.Log($"{currentPhase} �Ϸ�!");

        // ��� ���
        yield return StartCoroutine(typingEffect.DisplayTypingEffect(dialogue));

        // ��ȯ ������
        yield return new WaitForSeconds(transitionDelay);

        // ���� ������� ��ȯ
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
            Debug.LogError($"�߸��� ���� ����: {pattern}");
            yield break;
        }

        string bossName = parts[0].Trim();
        string patternName = parts[1].Trim();

        if (!int.TryParse(bossName, out int bossNumber))
        {
            Debug.LogError($"�߸��� ���� �̸�: {bossName}");
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
                Debug.LogError($"�߸��� ���� ��ȣ: {bossNumber}");
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
            Debug.LogError($"boss {boss}: Pattern{patternName}");
        }
    }
}
