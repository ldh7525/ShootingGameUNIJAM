using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PhaseManager;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> bosses; // 생성 가능한 보스 프리팹 리스트
    [SerializeField] private List<GameObject> activeBosses; // 활성화된 보스 객체 리스트
    [SerializeField] private List<GameObject> parent; // 부모 오브젝트 리스트
    [SerializeField] private PhaseManager phaseManager; // PhaseManager 참조

    private Dictionary<GamePhase, bool> phaseFlags; // 페이즈별 상태 플래그

    // 보스 이름별 위치를 저장하는 Dictionary
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
        // 현재 페이즈에 따라 보스 관리
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
        // 이미 처리된 페이즈라면 무시
        if (!phaseFlags[phase])
            return;

        // 현재 페이즈에 해당하는 보스를 관리
        ManageBossesForPhase(phase);

        // 플래그 업데이트
        phaseFlags[phase] = false;
    }

    void ManageBossesForPhase(GamePhase phase)
    {
        // PhaseManager에서 현재 실행 중인 패턴 추출
        string bossName = GetCurrentBossName(phaseManager);

        if (string.IsNullOrEmpty(bossName))
        {
            Debug.LogError("Boss name is invalid.");
            return;
        }

        // 보스 스폰
        SpawnBoss(bossName);

        // 보스 삭제
        DestroyInactiveBosses(bossName);
    }

    void SpawnBoss(string bossName)
    {
        // `bossName`에 해당하는 보스가 이미 활성화되어 있다면 무시
        if (activeBosses.Exists(boss => boss.name == bossName))
            return;

        // 해당 이름의 보스 프리팹을 찾아 스폰
        GameObject bossPrefab = bosses.Find(boss => boss.name == bossName);
        if (bossPrefab != null)
        {
            // 보스 위치 설정
            Vector2 spawnPosition = bossSpawnPositions.ContainsKey(bossName)
                ? bossSpawnPositions[bossName]
                : Vector2.zero; // 기본값 (0,0)

            // 부모 오브젝트 리스트에서 랜덤으로 선택
            GameObject selectedParent = parent[Random.Range(0, parent.Count)];

            // 선택된 부모의 자식으로 생성
            var newBoss = Instantiate(bossPrefab, spawnPosition, Quaternion.identity, selectedParent.transform);
            newBoss.name = bossPrefab.name; // 생성된 보스 이름 동기화
            activeBosses.Add(newBoss);
            Debug.Log($"{bossName} 보스가 {selectedParent.name}의 자식으로 생성되었습니다. 위치: {spawnPosition}");
        }
        else
        {
            Debug.LogError($"보스 프리팹 '{bossName}'을 찾을 수 없습니다.");
        }
    }

    void DestroyInactiveBosses(string activeBossName)
    {
        // 활성화된 보스 중 현재 활성 보스가 아닌 보스 제거
        for (int i = activeBosses.Count - 1; i >= 0; i--)
        {
            if (activeBosses[i].name != activeBossName)
            {
                Debug.Log($"비활성화된 보스 {activeBosses[i].name} 제거됨.");
                Destroy(activeBosses[i]);
                activeBosses.RemoveAt(i);
            }
        }
    }

    string GetCurrentBossName(PhaseManager phaseManager)
    {
        // 현재 실행 중인 패턴에서 bossName 추출
        foreach (string pattern in phaseManager.GetCurrentPatternCombination())
        {
            string[] parts = pattern.Split('-');
            if (parts.Length > 0)
            {
                return parts[0].Trim(); // bossName 반환
            }
        }
        return string.Empty;
    }
}
