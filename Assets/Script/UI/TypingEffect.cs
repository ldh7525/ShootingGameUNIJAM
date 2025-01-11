using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay; // 출력할 TextMeshProUGUI

    // 초성, 중성, 종성 정의
    private readonly char[] 초성 = { 'ㄱ', 'ㄴ', 'ㄷ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅅ', 'ㅇ', 'ㅈ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };
    private readonly char[] 중성 = { 'ㅏ', 'ㅑ', 'ㅓ', 'ㅕ', 'ㅗ', 'ㅛ', 'ㅜ', 'ㅠ', 'ㅡ', 'ㅣ' };
    private readonly char[] 종성 = { ' ', 'ㄱ', 'ㄴ', 'ㄷ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅅ', 'ㅇ', 'ㅈ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };

    void Start()
    {
        // 예제 텍스트 설정
        string text = "안녕하세요! 유니티에서 텍스트를 출력합니다.";
        StartCoroutine(DisplayTypingEffect(text));
    }

    IEnumerator DisplayTypingEffect(string text)
    {
        string displayedText = ""; // 누적된 출력 텍스트

        foreach (char c in text) // 한 글자씩 처리
        {
            if (IsHangul(c)) // 한글인지 확인
            {
                yield return StartCoroutine(TypingHangul(c, displayedText));
                displayedText += c; // 완성된 글자 누적
            }
            else
            {
                displayedText += c; // 비한글 문자 추가
                textDisplay.text = displayedText;
                yield return new WaitForSeconds(0.1f); // 대기 시간
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

        // 단계 1: 초성 입력
        string tempText = prefix + 초성[초성Index];
        textDisplay.text = tempText;
        yield return new WaitForSeconds(0.1f);

        // 단계 2: 초성 + 중성 입력
        char 초중성 = (char)(0xAC00 + 초성Index * 21 * 28 + 중성Index * 28);
        tempText = prefix + 초중성;
        textDisplay.text = tempText;
        yield return new WaitForSeconds(0.1f);

        // 단계 3: 완성 글자 입력
        if (종성Index > 0)
        {
            // 종성이 있는 경우
            char 완성글자 = (char)(0xAC00 + 초성Index * 21 * 28 + 중성Index * 28 + 종성Index);
            tempText = prefix + 완성글자;
            textDisplay.text = tempText;
        }
        else
        {
            // 종성이 없는 경우, 초중성 상태 그대로 유지
            tempText = prefix + 초중성;
            textDisplay.text = tempText;
            yield return new WaitForSeconds(0.1f); // 마지막 단계 대기 시간
        }
    }

    // 한글 여부 확인
    bool IsHangul(char c)
    {
        return c >= 0xAC00 && c <= 0xD7A3;
    }
}
