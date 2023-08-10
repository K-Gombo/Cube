using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    public void OnHomeButtonClick()
    {
        // "MainScene"을 로드합니다.
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
    }
}
