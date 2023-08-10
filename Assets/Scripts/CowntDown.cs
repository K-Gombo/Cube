using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDown : MonoBehaviour
{
    public Text countDownText; // 카운트 다운 표시할 Text 컴포넌트
    public GameObject countPanel; // 카운트 다운 패널
    public GameObject normalPlayPanel; // 게임 플레이 패널
    
    private void Start()
    {
        // 게임 시작 시 카운트 다운 코루틴 실행
        StartCoroutine(CountDownCoroutine());
    }

    private IEnumerator CountDownCoroutine()
    {
        // 카운트 다운 3, 2, 1
        for (int i = 3; i > 0; i--)
        {
            countDownText.text = i.ToString();
            yield return new WaitForSeconds(1f); // 1초 대기
        }

        // 카운트 다운 후 패널 변경
        countPanel.SetActive(false);
        normalPlayPanel.SetActive(true);
       
    }
}