using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplay; // 출력할 TextMeshProUGUI
    [SerializeField] private GameObject bossStart;

    public bool isTypingComplete;

    // 초성, 중성, 종성 정의
    private readonly char[] 초성 = { 'ㄱ', 'ㄲ', 'ㄴ', 'ㄷ', 'ㄸ', 'ㄹ', 'ㅁ', 'ㅂ', 'ㅃ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅉ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };
    private readonly char[] 중성 = { 'ㅏ', 'ㅐ', 'ㅑ', 'ㅒ', 'ㅓ', 'ㅔ', 'ㅕ', 'ㅖ', 'ㅗ', 'ㅘ', 'ㅙ', 'ㅚ', 'ㅛ', 'ㅜ', 'ㅝ', 'ㅞ', 'ㅟ', 'ㅠ', 'ㅡ', 'ㅢ', 'ㅣ' };
    private readonly char[] 종성 = { ' ', 'ㄱ', 'ㄲ', 'ㄳ', 'ㄴ', 'ㄵ', 'ㄶ', 'ㄷ', 'ㄹ', 'ㄺ', 'ㄻ', 'ㄼ', 'ㄽ', 'ㄾ', 'ㄿ', 'ㅀ', 'ㅁ', 'ㅂ', 'ㅄ', 'ㅅ', 'ㅆ', 'ㅇ', 'ㅈ', 'ㅊ', 'ㅋ', 'ㅌ', 'ㅍ', 'ㅎ' };

    private bool isSkipping = false; // 텍스트 스킵 여부

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
    /// 글자가 모두 출력된 후 마우스 클릭을 기다리는 코루틴
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

        // 마우스 클릭 또는 5초 타임아웃을 기다림
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
        Debug.Log("마우스 클릭 또는 5초 대기를 시작합니다...");

        float elapsedTime = 0f;

        while (!Input.GetKeyDown(KeyCode.Space) && elapsedTime < timeout)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("마우스 클릭 감지!");
        }
        else
        {
            Debug.Log("5초 타임아웃!");
        }
    }

    private IEnumerator WaitForMouseClick()
    {
        Debug.Log("마우스 클릭을 기다리는 중...");

        while (!Input.GetMouseButtonDown(0))
        {
            yield return null;
        }

        Debug.Log("마우스 클릭 감지!");
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
        yield return new WaitForSeconds(0.05f);

        // 단계 2: 초성 + 중성 입력
        char 초중성 = (char)(0xAC00 + 초성Index * 21 * 28 + 중성Index * 28);
        tempText = prefix + 초중성;
        textDisplay.text = tempText;
        yield return new WaitForSeconds(0.05f);

        // 단계 3: 완성 글자 입력
        if (종성Index > 0)
        {
            char 완성글자 = (char)(0xAC00 + 초성Index * 21 * 28 + 중성Index * 28 + 종성Index);
            tempText = prefix + 완성글자;
            textDisplay.text = tempText;
        }
        else
        {
            tempText = prefix + 초중성;
            textDisplay.text = tempText;
        }

        yield return new WaitForSeconds(0.05f);
    }

    // 한글 여부 확인
    bool IsHangul(char c)
    {
        return c >= 0xAC00 && c <= 0xD7A3;
    }

}
