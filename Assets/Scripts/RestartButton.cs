using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public void OnRestartButtonClick()
    {
        // 게임의 타임스케일을 정상으로 복원
        Time.timeScale = 1;

        // 현재 씬 이름을 가져옴
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 현재 씬 이름이 "MainScene"인 경우 "GameScene"을 로드
        if (currentSceneName == "MainScene")
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            // 현재 씬을 다시 로드
            SceneManager.LoadScene(currentSceneName);
        }
    }
}