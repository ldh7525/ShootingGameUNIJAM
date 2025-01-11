using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay; // ����� TextMeshProUGUI

    // �ʼ�, �߼�, ���� ����
    private readonly char[] �ʼ� = { '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��' };
    private readonly char[] �߼� = { '��', '��', '��', '��', '��', '��', '��', '��', '��', '��' };
    private readonly char[] ���� = { ' ', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��' };

    void Start()
    {
        // ���� �ؽ�Ʈ ����
        string text = "�ȳ��ϼ���! ����Ƽ���� �ؽ�Ʈ�� ����մϴ�.";
        StartCoroutine(DisplayTypingEffect(text));
    }

    IEnumerator DisplayTypingEffect(string text)
    {
        string displayedText = ""; // ������ ��� �ؽ�Ʈ

        foreach (char c in text) // �� ���ھ� ó��
        {
            if (IsHangul(c)) // �ѱ����� Ȯ��
            {
                yield return StartCoroutine(TypingHangul(c, displayedText));
                displayedText += c; // �ϼ��� ���� ����
            }
            else
            {
                displayedText += c; // ���ѱ� ���� �߰�
                textDisplay.text = displayedText;
                yield return new WaitForSeconds(0.1f); // ��� �ð�
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

        // �ܰ� 1: �ʼ� �Է�
        string tempText = prefix + �ʼ�[�ʼ�Index];
        textDisplay.text = tempText;
        yield return new WaitForSeconds(0.1f);

        // �ܰ� 2: �ʼ� + �߼� �Է�
        char ���߼� = (char)(0xAC00 + �ʼ�Index * 21 * 28 + �߼�Index * 28);
        tempText = prefix + ���߼�;
        textDisplay.text = tempText;
        yield return new WaitForSeconds(0.1f);

        // �ܰ� 3: �ϼ� ���� �Է�
        if (����Index > 0)
        {
            // ������ �ִ� ���
            char �ϼ����� = (char)(0xAC00 + �ʼ�Index * 21 * 28 + �߼�Index * 28 + ����Index);
            tempText = prefix + �ϼ�����;
            textDisplay.text = tempText;
        }
        else
        {
            // ������ ���� ���, ���߼� ���� �״�� ����
            tempText = prefix + ���߼�;
            textDisplay.text = tempText;
            yield return new WaitForSeconds(0.1f); // ������ �ܰ� ��� �ð�
        }
    }

    // �ѱ� ���� Ȯ��
    bool IsHangul(char c)
    {
        return c >= 0xAC00 && c <= 0xD7A3;
    }
}
