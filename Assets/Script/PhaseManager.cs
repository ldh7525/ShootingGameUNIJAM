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
    public float phase1Time;  // Phase 1 시간
    public float phase2Time;  // Phase 2 시간
    public float phase3Time;  // Phase 3 시간
    public float phase4Time;  // Phase 4 시간
    public float phase5Time;  // Phase 5 시간 
    private float phaseTimer; // 페이즈 지속 시간 카운터

    void Start()
    {
        SetPhase(GamePhase.Phase1); // 시작 시 Phase 1로 설정
    }

    void Update()
    {
        UpdatePhase();
    }

    // 페이즈별 업데이트 처리
    private void UpdatePhase()
    {
        if (phaseTimer > 0)
        {
            phaseTimer -= Time.deltaTime;
            return;
        }

        // 페이즈 타이머가 끝났을 때 다음 페이즈로 전환
        switch (currentPhase)
        {
            case GamePhase.Phase1:
                SetPhase(GamePhase.Phase2);
                break;
            case GamePhase.Phase2:
                SetPhase(GamePhase.Phase3);
                break;
            case GamePhase.Phase3:
                SetPhase(GamePhase.Phase4);
                break;
            case GamePhase.Phase4:
                SetPhase(GamePhase.Phase5);
                break;
        }
    }

    // 페이즈 전환
    public void SetPhase(GamePhase newPhase)
    {
        currentPhase = newPhase;

        // 페이즈 시작 시 초기화
        switch (currentPhase)
        {
            case GamePhase.Phase1:
                phaseTimer = phase1Time;
                Debug.Log("Phase 1 시작!");
                break;
            case GamePhase.Phase2:
                phaseTimer = phase2Time;
                Debug.Log("Phase 2 시작!");
                break;
            case GamePhase.Phase3:
                phaseTimer = phase3Time;
                Debug.Log("Phase 3 시작!");
                break;
            case GamePhase.Phase4:
                phaseTimer = phase4Time;
                Debug.Log("Phase 4 시작!");
                break;
            case GamePhase.Phase5:
                phaseTimer = phase5Time;
                Debug.Log("Phase 5 시작!");
                break;
        }
    }

    // 현재 페이즈 상태 확인
    public bool IsCurrentPhase(GamePhase phase)
    {
        return currentPhase == phase;
    }
}
