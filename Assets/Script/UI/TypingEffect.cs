using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;

    // �ʼ�, �߼�, ���� ����
    private readonly char[] �ʼ� = { '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��' };
    private readonly char[] �߼� = { '��', '��', '��', '��', '��', '��', '��', '��', '��', '��' };
    private readonly char[] ���� = { ' ', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��' };

    void Start()
    {
        // ���� �ؽ�Ʈ ���
        string text = "�ȳ��ϼ��ϼ��ϼ���!";
        StartCoroutine(DisplayTypingEffect(text));
    }

    IEnumerator DisplayTypingEffect(string text)
    {
        string displayedText = ""; // �ϼ��� �ؽ�Ʈ

        foreach (char c in text)
        {
            if (IsHangul(c))
            {
                // �ѱ� ���� ����
                yield return StartCoroutine(TypingHangul(c, displayedText));
                displayedText += c; // ���� ���� �ϼ� �� ����
            }
            else
            {
                displayedText += c; // ���ѱ� ���� �߰�
                textDisplay.text = displayedText;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    IEnumerator TypingHangul(char targetChar, string prefix)
    {
        int unicode = targetChar - 0xAC00;

        // �ʼ�, �߼�, ���� �и�
        int �ʼ�Index = unicode / (21 * 28);
        int �߼�Index = (unicode % (21 * 28)) / 28;
        int ����Index = unicode % 28;

        // �ʼ� �Է�
        string tempText = prefix + �ʼ�[�ʼ�Index]; // ���� �ؽ�Ʈ + �ʼ�
        textDisplay.text = tempText;
        yield return new WaitForSeconds(0.1f);

        // �ʼ� + �߼� �Է�
        char ���߼� = (char)(0xAC00 + �ʼ�Index * 21 * 28 + �߼�Index * 28);
        tempText = prefix + ���߼�; // ���� �ؽ�Ʈ + �ʼ� + �߼�
        textDisplay.text = tempText;
        yield return new WaitForSeconds(0.1f);

        // �ʼ� + �߼� + ���� �Է�
        if (����Index > 0)
        {
            char �ϼ����� = (char)(0xAC00 + �ʼ�Index * 21 * 28 + �߼�Index * 28 + ����Index);
            tempText = prefix + �ϼ�����; // ���� �ؽ�Ʈ + �ϼ��� ����
            textDisplay.text = tempText;
            yield return new WaitForSeconds(0.1f);
        }
    }

    // �ѱ� ���� Ȯ��
    bool IsHangul(char c)
    {
        return c >= 0xAC00 && c <= 0xD7A3;
    }
}
    