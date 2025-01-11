using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundReactiveCircle : MonoBehaviour
{
    private AudioSource audioSource;
    public float sensitivity = 100f;  // ���� ������ �ΰ���
    public float scaleMultiplier = 1f;  // ũ�� Ȯ�� ����
    private float[] spectrumData = new float[64];  // ����� ����Ʈ�� ������ �迭

    [SerializeField] private float smoothScale;  // �ε巯�� ũ�� ������ ���� ����
    public float smoothTime = 0.1f;  // SmoothDamp �ð� ���� ����
    [SerializeField] private float velocity = 0f;  // �ε巯�� ��ȭ�� ���Ǵ� �ӵ��� (SmoothDamp���� ���)

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // ����Ʈ�� ������ ��������
        audioSource.GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        // Ư�� ���ļ� �뿪 �����͸� ����Ͽ� ���� ũ�� ����
        float bassValue = spectrumData[1] * sensitivity;

        // �ε巯�� ũ�� ��ȭ ���� (y�� ��ġ ��ȭ�� ũ�� ��ȭ�� ��� ����)
        float targetSize = Mathf.Clamp(bassValue * scaleMultiplier, 7f, 8f);
        smoothScale = Mathf.SmoothDamp(smoothScale, targetSize, ref velocity, smoothTime);

        // ����� ũ�⸦ ���� �����Ͽ� �ݿ�
        transform.localScale = new Vector3(smoothScale, smoothScale, 1);
    }
}