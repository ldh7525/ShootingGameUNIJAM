using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // TextMeshPro UI 텍스트 연결
    private float elapsedTime = 0f;  // 지난 시간

    void Start()
    {
        // 게임 시작 시 타이머 초기화
        elapsedTime = 0f;
    }

    void Update()
    {
        // 매 프레임 경과 시간 누적
        elapsedTime += Time.deltaTime;

        // 텍스트 업데이트
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        // 지난 시간을 분:초:밀리초 형식으로 변환
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 100f) % 100f);

        // 텍스트 업데이트 (00:00:000 형식)
        timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }
}
