using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PhaseManager;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> attackers;
    [SerializeField] private GameObject attacker;
    [SerializeField] private List<GameObject> parent;
    [SerializeField] private PhaseManager phaseManager;

    private bool phase1Changed = true;
    private bool phase2Changed = true;
    private bool phase3Changed = true;
    private bool phase4Changed = true;

    void Update()
    {
        switch (phaseManager.currentPhase)
        {
            case GamePhase.Phase1:
                p1();
                break;
            case GamePhase.Phase2:
                p2();
                break;
            case GamePhase.Phase3:
                p3();
                break;
            case GamePhase.Phase4:
                p4();
                break;
            case GamePhase.Phase5:
                p5();
                break;
        }
    }

    void p1()
    {
        if (phase1Changed)
        {
            attackers.Clear();
            var a = Instantiate(attacker, new Vector2(4, 0), Quaternion.identity, parent[Random.Range(0,parent.Count)].transform);
            attackers.Add(a);
            a.transform.rotation = Quaternion.identity;
            phase1Changed = false;
        }
    }

    void p2()
    {
        if (phase2Changed)
        {
            for (int i = 0; i < attackers.Count; i++)
            {
                Destroy(attackers[i].gameObject);
            }
            attackers.Clear();
            var a = Instantiate(attacker, new Vector2(4, 0), Quaternion.identity, parent[Random.Range(0, parent.Count)].transform);
            attackers.Add(a);
            a.transform.rotation = Quaternion.identity;
            phase2Changed = false;
        }
    }

    void p3()
    {
        if (phase3Changed)
        {
            for (int i = 0; i < attackers.Count; i++)
            {
                Destroy(attackers[i].gameObject);
            }
            attackers.Clear();
            var a = Instantiate(attacker, new Vector2(4, 0), Quaternion.identity, parent[Random.Range(0, parent.Count)].transform);
            attackers.Add(a);
            a.transform.rotation = Quaternion.identity;
            phase3Changed = false;
        }
    }

    void p4()
    {
        if (phase4Changed)
        {
            for (int i = 0; i < attackers.Count; i++)
            {
                Destroy(attackers[i].gameObject);
            }
            attackers.Clear();
            var a = Instantiate(attacker, new Vector2(4, 0), Quaternion.identity, parent[Random.Range(0, parent.Count)].transform);
            attackers.Add(a);
            a.transform.rotation = Quaternion.identity;
            phase4Changed = false;
        }
    }

    void p5()
    {
        if (phase4Changed)
        {
            for (int i = 0; i < attackers.Count; i++)
            {
                Destroy(attackers[i].gameObject);
            }
            attackers.Clear();
            var a = Instantiate(attacker, new Vector2(4, 0), Quaternion.identity, parent[Random.Range(0, parent.Count)].transform);
            attackers.Add(a);
            a.transform.rotation = Quaternion.identity;
            phase4Changed = false;
        }
    }

    public void ResetPhaseFlags()
    {
        phase1Changed = true;
        phase2Changed = true;
        phase3Changed = true;
        phase4Changed = true;
    }
}
