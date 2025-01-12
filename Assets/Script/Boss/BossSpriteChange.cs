using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpriteChange : MonoBehaviour
{

    [Header ("스프라이트")]
    [SerializeField] private PhaseManager phaseManager;
    [SerializeField] private Image character;
    [SerializeField] private List<Sprite> sprite;

    // Update is called once per frame
    void Update()
    {
        switch (phaseManager.currentPhase)
        {
            case PhaseManager.GamePhase.Phase1:
                character.sprite = sprite[0];
                break;
            case PhaseManager.GamePhase.Phase2:
                character.sprite = sprite[1];
                break;
            case PhaseManager.GamePhase.Phase3:
                character.sprite = sprite[2];
                break;
            case PhaseManager.GamePhase.Phase4:
                character.sprite = sprite[3];
                break;

        }
    }
}
