using UnityEngine;

public class PhaseManager : MonoBehaviour
{
    // ���� ����� ���� 
    public enum GamePhase
    {
        Phase1, 
        Phase2, 
        Phase3, 
        Phase4, 
        Phase5  
    }

    public GamePhase currentPhase = GamePhase.Phase1; // �ʱ� ������ ����
    public float phase1Time;  // Phase 1 �ð�
    public float phase2Time;  // Phase 2 �ð�
    public float phase3Time;  // Phase 3 �ð�
    public float phase4Time;  // Phase 4 �ð�
    public float phase5Time;  // Phase 5 �ð� 
    private float phaseTimer; // ������ ���� �ð� ī����

    void Start()
    {
        SetPhase(GamePhase.Phase1); // ���� �� Phase 1�� ����
    }

    void Update()
    {
        UpdatePhase();
    }

    // ����� ������Ʈ ó��
    private void UpdatePhase()
    {
        if (phaseTimer > 0)
        {
            phaseTimer -= Time.deltaTime;
            return;
        }

        // ������ Ÿ�̸Ӱ� ������ �� ���� ������� ��ȯ
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

    // ������ ��ȯ
    public void SetPhase(GamePhase newPhase)
    {
        currentPhase = newPhase;

        // ������ ���� �� �ʱ�ȭ
        switch (currentPhase)
        {
            case GamePhase.Phase1:
                phaseTimer = phase1Time;
                Debug.Log("Phase 1 ����!");
                break;
            case GamePhase.Phase2:
                phaseTimer = phase2Time;
                Debug.Log("Phase 2 ����!");
                break;
            case GamePhase.Phase3:
                phaseTimer = phase3Time;
                Debug.Log("Phase 3 ����!");
                break;
            case GamePhase.Phase4:
                phaseTimer = phase4Time;
                Debug.Log("Phase 4 ����!");
                break;
            case GamePhase.Phase5:
                phaseTimer = phase5Time;
                Debug.Log("Phase 5 ����!");
                break;
        }
    }

    // ���� ������ ���� Ȯ��
    public bool IsCurrentPhase(GamePhase phase)
    {
        return currentPhase == phase;
    }
}
