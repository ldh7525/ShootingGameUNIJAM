using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundReactiveCircle : MonoBehaviour
{
    [SerializeField] private GameObject audioSource;
    public float sensitivity = 100f;  // 조정 가능한 민감도
    public float scaleMultiplier = 1f;  // 크기 확대 비율
    private float[] spectrumData = new float[64];  // 오디오 스펙트럼 데이터 배열

    [SerializeField] private float smoothScale;  // 부드러운 크기 조정을 위한 변수
    public float smoothTime = 0.1f;  // SmoothDamp 시간 조정 변수
    [SerializeField] private float velocity = 0f;  // 부드러운 변화에 사용되는 속도값 (SmoothDamp에서 사용)

    void Start()
    {
        audioSource = GameObject.Find("SoundManager");
    }

    void Update()
    {
        // 스펙트럼 데이터 가져오기
        audioSource.GetComponent<AudioSource>().GetSpectrumData(spectrumData, 0, FFTWindow.BlackmanHarris);

        // 특정 주파수 대역 데이터를 사용하여 원의 크기 변경
        float bassValue = spectrumData[1] * sensitivity;

        // 부드러운 크기 변화 적용 (y축 위치 변화와 크기 변화에 사용 가능)
        float targetSize = Mathf.Clamp(bassValue * scaleMultiplier, 6f, 7f);
        smoothScale = Mathf.SmoothDamp(smoothScale, targetSize, ref velocity, smoothTime);

        // 적용된 크기를 원의 스케일에 반영
        transform.localScale = new Vector3(smoothScale, smoothScale, 1);
    }
}
