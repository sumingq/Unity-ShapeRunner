using UnityEngine;

// 발판으로서 필요한 동작을 담은 스크립트
public class Platform : MonoBehaviour
{
    public GameObject[] obstacles; // 장애물 오브젝트들

    // 컴포넌트가 활성화될때마다 매번 실행되는 메서드
    private void OnEnable()
    {
        //장애물 1개~3개 랜덤생성
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (Random.Range(0, 3) == 0)
            {
                obstacles[i].SetActive(true);
            }
            else
            {
                obstacles[i].SetActive(false);
            }
        }
    }
}