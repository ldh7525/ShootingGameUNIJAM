using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay;

    // 초성, 중성, 종성 정의
    private readonly char[] 초성 = { 'ㄱ', 'ㄴ', 'ㄷ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅅ', 'ㅇ', 'ㅈ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };
    private readonly char[] 중성 = { 'ㅏ', 'ㅑ', 'ㅓ', 'ㅕ', 'ㅗ', 'ㅛ', 'ㅜ', 'ㅠ', 'ㅡ', 'ㅣ' };
    private readonly char[] 종성 = { ' ', 'ㄱ', 'ㄴ', 'ㄷ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅅ', 'ㅇ', 'ㅈ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };

    void Start()
    {
        // 예제 텍스트 출력
        string text = "안녕하세하세하세요!";
        StartCoroutine(DisplayTypingEffect(text));
    }

    IEnumerator DisplayTypingEffect(string text)
    {
        string displayedText = ""; // 완성된 텍스트

        foreach (char c in text)
        {
            if (IsHangul(c))
            {
                // 한글 조합 과정
                yield return StartCoroutine(TypingHangul(c, displayedText));
                displayedText += c; // 최종 글자 완성 후 누적
            }
            else
            {
                displayedText += c; // 비한글 문자 추가
                textDisplay.text = displayedText;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    IEnumerator TypingHangul(char targetChar, string prefix)
    {
        int unicode = targetChar - 0xAC00;

        // 초성, 중성, 종성 분리
        int 초성Index = unicode / (21 * 28);
        int 중성Index = (unicode % (21 * 28)) / 28;
        int 종성Index = unicode % 28;

        // 초성 입력
        string tempText = prefix + 초성[초성Index]; // 기존 텍스트 + 초성
        textDisplay.text = tempText;
        yield return new WaitForSeconds(0.1f);

        // 초성 + 중성 입력
        char 초중성 = (char)(0xAC00 + 초성Index * 21 * 28 + 중성Index * 28);
        tempText = prefix + 초중성; // 기존 텍스트 + 초성 + 중성
        textDisplay.text = tempText;
        yield return new WaitForSeconds(0.1f);

        // 초성 + 중성 + 종성 입력
        if (종성Index > 0)
        {
            char 완성글자 = (char)(0xAC00 + 초성Index * 21 * 28 + 중성Index * 28 + 종성Index);
            tempText = prefix + 완성글자; // 기존 텍스트 + 완성된 글자
            textDisplay.text = tempText;
            yield return new WaitForSeconds(0.1f);
        }
    }

    // 한글 여부 확인
    bool IsHangul(char c)
    {
        return c >= 0xAC00 && c <= 0xD7A3;
    }
}
    