using UnityEngine;

public class PauseButton : MonoBehaviour
{
    private bool isPaused = false; 
    public GameObject pauseBackPanel; 
    public GameObject pausePanel; 
    public GameObject leftButton; 
    public GameObject rightButton; 
    public GameObject pauseButton; 
    void Start()
    {
        if (pauseBackPanel != null)
            pauseBackPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);
    }

    public void OnPauseButtonClick()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        isPaused = true;

        if (pauseBackPanel != null)
            pauseBackPanel.SetActive(true);

        if (pausePanel != null)
            pausePanel.SetActive(true);

        if (leftButton != null)
            leftButton.SetActive(false); // LeftButton 비활성화

        if (rightButton != null)
            rightButton.SetActive(false); // RightButton 비활성화
        
        if (pauseButton != null)
            pauseButton.SetActive(false); // PauseButton 자신을 비활성화
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        isPaused = false;

        if (pauseBackPanel != null)
            pauseBackPanel.SetActive(false);

        if (pausePanel != null)
            pausePanel.SetActive(false);

        if (leftButton != null)
            leftButton.SetActive(true); // LeftButton 활성화

        if (rightButton != null)
            rightButton.SetActive(true); // RightButton 활성화
        
        if (pauseButton != null)
            pauseButton.SetActive(true); // PauseButton 자신을 활성화
    }
}