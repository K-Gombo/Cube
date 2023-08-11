using UnityEngine;

public class OptionButton : MonoBehaviour
{
    public GameObject optionPanel; // 옵션 패널을 연결할 변수

    public void OnOptionButtonClick() // 버튼 클릭 이벤트
    {
        optionPanel.SetActive(true); // 옵션 패널을 활성화
    }
}