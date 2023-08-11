using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class MusicControl : MonoBehaviour
{
    public AudioSource bgmSource1; // 첫 번째 BGM 소스
    public AudioSource bgmSource2; // 두 번째 BGM 소스
    private Slider volumeSlider;   // 음량 조절 슬라이더

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();
        volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        volumeSlider.value = bgmSource1.volume; // 초기값 설정 (두 BGM의 초기 볼륨이 동일하다고 가정)
    }

    private void OnVolumeChanged(float value)
    {
        bgmSource1.volume = value; // 첫 번째 BGM의 음량 조절
        bgmSource2.volume = value; // 두 번째 BGM의 음량 조절
    }
}