using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 게임 오버 상태를 표현하고, 게임 점수와 UI를 관리하는 게임 매니저
// 씬에는 단 하나의 게임 매니저만 존재할 수 있다.
public class GameManager : MonoBehaviour
{
    public static GameManager instance; // 싱글톤을 할당할 전역 변수

    public bool isGameover = false; // 게임 오버 상태
    public Text scoreText; // 점수를 출력할 UI 텍스트
    public GameObject gameoverUI; // 게임 오버시 활성화 할 UI 게임 오브젝트
    public int itemnum;

    private int score = 0; // 게임 점수

    // 게임 시작과 동시에 싱글톤을 구성
    void Awake()
    {
        // 싱글톤 변수 instance가 비어있는가?
        if (instance == null)
        {
            // instance가 비어있다면(null) 그곳에 자기 자신을 할당
            instance = this;
        }
        else
        {
            // 씬에 두개 이상의 GameManager 오브젝트가 존재한다는 의미.
            // 싱글톤 오브젝트는 하나만 존재해야 하므로 자신의 게임 오브젝트를 파괴
            Debug.LogWarning("씬에 두개 이상의 게임 매니저가 존재합니다!");
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // 게임 오버 상태에서 게임을 재시작할 수 있게 하는 처리
        if (isGameover && Input.GetMouseButtonDown(0))
        {
            //현재 씬의 이름을 변수 name으로 제공
            //따라서 SceneManager.GetActiveScene().name은 현재 씬의 이름을 가져온 것
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == "Level 1")
        {
            if (score >= itemnum)
            {
                SceneManager.LoadScene("Level 2");
            }
        }
        else if (currentSceneName == "Level 2")
        {
            if (score >= itemnum)
            {
                SceneManager.LoadScene("Level 3");
            }
        }
        else if (currentSceneName == "Level 3")
        {
            if (score >= itemnum)
            {
                SceneManager.LoadScene("Outro");
            }
        }
    }

    // 점수를 증가시키는 메서드
    public void AddScore(int newScore)
    {
        if (!isGameover)
        {
            score += newScore;
            scoreText.text = "SCORE : " + score;
        }
    }

    // 플레이어 캐릭터가 사망시 게임 오버를 실행하는 메서드
    public void OnPlayerDead()
    {
        isGameover = true;
        gameoverUI.SetActive(true);
    }
}