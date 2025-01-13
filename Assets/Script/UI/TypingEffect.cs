using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public bool isTypingComplete;
    private bool isSkipping = false; // 텍스트 스킵 여부
    private IEnumerator currentCoroutine;

    public IEnumerator DisplayTypingEffect(GameObject scriptPanel, TextMeshProUGUI textDisplay, string text)
    {
        scriptPanel.SetActive(true);
        isTypingComplete = false;
        isSkipping = false;
        string displayedText = "";

        // 텍스트 출력 코루틴과 입력 감지를 동시에 실행
        Coroutine inputCoroutine = StartCoroutine(CheckForSkipInput(textDisplay, text));

        foreach (char c in text)
        {
            if (isSkipping) // 스킵 조건 확인
            {
                textDisplay.text = text;
                break;
            }

            displayedText += c;
            textDisplay.text = displayedText;
            yield return new WaitForSeconds(0.1f);
        }

        // 입력 감지 코루틴 종료
        StopCoroutine(inputCoroutine);

        // 텍스트 출력이 완료된 후 패널 비활성화
        yield return new WaitForSeconds(1f);
        scriptPanel.SetActive(false);
        textDisplay.text = "";
        isTypingComplete = true;
    }

    /// <summary>
    /// 스페이스바 입력을 감지하고 스킵 여부를 설정하는 코루틴
    /// </summary>
    private IEnumerator CheckForSkipInput(TextMeshProUGUI textDisplay, string text)
    {
        while (!isTypingComplete)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isSkipping = true;
                textDisplay.text = text; // 전체 대사 출력
                break;
            }
            yield return null;
        }
    }
}
