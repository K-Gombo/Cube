using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public GameObject HomePanel; // HomePanel 오브젝트를 연결할 곳
    public GameObject LoadingPanel; // LoadingPanel 오브젝트를 연결할 곳
    public AudioSource audioSource; // 효과음을 재생할 오디오 소스
    public AudioClip clickSound; // 효과음 클립

    // 버튼 컴포넌트를 캐싱하기 위한 변수
    private Button playButton;

    void Start()
    {
        // 현재 게임 오브젝트에 있는 Button 컴포넌트를 찾습니다.
        playButton = GetComponent<Button>();
        if (playButton != null)
        {
            // OnClick 이벤트에 메서드를 할당합니다.
            playButton.onClick.AddListener(OnClickPlayButton);
        }
    }

    // Play 버튼이 클릭되면 호출되는 메서드
    public void OnClickPlayButton()
    {
        audioSource.clip = clickSound; // 오디오 클립 지정
        audioSource.Play(); // 효과음 재생
        Invoke("ChangePanel", clickSound.length); // 효과음이 끝난 후 패널 변경
    }

    private void ChangePanel()
    {
        HomePanel.SetActive(false); // HomePanel 비활성화
        LoadingPanel.SetActive(true); // LoadingPanel 활성화
    }
}