using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    public void OnRetryButtonClick()
    {
        // 현재 씬 이름을 가져옵니다.
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 현재 씬 이름이 "MainScene"인 경우 "GameScene"을 로드합니다.
        if (currentSceneName == "MainScene")
        {
            SceneManager.LoadScene("GameScene");
        }
        else
        {
            // 현재 씬을 다시 로드합니다.
            SceneManager.LoadScene(currentSceneName);
        }
    }
}
