using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel; // ��ȭâ �г� (Image + Text)
    public TextMeshProUGUI dialogueText; // ��ȭâ �ؽ�Ʈ

    private bool isDialogueActive = false; // ��ȭâ Ȱ��ȭ ����

    void Start()
    {
        // ��ȭâ �ʱ� ��Ȱ��ȭ
        //dialoguePanel.SetActive(false);
    }

    void Update()
    {
        // ����: �����̽��ٸ� ���� ��ȭâ Ȱ��ȭ/��Ȱ��ȭ
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleDialogue("��ȭâ�� ��Ÿ�����ϴ�!");
            print("ads");
        }
    }

    // ��ȭâ ��� �޼���
    public void ToggleDialogue(string message)
    {
        isDialogueActive = !isDialogueActive;
        dialoguePanel.SetActive(isDialogueActive);

        if (isDialogueActive)
        {
            // �ؽ�Ʈ �� �̹��� ����
            dialogueText.text = message;
        }
    }
}
