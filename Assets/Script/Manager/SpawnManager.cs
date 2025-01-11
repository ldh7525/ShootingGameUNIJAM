using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PhaseManager;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> bosses; // ���� ������ ���� ������ ����Ʈ
    [SerializeField] private List<GameObject> activeBosses; // Ȱ��ȭ�� ���� ��ü ����Ʈ
    [SerializeField] private List<GameObject> parent; // �θ� ������Ʈ ����Ʈ
    [SerializeField] private PhaseManager phaseManager; // PhaseManager ����

    private Dictionary<GamePhase, bool> phaseFlags; // ����� ���� �÷���

    // ���� �̸��� ��ġ�� �����ϴ� Dictionary
    private readonly Dictionary<string, Vector2> bossSpawnPositions = new Dictionary<string, Vector2>
    {
        { "Boss1", new Vector2(0, -4) },
        { "Boss2", new Vector2(4, 0) },
        { "Boss3", new Vector2(-4, 0) },
        { "Boss4", new Vector2(0, 4) }
    };

    void Start()
    {
        InitializePhaseFlags();
    }

    void Update()
    {
        // ���� ����� ���� ���� ����
        HandlePhase(phaseManager.currentPhase);
    }

    void InitializePhaseFlags()
    {
        phaseFlags = new Dictionary<GamePhase, bool>
        {
            { GamePhase.Phase1, true },
            { GamePhase.Phase2, true },
            { GamePhase.Phase3, true },
            { GamePhase.Phase4, true },
            { GamePhase.Phase5, true }
        };
    }

    void HandlePhase(GamePhase phase)
    {
        // �̹� ó���� �������� ����
        if (!phaseFlags[phase])
            return;

        // ���� ����� �ش��ϴ� ������ ����
        ManageBossesForPhase(phase);

        // �÷��� ������Ʈ
        phaseFlags[phase] = false;
    }

    void ManageBossesForPhase(GamePhase phase)
    {
        // PhaseManager���� ���� ���� ���� ���� ����
        string bossName = GetCurrentBossName(phaseManager);

        if (string.IsNullOrEmpty(bossName))
        {
            Debug.LogError("Boss name is invalid.");
            return;
        }

        // ���� ����
        SpawnBoss(bossName);

        // ���� ����
        DestroyInactiveBosses(bossName);
    }

    void SpawnBoss(string bossName)
    {
        // `bossName`�� �ش��ϴ� ������ �̹� Ȱ��ȭ�Ǿ� �ִٸ� ����
        if (activeBosses.Exists(boss => boss.name == bossName))
            return;

        // �ش� �̸��� ���� �������� ã�� ����
        GameObject bossPrefab = bosses.Find(boss => boss.name == bossName);
        if (bossPrefab != null)
        {
            // ���� ��ġ ����
            Vector2 spawnPosition = bossSpawnPositions.ContainsKey(bossName)
                ? bossSpawnPositions[bossName]
                : Vector2.zero; // �⺻�� (0,0)

            // �θ� ������Ʈ ����Ʈ���� �������� ����
            GameObject selectedParent = parent[Random.Range(0, parent.Count)];

            // ���õ� �θ��� �ڽ����� ����
            var newBoss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity, selectedParent.transform);
            newBoss.name = bossPrefab.name; // ������ ���� �̸� ����ȭ
            activeBosses.Add(newBoss);
            Debug.Log($"{bossName} ������ {selectedParent.name}�� �ڽ����� �����Ǿ����ϴ�. ��ġ: {spawnPosition}");
        }
        else
        {
            Debug.LogError($"���� ������ '{bossName}'�� ã�� �� �����ϴ�.");
        }
    }

    void DestroyInactiveBosses(string activeBossName)
    {
        // Ȱ��ȭ�� ���� �� ���� Ȱ�� ������ �ƴ� ���� ����
        for (int i = activeBosses.Count - 1; i >= 0; i--)
        {
            if (activeBosses[i].name != activeBossName)
            {
                Debug.Log($"��Ȱ��ȭ�� ���� {activeBosses[i].name} ���ŵ�.");
                Destroy(activeBosses[i]);
                activeBosses.RemoveAt(i);
            }
        }
    }

    string GetCurrentBossName(PhaseManager phaseManager)
    {
        // ���� ���� ���� ���Ͽ��� bossName ����
        foreach (string pattern in phaseManager.GetCurrentPatternCombination())
        {
            string[] parts = pattern.Split('-');
            if (parts.Length > 0)
            {
                return parts[0].Trim(); // bossName ��ȯ
            }
        }
        return string.Empty;
    }
}
