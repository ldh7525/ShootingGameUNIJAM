using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay; // ����� TextMeshProUGUI
    [SerializeField] private GameObject bossStart;

    public bool isTypingComplete;

    // �ʼ�, �߼�, ���� ����
    private readonly char[] �ʼ� = { '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��' };
    private readonly char[] �߼� = { '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��' };
    private readonly char[] ���� = { ' ', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��', '��' };

    private bool isSkipping = false; // �ؽ�Ʈ ��ŵ ����

    void Start()
    {
        isTypingComplete = false;
    }

    public IEnumerator DisplayTypingEffect(string text)
    {
        isTypingComplete = false;
        isSkipping = false;
        string displayedText = "";

        foreach (char c in text)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSkipping = true;
                textDisplay.text = text;
                break;
            }

            displayedText += c;
            textDisplay.text = displayedText;
            yield return new WaitForSeconds(0.1f);
        }

        if (bossStart != null && !isSkipping)
        {
            yield return new WaitForSeconds(1f);
            bossStart.SetActive(false);
            textDisplay.text = "";
        }

        yield return new WaitForSeconds(2f);
        isTypingComplete = true;
    }

    /// <summary>
    /// ���ڰ� ��� ��µ� �� ���콺 Ŭ���� ��ٸ��� �ڷ�ƾ
    /// </summary>
    public IEnumerator DisplayTypingEffectWithPause(string text)
    {
        isTypingComplete = false;
        isSkipping = false;
        string displayedText = "";

        foreach (char c in text)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isSkipping = true;
                textDisplay.text = text;
                break;
            }

            displayedText += c;
            textDisplay.text = displayedText;
            yield return new WaitForSeconds(0.1f);
        }

        // ���콺 Ŭ�� �Ǵ� 5�� Ÿ�Ӿƿ��� ��ٸ�
        yield return StartCoroutine(WaitForMouseClickOrTimeout());

        if (bossStart != null && !isSkipping)
        {
            yield return new WaitForSeconds(1f);
            bossStart.SetActive(false);
            textDisplay.text = "";
        }

        isTypingComplete = true;
    }

    private IEnumerator WaitForMouseClickOrTimeout(float timeout = 3f)
    {
        Debug.Log("���콺 Ŭ�� �Ǵ� 5�� ��⸦ �����մϴ�...");

        float elapsedTime = 0f;

        while (!Input.GetKeyDown(KeyCode.Space) && elapsedTime < timeout)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("���콺 Ŭ�� ����!");
        }
        else
        {
            Debug.Log("5�� Ÿ�Ӿƿ�!");
        }
    }

    private IEnumerator WaitForMouseClick()
    {
        Debug.Log("���콺 Ŭ���� ��ٸ��� ��...");

        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }

        Debug.Log("���콺 Ŭ�� ����!");
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
        yield return new WaitForSeconds(0.05f);

        // �ܰ� 2: �ʼ� + �߼� �Է�
        char ���߼� = (char)(0xAC00 + �ʼ�Index * 21 * 28 + �߼�Index * 28);
        tempText = prefix + ���߼�;
        textDisplay.text = tempText;
        yield return new WaitForSeconds(0.05f);

        // �ܰ� 3: �ϼ� ���� �Է�
        if (����Index > 0)
        {
            char �ϼ����� = (char)(0xAC00 + �ʼ�Index * 21 * 28 + �߼�Index * 28 + ����Index);
            tempText = prefix + �ϼ�����;
            textDisplay.text = tempText;
        }
        else
        {
            tempText = prefix + ���߼�;
            textDisplay.text = tempText;
        }

        yield return new WaitForSeconds(0.05f);
    }

    // �ѱ� ���� Ȯ��
    bool IsHangul(char c)
    {
        return c >= 0xAC00 && c <= 0xD7A3;
    }

}
