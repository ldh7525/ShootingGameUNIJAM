using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource music1; // Music1 오디오 소스
    [SerializeField] private AudioSource music2; // Music2 오디오 소스
    [SerializeField] private float fadeDuration = 0.5f; // 페이드 효과 지속 시간

    private bool isMusic2Playing = false; // 현재 재생 중인 음악 상태를 확인

    void Start()
    {
        PlayMusic1(); // 처음에는 Music1 재생
    }

    public void OnStartButtonPressed()
    {
        if (!isMusic2Playing)
        {
            StartCoroutine(SwitchToMusic2()); // Music2로 전환
        }
    }

    private void PlayMusic1()
    {
        music1.volume = 1f; // Music1 볼륨을 최대치로 설정
        music1.Play();      // Music1 재생
    }

    private IEnumerator SwitchToMusic2()
    {
        isMusic2Playing = true;

        // Music1 페이드 아웃
        yield return StartCoroutine(FadeOut(music1));

        // Music2 페이드 인
        music2.loop = true; // Music2에서만 루프 활성화
        music2.Play();
        yield return StartCoroutine(FadeIn(music2));
    }


    private IEnumerator FadeOut(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null; // 다음 프레임까지 대기
        }

        audioSource.volume = 0;
        audioSource.Stop();
    }

    private IEnumerator FadeIn(AudioSource audioSource)
    {
        float startVolume = 0f;
        audioSource.volume = startVolume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 1f, t / fadeDuration);
            yield return null; // 다음 프레임까지 대기
        }

        audioSource.volume = 1f;
    }
}
