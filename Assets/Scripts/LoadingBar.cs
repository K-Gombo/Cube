using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingBar : MonoBehaviour
{
    public Slider slider;
    public GameObject CountPanel; // CountPanel 오브젝트를 연결할 곳
    public GameObject LoadingPanel; // LoadingPanel 오브젝트를 연결할 곳
    private float targetValue = 1.0f; // 목표 값 (최대)
    private float duration = 2.5f; // 채우는 데 걸리는 시간 (2.5초)

    private void Start()
    {
        slider.value = 0; // 시작 시 슬라이더 값 초기화
        StartCoroutine(FillSlider());
    }

    private IEnumerator FillSlider()
    {
        float timeElapsed = 0;

        while (timeElapsed < duration)
        {
            timeElapsed += Time.deltaTime;
            slider.value = Mathf.Lerp(0, targetValue, timeElapsed / duration);
            yield return null;
        }

        slider.value = targetValue; // 슬라이더 값을 최종 목표 값으로 설정
        CountPanel.SetActive(true); // CountPanel 활성화
        LoadingPanel.SetActive(false); // LoadingPanel 비활성화
    }
}