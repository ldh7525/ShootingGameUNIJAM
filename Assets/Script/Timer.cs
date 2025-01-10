using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // TextMeshPro UI �ؽ�Ʈ ����
    private float elapsedTime = 0f;  // ���� �ð�

    void Start()
    {
        // ���� ���� �� Ÿ�̸� �ʱ�ȭ
        elapsedTime = 0f;
    }

    void Update()
    {
        // �� ������ ��� �ð� ����
        elapsedTime += Time.deltaTime;

        // �ؽ�Ʈ ������Ʈ
        UpdateTimerText();
    }

    private void UpdateTimerText()
    {
        // ���� �ð��� ��:��:�и��� �������� ��ȯ
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 100f) % 100f);

        // �ؽ�Ʈ ������Ʈ (00:00:000 ����)
        timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }
}
