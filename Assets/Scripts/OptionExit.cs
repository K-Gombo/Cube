using UnityEngine;

public class OptionExit : MonoBehaviour
{
    public GameObject optionPanel; // 옵션 패널을 연결할 변수

    public void OnOptionExitClick() // 버튼 클릭 이벤트
    {
        optionPanel.SetActive(false); // 옵션 패널을 비활성화
    }
}