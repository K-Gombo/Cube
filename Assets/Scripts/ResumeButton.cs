using UnityEngine;

public class ResumeButton : MonoBehaviour
{
    public PauseButton pauseButtonScript; // PauseButton의 스크립트를 참조하기 위한 변수

    public void OnResumeButtonClick()
    {
        pauseButtonScript.ResumeGame(); // PauseButton의 ResumeGame 함수 호출
    }
}