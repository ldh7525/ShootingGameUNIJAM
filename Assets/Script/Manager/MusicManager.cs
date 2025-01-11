using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource music1; // Music1 ����� �ҽ�
    [SerializeField] private AudioSource music2; // Music2 ����� �ҽ�
    [SerializeField] private float fadeDuration = 0.5f; // ���̵� ȿ�� ���� �ð�

    private bool isMusic2Playing = false; // ���� ��� ���� ���� ���¸� Ȯ��

    void Start()
    {
        PlayMusic1(); // ó������ Music1 ���
    }

    public void OnStartButtonPressed()
    {
        if (!isMusic2Playing)
        {
            StartCoroutine(SwitchToMusic2()); // Music2�� ��ȯ
        }
    }

    private void PlayMusic1()
    {
        music1.volume = 1f; // Music1 ������ �ִ�ġ�� ����
        music1.Play();      // Music1 ���
    }

    private IEnumerator SwitchToMusic2()
    {
        isMusic2Playing = true;

        // Music1 ���̵� �ƿ�
        yield return StartCoroutine(FadeOut(music1));

        // Music2 ���̵� ��
        music2.loop = true; // Music2������ ���� Ȱ��ȭ
        music2.Play();
        yield return StartCoroutine(FadeIn(music2));
    }


    private IEnumerator FadeOut(AudioSource audioSource)
    {
        float startVolume = audioSource.volume;

        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(startVolume, 0, t / fadeDuration);
            yield return null; // ���� �����ӱ��� ���
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
            yield return null; // ���� �����ӱ��� ���
        }

        audioSource.volume = 1f;
    }
}
