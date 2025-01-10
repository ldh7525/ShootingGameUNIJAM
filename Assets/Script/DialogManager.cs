using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel; // 대화창 패널 (Image + Text)
    public TextMeshProUGUI dialogueText; // 대화창 텍스트

    private bool isDialogueActive = false; // 대화창 활성화 여부

    void Start()
    {
        // 대화창 초기 비활성화
        //dialoguePanel.SetActive(false);
    }

    void Update()
    {
        // 예시: 스페이스바를 눌러 대화창 활성화/비활성화
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleDialogue("대화창이 나타났습니다!");
            print("ads");
        }
    }

    // 대화창 토글 메서드
    public void ToggleDialogue(string message)
    {
        isDialogueActive = !isDialogueActive;
        dialoguePanel.SetActive(isDialogueActive);

        if (isDialogueActive)
        {
            // 텍스트 및 이미지 설정
            dialogueText.text = message;
        }
    }
}
