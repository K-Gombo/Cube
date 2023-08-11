using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SoundControl : MonoBehaviour
{
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    private Slider soundSlider;

    private void Start()
    {
        soundSlider = GetComponent<Slider>();
        soundSlider.value = audioSource1.volume; // 초기값은 audioSource1의 볼륨으로 설정
        soundSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        audioSource1.volume = volume;
        audioSource2.volume = volume;
    }
}