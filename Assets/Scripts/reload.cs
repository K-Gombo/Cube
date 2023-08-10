using UnityEngine;
using UnityEngine.SceneManagement;

public class reload : MonoBehaviour
{
    public GameObject HomePanel; // HomePanel 오브젝트를 연결할 곳
    public GameObject CountPanel; // CountPanel 오브젝트를 연결할 곳

    private void Start()
    {
        // 씬이 로드될 때마다 실행할 메서드를 지정합니다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // HomePanel을 비활성화하고 CountPanel을 활성화합니다.
        HomePanel.SetActive(false);
        CountPanel.SetActive(true);
    }

    private void OnDestroy()
    {
        // 이벤트 할당을 제거하여 메모리 누수를 방지합니다.
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}