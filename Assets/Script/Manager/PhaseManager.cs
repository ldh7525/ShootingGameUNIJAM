using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public float[] phaseTime = new float[5];  // Phase �ð� ���� �迭
    private int phaseIndex;
    private float phaseTimer; // ������ ���� �ð� ī����

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private Boss1 Boss1;
    [SerializeField] private GameObject Boss2;
    [SerializeField] private GameObject Boss3;
    [SerializeField] private GameObject Boss4;

    void Start()
    {
        StartPhase(GamePhase.Phase1); // Phase 1 ����
    }

    void StartPhase(GamePhase phase)
    {
        currentPhase = phase;
        Debug.Log($"{phase} ����!");

        // �� ����� �ڷ�ƾ ����
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
        // Phase 1���� ������ �۾�
        yield return StartCoroutine(Boss1.Pattern2()); // ��: Ư�� �۾��� ���
        Debug.Log("Phase 1 �Ϸ�!");

        StartPhase(GamePhase.Phase2); // ���� Phase�� ��ȯ
    }
    IEnumerator Phase2Routine()
    {
        // Phase 2���� ������ �۾�
        yield return StartCoroutine(SomeTask()); // �ڷ�ƾ �۾� ����
        Debug.Log("Phase 2 �Ϸ�!");

        StartPhase(GamePhase.Phase3); // ���� Phase�� ��ȯ
    }

    IEnumerator Phase3Routine()
    {
        // Phase 3���� ������ �۾�
        yield return new WaitForSeconds(3f);
        Debug.Log("Phase 3 �Ϸ�!");

        StartPhase(GamePhase.Phase4); // ���� Phase�� ��ȯ
    }

    IEnumerator Phase4Routine()
    {
        // Phase 4���� ������ �۾�
        yield return new WaitForSeconds(2f);
        Debug.Log("Phase 4 �Ϸ�!");

        StartPhase(GamePhase.Phase5); // ���� Phase�� ��ȯ
    }

    IEnumerator Phase5Routine()
    {
        // Phase 5���� ������ �۾�
        yield return new WaitForSeconds(1f);
        Debug.Log("Phase 5 �Ϸ�!");

        gameManager.isGameClear = true; // ���� ���� ó��
    }

    IEnumerator SomeTask()
    {
        Debug.Log("Phase 2: SomeTask ���� ��...");
        yield return new WaitForSeconds(1f); // ��: �۾� ���
        Debug.Log("Phase 2: SomeTask �Ϸ�!");
    }
}
