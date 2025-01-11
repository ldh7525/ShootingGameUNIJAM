using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    // 게임 페이즈를 정의 
    public enum GamePhase
    {
        Phase1,
        Phase2,
        Phase3,
        Phase4,
        Phase5
    }
    public GamePhase currentPhase = GamePhase.Phase1; // 초기 페이즈 설정
    public float[] phaseTime = new float[5];  // Phase 시간 저장 배열
    private int phaseIndex;
    private float phaseTimer; // 페이즈 지속 시간 카운터

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private Boss1 Boss1;
    [SerializeField] private GameObject Boss2;
    [SerializeField] private GameObject Boss3;
    [SerializeField] private GameObject Boss4;

    void Start()
    {
        StartPhase(GamePhase.Phase1); // Phase 1 시작
    }

    void StartPhase(GamePhase phase)
    {
        currentPhase = phase;
        Debug.Log($"{phase} 시작!");

        // 각 페이즈별 코루틴 실행
        switch (phase)
        {
            case GamePhase.Phase1:
                StartCoroutine(Phase1Routine());
                break;
            case GamePhase.Phase2:
                StartCoroutine(Phase2Routine());
                break;
            case GamePhase.Phase3:
                StartCoroutine(Phase3Routine());
                break;
            case GamePhase.Phase4:
                StartCoroutine(Phase4Routine());
                break;
            case GamePhase.Phase5:
                StartCoroutine(Phase5Routine());
                break;
        }
    }

    IEnumerator Phase1Routine()
    {
        // Phase 1에서 실행할 작업
        yield return StartCoroutine(Boss1.Pattern2()); // 예: 특정 작업을 대기
        Debug.Log("Phase 1 완료!");

        StartPhase(GamePhase.Phase2); // 다음 Phase로 전환
    }
    IEnumerator Phase2Routine()
    {
        // Phase 2에서 실행할 작업
        yield return StartCoroutine(SomeTask()); // 코루틴 작업 수행
        Debug.Log("Phase 2 완료!");

        StartPhase(GamePhase.Phase3); // 다음 Phase로 전환
    }

    IEnumerator Phase3Routine()
    {
        // Phase 3에서 실행할 작업
        yield return new WaitForSeconds(3f);
        Debug.Log("Phase 3 완료!");

        StartPhase(GamePhase.Phase4); // 다음 Phase로 전환
    }

    IEnumerator Phase4Routine()
    {
        // Phase 4에서 실행할 작업
        yield return new WaitForSeconds(2f);
        Debug.Log("Phase 4 완료!");

        StartPhase(GamePhase.Phase5); // 다음 Phase로 전환
    }

    IEnumerator Phase5Routine()
    {
        // Phase 5에서 실행할 작업
        yield return new WaitForSeconds(1f);
        Debug.Log("Phase 5 완료!");

        gameManager.isGameClear = true; // 게임 종료 처리
    }

    IEnumerator SomeTask()
    {
        Debug.Log("Phase 2: SomeTask 실행 중...");
        yield return new WaitForSeconds(1f); // 예: 작업 대기
        Debug.Log("Phase 2: SomeTask 완료!");
    }
}
