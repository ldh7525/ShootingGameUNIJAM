using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public bool isTypingComplete;
    private bool isSkipping = false; // �ؽ�Ʈ ��ŵ ����
    private IEnumerator currentCoroutine;

    public IEnumerator DisplayTypingEffect(GameObject scriptPanel, TextMeshProUGUI textDisplay, string text)
    {
        scriptPanel.SetActive(true);
        isTypingComplete = false;
        isSkipping = false;
        string displayedText = "";

        // �ؽ�Ʈ ��� �ڷ�ƾ�� �Է� ������ ���ÿ� ����
        Coroutine inputCoroutine = StartCoroutine(CheckForSkipInput(textDisplay, text));

        foreach (char c in text)
        {
            if (isSkipping) // ��ŵ ���� Ȯ��
            {
                textDisplay.text = text;
                break;
            }

            displayedText += c;
            textDisplay.text = displayedText;
            yield return new WaitForSeconds(0.1f);
        }

        // �Է� ���� �ڷ�ƾ ����
        StopCoroutine(inputCoroutine);

        // �ؽ�Ʈ ����� �Ϸ�� �� �г� ��Ȱ��ȭ
        yield return new WaitForSeconds(1f);
        scriptPanel.SetActive(false);
        textDisplay.text = "";
        isTypingComplete = true;
    }

    /// <summary>
    /// �����̽��� �Է��� �����ϰ� ��ŵ ���θ� �����ϴ� �ڷ�ƾ
    /// </summary>
    private IEnumerator CheckForSkipInput(TextMeshProUGUI textDisplay, string text)
    {
        while (!isTypingComplete)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isSkipping = true;
                textDisplay.text = text; // ��ü ��� ���
                break;
            }
            yield return null;
        }
    }
}
